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
        }
    }
});

app.controller('Ctroller', function ($scope, $routeParams, $uibModal, $rootScope, datasevice) {
    $scope.seat;
    $scope.dsghedachon = [];

    var socket = io.connect('https://my-cinema-qlrp.herokuapp.com/bookticket');

    //$scope.socket = io.connect('localhost:3000/bookticket');
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
                    }
                    else {
                        $scope.ListSeat = rs.object;
                        console.log($scope.ListSeat);
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

        $scope.dsghedachon = [];
        if (data != null) {
            data.forEach(function (seat, ind) {
                if (seat.idGhe != -1) {
                    $scope.dsghedachon.push(seat.idGhe);
                    if ($scope.seat != null && $scope.seat == seat.idGhe)
                        $scope.seat = null;
                }

            });
        }
        $scope.dsghedachon = $scope.dsghedachon.concat($scope.dsGheDaThanhToan);
        console.log("dang load ghe");
        $scope.$apply(function () {
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
                key: 'chon-ghe',
                idGhe: $scope.seat.id,
                idLichChieu: $scope.idShowtime
            };
            //gửi socket lên sever
            socket.emit('Client-to-server-to-all', data);

            var modalInstance = $uibModal.open({
                scope: $scope,
                animation: true,
                templateUrl: ctxfolderurl + "/bookticket/payment.htm",
                controller: "payment",
                backdrop: 'static',
                size: 'lg',
            });

            modalInstance.result.then(function (result) {
                console.log('result: ' + result);
                // $scope.schedule = angular.fromJson(scheduleJSON);
                data.key = 'huy-ghe-da-chon';
                socket.emit('Client-to-server-to-all', data);
            }, function () {
                console.info('Modal dismissed at: ' + new Date());
            });
        }
        else {
            alert('Ban chưa chon ghe');
        }
    };
    $scope.controller = "bookticket";
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