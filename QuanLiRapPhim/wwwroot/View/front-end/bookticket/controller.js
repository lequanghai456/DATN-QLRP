var ctxfolderurl = "/View/front-end";

var app = angular.module('App', ['ui.bootstrap', 'ngRoute', 'ngAnimate']);

app.factory("datasevice", function ($http) {
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }

    return {
        GetRoom: function (data,callback) {
            $http.get('/Bookticket/getRoomByIdShowtime/'+data).then(callback);
        },
        GetMovie: function (data,callback) {
            $http.get('/Bookticket/getmovie/'+data).then(callback);
        },
        GetListSeatChoosed: function (callback) {
            $http.get('/Bookticket/GetListSeatChoosed').then(callback);
        },
        getListSeatOfShowtime: function(data,callback) {
            $http.get('/Bookticket/getListSeatOfShowtime/'+data).then(callback);
        },
        DsGheDaDat: function (id, callback) {
            $http.get('/Bookticket/DsGheDaDat/' + id).then(callback);
        }
    }
});

app.controller('Ctroller', function ($scope, $routeParams, $uibModal, $rootScope, datasevice) {
    $scope.seat;
    $scope.dsghedachon = [];

    var socket = io.connect('https://my-cinema-qlrp.herokuapp.com/bookticket');

    //var socket = io.connect('http://localhost:3000/bookticket');
    socket.on('connect', function () {

        $.blockUI({
            boxed: true,
            message: 'loading...'
        });

        datasevice.GetRoom($scope.idShowtime, function (rs) {
            rs = rs.data;
            console.log(rs);
            if (rs.error) {
                alert(rs.title);
            }
            else {
                $scope.room = rs.object;

                $scope.col = range(0, $scope.room.col-1, 1);
                
                datasevice.getListSeatOfShowtime($scope.idShowtime, function (rs) {
                    rs = rs.data;
                    if (rs.error) {
                        alert(rs.title);
                        $scope.room = null;
                    }
                    else {
                        $scope.ListSeat = rs.object;
                    }
                });

                console.log($scope.seats);
            }
        });

        socket.emit('Join room', { idLichChieu: $scope.idShowtime });

    });

    $scope.dsGheDaThanhToan = [];

        //load những ghế đang được người khác chọn
    socket.on('load_ghe_da_chon', function (data) {
        console.log(data);
        datasevice.DsGheDaDat($scope.idShowtime, function (rs) {
            $scope.dsghedachon = [];
            rs = rs.data;
            if (rs.error) {
                alert(rs.title);
            }
            else {
                $scope.dsghedachon = rs.object;
                console.log(rs.object);
            }

            if (data != null) {
                data.forEach(function (seat, ind) {
                        $scope.dsghedachon.push(seat);
                        if ($scope.seat != null && $scope.seat == seat)
                            $scope.seat = null;
                });
            }

            $scope.dsghedachon = $scope.dsghedachon.concat($scope.dsGheDaThanhToan);
            console.log("dang load ghe");
            $scope.$apply;

            $.unblockUI();
        });
    });

    $scope.Click = function (seat) {
        if (seat.status == 0) {
            if ($scope.dsghedachon.indexOf(seat.id) === -1) {
                //đổi thuộc tính ghế đã chọn
                $scope.seat = seat;
                $scope.$apply;
            }
            else {
                alert('ghế đã có người chọn');
            }
        }
    }
    function range(start, stop, step) {
        var a = [start], b = start;
        if (typeof start == 'bigint') {
            stop = BigInt(stop)
            step = step ? BigInt(step) : 1n;
        } else
            step = step || 1;
        while (b < stop) {
            a.push(b += step);
        }
        return a;
    }
    $scope.open = function () {
        if ($scope.seat != null) {
            var data = {
                idGhe: $scope.seat.id
            };
            //gửi socket lên sever
            socket.emit('ChonGhe', data);

            var modalInstance = $uibModal.open({
                scope: $scope,
                animation: true,
                templateUrl: ctxfolderurl + "/bookticket/payment.html",
                controller: "payment",
                backdrop: 'static',
                size: 'lg',
            });

            modalInstance.result.then(function (result) {
                console.log('result: ' + result);
                // $scope.schedule = angular.fromJson(scheduleJSON);
                socket.emit('HuyGhe', data);
            }, function () {
                console.info('Modal dismissed at: ' + new Date());
            });
        }
        else {
            alert('Ban chưa chon ghe');
        }
    };
    $scope.choseService = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: ctxfolderurl + "/your_order/chose-services.html",
            controller: "choseService",
            size: 'xl'
        });

        modalInstance.result.then(function (res) {
            //xử lý payment và cancel
        });
    }
});
app.controller('payment', function ($scope, $uibModalInstance, $uibModal) {
    $scope.data = {
        Date: $scope.room.date,
        Name: $scope.room.user,
        Room: $scope.room.name,
        Seat: { X: $scope.seat.x, Y: $scope.seat.y },
        Title: $scope.room.title,
        Time: $scope.room.time,
    }
    console.log($scope.data);
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
        $("#bookticket").submit();
        $uibModalInstance.close('ok');
    }

});


app.directive('myTicket', function () {
    function link($scope, element, attributes) {
        $scope.data = $scope.model;
        console.log($scope.data);
    }

    return {
        restrict: 'E',
        scope: {
            model: '='
        },
        templateUrl: "/View/front-end/your_order/Ticket.htm",
        link: link
    };
});