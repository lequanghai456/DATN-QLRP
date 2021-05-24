var ctxfolderurl = "/View/front-end/movie_review";

var app = angular.module('App', ['ui.bootstrap', 'ngRoute', 'ngAnimate']);

app.controller('Ctroller', function ($scope) {
    $scope.init = function () {
        $("#movie_review").addClass("current-menu-item");
    }
    $scope.init();
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

app.controller('index', function ($scope, $rootscope) {
    $rootscope.data = [
        {
            id:1,
            name: 'Maleficient',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-3.jpg'
        }, {
            id: 2,
            name: 'Maleficient',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-4.jpg'
        }, {
            id: 3,
            name: 'The adventure of Tintin',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-5.jpg'
        }, {
            id: 4,
            name: 'Hobbit',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-6.jpg'
        }, {
            id: 5,
            name: 'Exists',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-7.jpg'
        }, {
            id: 6,
            name: 'Drive hard',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-8.jpg'
        }, {
            id: 7,
            name: 'Maleficient',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-1.jpg'
        }, {
            id: 8,
            name: 'Robocop',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-2.jpg'
        }, {
            id: 9,
            name: 'Life of Pi',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-3.jpg'
        }, {
            id: 10,
            name: 'The Colony',
            descript: 'Sed ut perspiciatis unde omnis iste natus error voluptatem doloremque.',
            img: 'thumb-3.jpg'
        }
    ];

    $scope.totalItems = $rootscope.data.length;
    $scope.currentPage = 1;
    $scope.numPerPage = 8;
    $scope.maxSize = 5; //Number of pager buttons to show

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.numPages = function () {
        return Math.ceil($rootscope.data.length / $scope.numPerPage);
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

        $scope.movies = $rootscope.data.slice(begin, end);
    });
    $(document).ready(function () {
        $(".pagination li").addClass("page-item");
        $(".pagination li a").addClass("page-link");
    });
});

app.controller('moviedetail', function ($scope, $routeParams) {
    $scope.NameMovie = $routeParams.NameMovie;
    //viết api kiểm tra tồn tại của phim
    
});
app.controller('bookticket', function ($scope, $routeParams,$uibModal) {
    $scope.seat;
    $scope.dsghedachon = [];

    $scope.init = function () {
        //kiểm tra tồn tại của phim
        if (false) {
            //vào trang không tìm thấy phim
        } 
        
    }

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