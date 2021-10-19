var ctxfolderurl = "/View/staff";

var app = angular.module('App', ['ngRoute']);

app.controller('Ctroller', function ($scope) {
});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
        .when('/:id', {
            templateUrl: ctxfolderurl + '/bookTicket.html',
            controller: 'bookTicket'            
        });
});


app.factory('dataservice', function ($http) {
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }

    return {
        GetListShowTime: function (Date, callback) {
            $http.get('/Showtimes/GetListShowtime?date=' + Date).then(callback);
        },
        GetRoom: function (data, callback) {
            $http.get('/Bookticket/getRoomByIdShowtime/' + data).then(callback);
        },
        getListSeatOfShowtime: function (data, callback) {
            $http.get('/Bookticket/getListSeatOfShowtime/' + data).then(callback);
        },
        DsGheDaDat: function (id, callback) {
            $http.get('/Bookticket/DsGheDaDat/' + id).then(callback);
        }
    }
});

app.controller('index', function ($scope, dataservice) {
    $scope.selectday = function () {
        if ($scope.day != "") {
            $scope.day=$scope.day.toLocaleDateString('en-US');
        }
        dataservice.GetListShowTime($scope.day, function (rs) {
            rs = rs.data;
            if (rs != null && !rs.error) {
                console.log(rs);
                $scope.data = rs;
                $scope.$apply();
            }
            else {
                $scope.mess = rs.title;
            }
        });
    }
    $scope.day = "";
    $scope.init = function () {
        $scope.selectday();
    }
    $scope.init();
});

app.controller('bookTicket', function ($scope, dataservice, $routeParams) {
    $scope.id = $routeParams.id;
    $scope.seat;
    $scope.dsghedachon = [];

    //var socket = io.connect('https://my-cinema-qlrp.herokuapp.com/bookticket');

    var socket = io.connect('http://localhost:3000/bookticket');
    socket.on('connect', function () {

        $.blockUI({
            boxed: true,
            message: 'loading...'
        });

        dataservice.GetRoom($scope.id, function (rs) {
            rs = rs.data;
            console.log(rs);
            if (rs.error) {
                alert(rs.title);
            }
            else {
                $scope.room = rs.object;

                $scope.col = range(0, $scope.room.col - 1, 1);

                dataservice .getListSeatOfShowtime($scope.id, function (rs) {
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

        socket.emit('Join room', { idLichChieu: $scope.id });

    });

    $scope.dsGheDaThanhToan = [];

    //load những ghế đang được người khác chọn
    socket.on('load_ghe_da_chon', function (data) {
        dataservice.DsGheDaDat($scope.id, function (rs) {
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
                    if (seat.idGhe != -1) {
                        $scope.dsghedachon.push(seat.idGhe);
                        if ($scope.seat != null && $scope.seat == seat.idGhe)
                            $scope.seat = null;
                    }

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
                key: 'chon-ghe',
                idGhe: $scope.seat.id,
                idLichChieu: $scope.idShowtime
            };
            //gửi socket lên sever
            socket.emit('Client-to-server-to-all', data);

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
    //$scope.choseService = function () {
    //    var modalInstance = $uibModal.open({
    //        animation: true,
    //        templateUrl: ctxfolderurl + "/your_order/chose-services.html",
    //        controller: "choseService",
    //        size: 'xl'
    //    });

    //    modalInstance.result.then(function (res) {
    //        //xử lý payment và cancel
    //    });
    //}
});
