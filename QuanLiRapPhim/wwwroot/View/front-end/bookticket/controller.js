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
        },
    }
});

app.controller('bookTicket', function ($scope, datasevice, $routeParams, $http, $uibModal) {
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

                $('title').html("Đặt vé - " + $scope.room.title);

                $scope.col = range(0, $scope.room.col - 1, 1);

                datasevice.getListSeatOfShowtime($scope.idShowtime, function (rs) {
                    rs = rs.data;
                    if (rs.error) {
                        alert(rs.title);
                        $scope.room = null;
                    }
                    else {
                        $scope.ListSeat = rs.object;
                        $.unblockUI();
                    }
                });

            }
        });

        socket.emit('Join room', { idLichChieu: $scope.idShowtime });

    });
    $scope.InVe = function () {
        if ($scope.listseat.length > 0) {
        $("#submit").submit();
        location.reload();

        }
    }
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
            if ($scope.dsghedachon != undefined)
                $scope.dsghedachon = $scope.dsghedachon.concat($scope.dsGheDaThanhToan);
            console.log("dang load ghe");
            $scope.$apply;

        });
    });

    $scope.Time = function () {
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

    $scope.back = function () {
        clearInterval($scope.timeID);
        $scope.listseat.forEach(function (ghe) {
            var data = {
                idGhe: ghe.id,
            };
            socket.emit('HuyGhe', data);
        });
        $scope.listseat = [];
    }

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

    $scope.listseat = [];
    $scope.addseat = function () {
        
        if ($scope.seat != null) {

            if ($scope.listseat.length == 0) {
                $scope.Time();
            } else {
                $scope.minuted += 1;
            }
            var data = {
                idGhe: $scope.seat.id,
            };
            //gửi socket lên sever
            socket.emit('ChonGhe', data);

            $scope.listseat.push($scope.seat);
            $scope.seat = null;

        }

    }

    $scope.Total = function (listseat) {
        var total = 0;
        listseat.forEach(function (s) {
            total += s.price;
        });
        //console.log(listseat);
        return total;
    }

    //$scope.InVe = function () {

    //    function listid() {
    //        var a = [];
    //        $scope.listseat.forEach(function (data1) {
    //            a.push(data1.id);
    //        });
    //        return a;
    //    }

    //    var st = {
    //        id: $scope.idShowtime,
    //        ids: listid()
    //    };

    //    datasevice.BookTickets(st, function (rs) {
    //        rs = rs.data;
    //        console.log(rs);
    //        if (!rs.error) {
    //            $http.defaults.headers.common.Authorization = '87c21eb5-0a4a-4b31-bba8-a8593992b110'; //replace with our API key from portal.api2pdf.com

    //            var endpoint = "https://v2018.api2pdf.com/chrome/html"
    //            var payload;

    //            var html = "";
    //            $scope.listseat.forEach(function (s) {
    //                var model = {
    //                    Objects: {
    //                        Title: $scope.room.title,
    //                        Name: "-------",
    //                        Room: $scope.room.name,
    //                        Seat: {
    //                            X: s.x,
    //                            Y: s.y,
    //                        },
    //                        Time: $scope.room.time,
    //                        Date: $scope.room.date,
    //                    }
    //                };
    //                html += HtmlTicket(model);
    //            });
    //            console.log(html);
    //            ////$http.get("https://localhost:44350/staffs/Home/Ticket/11").then(function (rs) {
    //            ////console.log(rs);

    //            payload = {
    //                html: html, //HtmlTicket(model),//"<p>Chó Hải dm</p>" //set your HTML here!
    //                inlinePdf: true
    //            }


    //            $http.post(endpoint, payload).then(
    //                function (response) {
    //                    //your PDF is in this response. Do something with it!
    //                    $scope.pdf = response.data.pdf;
    //                    console.log($scope.pdf)
    //                    $scope.$apply;

    //                    window.open($scope.pdf, '_blank').focus();
    //                    location.reload();
    //                },
    //                function (error) {

    //                });
    //        }
    //    });
    //}

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
    $scope.choseService = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: ctxfolderurl + "/your_order/chose-services.html",
            controller: "choseService",
            size: 'xl'
        });

        modalInstance.result.then(function (res) {
            
        });
    }
});

//app.controller('Ctroller', function ($scope, $routeParams, $uibModal, $rootScope, datasevice) {
//    $scope.seat;
//    $scope.dsghedachon = [];

//    var socket = io.connect('https://my-cinema-qlrp.herokuapp.com/bookticket');

//    //var socket = io.connect('http://localhost:3000/bookticket');
//    socket.on('connect', function () {

//        $.blockUI({
//            boxed: true,
//            message: 'loading...'
//        });

//        datasevice.GetRoom($scope.idShowtime, function (rs) {
//            rs = rs.data;
//            console.log(rs);
//            if (rs.error) {
//                alert(rs.title);
//            }
//            else {
//                $scope.room = rs.object;

//                $scope.col = range(0, $scope.room.col-1, 1);
                
//                datasevice.getListSeatOfShowtime($scope.idShowtime, function (rs) {
//                    rs = rs.data;
//                    if (rs.error) {
//                        alert(rs.title);
//                        $scope.room = null;
//                    }
//                    else {
//                        $scope.ListSeat = rs.object;
//                    }
//                });

//                console.log($scope.seats);
//            }
//        });

//        socket.emit('Join room', { idLichChieu: $scope.idShowtime });

//    });

//    $scope.dsGheDaThanhToan = [];

//        //load những ghế đang được người khác chọn
//    socket.on('load_ghe_da_chon', function (data) {
//        console.log(data);
//        datasevice.DsGheDaDat($scope.idShowtime, function (rs) {
//            $scope.dsghedachon = [];
//            rs = rs.data;
//            if (rs.error) {
//                alert(rs.title);
//            }
//            else {
//                $scope.dsghedachon = rs.object;
//                console.log(rs.object);
//            }

//            if (data != null) {
//                data.forEach(function (seat, ind) {
//                        $scope.dsghedachon.push(seat);
//                        if ($scope.seat != null && $scope.seat == seat)
//                            $scope.seat = null;
//                });
//            }

//            $scope.dsghedachon = $scope.dsghedachon.concat($scope.dsGheDaThanhToan);
//            console.log("dang load ghe");
//            $scope.$apply;

//            $.unblockUI();
//        });
//    });

//    $scope.Click = function (seat) {
//        if (seat.status == 0) {
//            if ($scope.dsghedachon.indexOf(seat.id) === -1) {
//                //đổi thuộc tính ghế đã chọn
//                $scope.seat = seat;
//                $scope.$apply;
//            }
//            else {
//                alert('ghế đã có người chọn');
//            }
//        }
//    }
//    function range(start, stop, step) {
//        var a = [start], b = start;
//        if (typeof start == 'bigint') {
//            stop = BigInt(stop)
//            step = step ? BigInt(step) : 1n;
//        } else
//            step = step || 1;
//        while (b < stop) {
//            a.push(b += step);
//        }
//        return a;
//    }
//    $scope.open = function () {
//        if ($scope.seat != null) {
//            var data = {
//                idGhe: $scope.seat.id
//            };
//            //gửi socket lên sever
//            socket.emit('ChonGhe', data);

//            var modalInstance = $uibModal.open({
//                scope: $scope,
//                animation: true,
//                templateUrl: ctxfolderurl + "/bookticket/payment.html",
//                controller: "payment",
//                backdrop: 'static',
//                size: 'lg',
//            });

//            modalInstance.result.then(function (result) {
//                console.log('result: ' + result);
//                // $scope.schedule = angular.fromJson(scheduleJSON);
//                socket.emit('HuyGhe', data);
//            }, function () {
//                console.info('Modal dismissed at: ' + new Date());
//            });
//        }
//        else {
//            alert('Ban chưa chon ghe');
//        }
//    };
//    
//});
//app.controller('payment', function ($scope, $uibModalInstance, $uibModal) {
//    $scope.data = {
//        Date: $scope.room.date,
//        Name: $scope.room.user,
//        Room: $scope.room.name,
//        Seat: { X: $scope.seat.x, Y: $scope.seat.y },
//        Title: $scope.room.title,
//        Time: $scope.room.time,
//    }
//    console.log($scope.data);
//    $scope.init = function () {
//        $scope.minuted = 1;
//        $scope.second = 10;
//        $scope.timeID = setInterval(function () {
//            $scope.second -= 1;
//            if ($scope.second == 0) {
//                if ($scope.minuted == 0) {
//                    $scope.back();
//                } else
//                    $scope.minuted -= 1;
//                $scope.second = 60;
//            }
//            $scope.$apply();
//        }, 1000);
//    }

//    $scope.init();

//    $scope.back = function () {
//        clearInterval($scope.timeID);
//        $uibModalInstance.close('cancel');
//    }

//    $scope.payment = function () {
//        clearInterval($scope.timeID);
//        $("#bookticket").submit();
//        $uibModalInstance.close('ok');
//    }

//});


//app.directive('myTicket', function () {
//    function link($scope, element, attributes) {
//        $scope.data = $scope.model;
//        console.log($scope.data);
//    }

//    return {
//        restrict: 'E',
//        scope: {
//            model: '='
//        },
//        templateUrl: "/View/front-end/your_order/Ticket.htm",
//        link: link
//    };
//});