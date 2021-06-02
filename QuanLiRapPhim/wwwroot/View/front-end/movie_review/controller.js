var ctxfolderurl = "/View/front-end/movie_review";

var app = angular.module('App', ['ui.bootstrap', 'ngRoute', 'ngAnimate']);

app.controller('Ctroller', function ($scope) {
    $scope.init = function () {
        $("#movie_review").addClass("current-menu-item");
    }
    $scope.init();
});

app.factory('dataservice', function ($http) {
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }

    return {
        GetMovie: function (callback) {
            $http.get('/moviereview/getmovie').then(callback);
        }
            
    }
});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        }).when('/:NameMovie', {
            templateUrl: ctxfolderurl + '/moviedetail.html',
            controller: 'moviedetail'
        })
        .when('/:NameMovie/bookticket/:id', {
            templateUrl: ctxfolderurl + '/bookticket.html',
            controller: 'bookticket'
        }).when('/:NameMovie/bookticket/:id/payment', {
            templateUrl: ctxfolderurl + '/payment.html',
            controller: 'payment'
        });
});

app.controller('index', function ($scope, $rootScope, dataservice) {
    $scope.init = function () {
        dataservice.GetMovie(function (rs) {
            $rootScope.data = rs.data;

            if ($rootScope.data == null) {
                console.log('Không có phim');
            } else {
                $scope.totalItems = $rootScope.data.length;
                $scope.currentPage = 1;
                $scope.numPerPage = 8;
                $scope.maxSize = 5; //Number of pager buttons to show

                $scope.setPage = function (pageNo) {
                    $scope.currentPage = pageNo;
                };

                $scope.numPages = function () {
                    return Math.ceil($rootScope.data.length / $scope.numPerPage);
                }

                $scope.pageChanged = function () {
                    console.log('Page changed to: ' + $scope.currentPage);
                };

                $scope.setItemsPerPage = function (num) {
                    $scope.itemsPerPage = num;
                    $scope.currentPage = 1;
                }
                $scope.$watch('currentPage + numPerPage', function () {
                    var begin = (($scope.currentPage - 1) * $scope.numPerPage)
                        , end = begin + $scope.numPerPage;

                    $scope.movies = $rootScope.data.slice(begin, end);
                });
                $(document).ready(function () {
                    $(".pagination li").addClass("page-item");
                    $(".pagination li a").addClass("page-link");
                });
                    }
        });
    }
    $scope.init();
});

app.controller('moviedetail', function ($scope, $routeParams) {
    $scope.NameMovie = $routeParams.NameMovie;
    //viết api kiểm tra tồn tại của phim

    $scope.init = function () {
        //kiểm tra tồn tại của phim
        if (false) {
            //vào trang không tìm thấy phim
        }

    }
    $scope.traloi = function (id) {
        $scope.comment = id;
    }
    
});
app.controller('bookticket', function ($scope, $routeParams,$uibModal,$rootScope) {
    $scope.seat;
    $scope.dsghedachon = [];

    

    //var socket = io.connect('https://my-cinema-qlrp.herokuapp.com/bookticket');
    var socket = io.connect('localhost:3000/bookticket');

    socket.on('connect', function () {
        socket.emit('Join room', { idLichChieu: $routeParams.id });
    });

    //api lấy ghế đã thanh toán
    var dsGheDaThanhToan = [1, 2, 5, 3];

    //load những ghế đang được người khác chọn
    socket.on('load_ghe_da_chon', function (data) {
        $scope.dsghedachon = [];
        if (data != null) {
            data.forEach(function (seat, ind) {
                if (seat.idGhe != -1) {
                    $scope.dsghedachon.push(seat.idGhe);
                    if ($scope.seat!=null && $scope.seat == seat.idGhe) $scope.seat = null;
                }
                    
            });
        }
        $scope.dsghedachon = $scope.dsghedachon.concat(dsGheDaThanhToan);
        $scope.$apply();
    });

    $scope.room = {
        row: 8,
        col: 9,
    }

    //khởi tạo ghế cho phòng
    $scope.seats = [];
    for (var i = 0; i < $scope.room.col; i++) {
        $scope.seats[i] = [];
        $scope.seats[i].name = String.fromCharCode(65+i);
        for (var j = 0; j < $scope.room.row; j++) {
            $scope.seats[i][j] = {
                id: i * 10 + j,
                status: true
            };
        }
    }

    //khởi tạo ghế hỏng
    $scope.seats[0][0].status = false;

    //khi chon 1 ghe
    $scope.Click = function (seat) {
        if (seat.status) {
            if ($scope.dsghedachon.indexOf(seat.id) === -1) {
                //đổi thuộc tính ghế đã chọn
                $scope.seat = seat.id;
            }
            else {
                alert('ghế đã có người chọn');
            }
        }
    }

    $scope.open = function () {
        if ($scope.seat != null) {
            var data = {
                key: 'chon-ghe',
                idGhe: $scope.seat,
                idLichChieu: $routeParams.id
            };

            //gửi socket lên sever
            socket.emit('Client-to-server-to-all', data);

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: ctxfolderurl + "/payment.html",
                controller: "payment",
                size: 'lg',
            });

            modalInstance.result.then(function (response) {
                if (response == 'cancel') {
                    data.key = 'huy-ghe-da-chon';
                    socket.emit('Client-to-server-to-all', data);
                    $scope.seat = data.idGhe;
                }
            }, function () {
                    data.key = 'huy-ghe-da-chon';
                    socket.emit('Client-to-server-to-all', data);
            });
        }
        else {
            alert('Ban chưa chon ghe');
        }

        

    };

    $scope.payment = function () {
        
    }
});
app.controller('payment', function ($scope, $uibModalInstance, $uibModal) {
    $scope.init = function () {
        $scope.minuted = 1;
        $scope.second = 10;
        $scope.timeID = setInterval(function () {
            $scope.second -= 1;
            if ($scope.second == 0) {
                if ($scope.minuted == 0) {
                    $scope.back();
                } else
                    $scope.minuted -= 1;
                $scope.second = 60;
            }
            $scope.$apply();
        }, 1000);
    }

    $scope.init();

    $scope.back = function () {
        clearInterval($scope.timeID);
        $uibModalInstance.close('cancel');
    }

    $scope.payment = function () {
        clearInterval($scope.timeID);
        $uibModalInstance.close('ok');
    }

});