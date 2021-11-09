var ctxfolderurl = "/View/staff";

var app = angular.module('App', ['ui.bootstrap', 'ngRoute', 'datatables']);

app.controller('Ctroller', function ($scope) {
});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
        .when('/TicketPrint', {
            templateUrl: ctxfolderurl + '/TicketPrint.html',
            controller: 'TicketPrint'
        })
        .when('/Sevice', {
            templateUrl: ctxfolderurl + '/Sevice.html',
            controller: 'choseService'
        })
        .when('/:id', {
            templateUrl: ctxfolderurl + '/bookTicket.html',
            controller: 'bookTicket'
        });
});
app.factory('service', function ($http) {
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }

    return {
        GetAllSevice: function (callback) {
            $http.get("https://localhost:44350/YourOrder/GetAllServices").then(callback);
        },
        BuyBill: function (data, callback) {
            $http.post("https://localhost:44350/Staffs/home/BuySevice",data).then(callback);
        }
    }
});
app.factory("datasevice", function ($http) {
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }

    return {
        GetRoom: function (data, callback) {
            $http.get('/Bookticket/getRoomByIdShowtime/' + data).then(callback);
        },
        GetMovie: function (data, callback) {
            $http.get('/Bookticket/getmovie/' + data).then(callback);
        },
        GetListSeatChoosed: function (callback) {
            $http.get('/Bookticket/GetListSeatChoosed').then(callback);
        },
        getListSeatOfShowtime: function (data, callback) {
            $http.get('/Bookticket/getListSeatOfShowtime/' + data).then(callback);
        },
        DsGheDaDat: function (id, callback) {
            $http.get('/Bookticket/DsGheDaDat/' + id).then(callback);
        },
        GetListShowTime: function (Date, callback) {
            $http.get('/Showtimes/GetListShowtime?date=' + Date).then(callback);
        },
        BookTickets: function (data, callback) {
            $http.post('https://localhost:44350/Staffs/home/BuyTicket',data).then(callback);
        },
        //GetPrice: function (idseat, idShowTime,callback) {
        //    $http.get('/YourOrder/Price?idseat=' + idseat + '&idShowTime=' + idShowTime).then(callback);
        //}
    }
});

app.controller('index', function ($scope, datasevice) {
    $scope.selectday = function () {
        if ($scope.day != "") {
            $scope.day=$scope.day.toLocaleDateString('en-US');
        }
        datasevice.GetListShowTime($scope.day, function (rs) {
            rs = rs.data;
            if (rs != null && !rs.error) {
                console.log(rs);
                $scope.data = rs;
                $scope.$apply;
            }
            else {
                $scope.mess = rs.title;
            }
        });
    }
    $scope.day = "";
    $scope.init = function () {
        $scope.selectday();
        $('title').html("Trang chủ");
    }
    $scope.init();
});

app.controller('bookTicket', function ($scope, datasevice, $routeParams, $http) {
    $scope.idShowtime = $routeParams.id;
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

                $('title').html("Đặt vé - " +$scope.room.title);

                $scope.col = range(0, $scope.room.col - 1, 1);

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
            if ($scope.dsghedachon != undefined)
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

    $scope.listseat = [];
    $scope.addseat = function () {
        if ($scope.seat != null) {
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

    $scope.InVe = function () {

        function listid() {
            var a=[];
            $scope.listseat.forEach(function (data1) {
                a.push(data1.id);
            });
            return a;
        }

        var st = {
            id: $scope.idShowtime,
            ids: listid()
        };

        datasevice.BookTickets(st, function (rs) {
            rs = rs.data;
            console.log(rs);
            if (!rs.error) {
                $http.defaults.headers.common.Authorization = '87c21eb5-0a4a-4b31-bba8-a8593992b110'; //replace with our API key from portal.api2pdf.com

                var endpoint = "https://v2018.api2pdf.com/chrome/html"
                var payload;

                var html="";
                $scope.listseat.forEach(function (s) {
                    var model = {
                        Objects: {
                            Title: $scope.room.title,
                            Name: "-------",
                            Room: $scope.room.name,
                            Seat: {
                                X: s.x,
                                Y: s.y,
                            },
                            Time: $scope.room.time,
                            Date: $scope.room.date,
                        }
                    };
                    html += HtmlTicket(model);
                });
                console.log(html);
                ////$http.get("https://localhost:44350/staffs/Home/Ticket/11").then(function (rs) {
                ////console.log(rs);

                payload = {
                    html: html, //HtmlTicket(model),//"<p>Chó Hải dm</p>" //set your HTML here!
                    inlinePdf: true
                }


                $http.post(endpoint, payload).then(
                    function (response) {
                        //your PDF is in this response. Do something with it!
                        $scope.pdf = response.data.pdf;
                        console.log($scope.pdf)
                        $scope.$apply;

                        window.open($scope.pdf, '_blank').focus();
                        location.reload();
                    },
                    function (error) {

                    });
            }
        });
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
});

app.controller('TicketPrint', function ($scope, $uibModal, DTOptionsBuilder, DTColumnBuilder, $compile, $filter,$http) {

    var vm = $scope;
    $('title').html("Đơn hàng đã đặt");
    vm.dtOrderhOptions = DTOptionsBuilder.newOptions()
        .withOption('ajax', {
            url: "/Staffs/Home/JtableTestModel"
            , beforeSend: function (jqXHR, settings) {
                $.blockUI({
                    boxed: true,
                    message: 'loading...'
                });
            }
            , type: 'GET'
            , data: function (d) {
                d.date = !$scope.Date ? "" : $filter('date')($scope.Date, 'yyyy-MM-dd');
                d.idsearch = $scope.idsearch;
                console.log(d);
            }
            , dataType: "json"
            , complete: function (rs) {
                $.unblockUI();
                //console.log(rs);
                console.log(rs.responseJSON.data);
                if (rs && rs.responseJSON && rs.responseJSON.Error) {
                    App.toastrError(rs.responseJSON.Title);
                }
                else $scope.All = rs.responseJSON.data;
                
            }
        })
        .withPaginationType('full_numbers').withDOM("<'table-scrollable't>ip")
        .withDataProp('data').withDisplayLength(3)
        .withOption('serverSide', true)
        .withOption('headerCallback', function (header) {
            if (!$scope.headerCompiled) {
                $scope.headerCompiled = true;
                $compile(angular.element(header).contents())($scope);
            }
        })
        .withOption('initComplete', function (settings, json) {
        })
        .withOption('createdRow', function (row) {
            $compile(angular.element(row).contents())($scope);
        });

    vm.dtOrderColumns = [];

    vm.dtOrderColumns.push(DTColumnBuilder.newColumn('id', 'id').withOption('sWidth', '20px').renderWith(function (data, type) {
        return data
    }).notSortable());

    vm.dtOrderColumns.push(DTColumnBuilder.newColumn("Username", 'Username').withOption('sWidth', '10px').renderWith(function (data, type) {
        return data
    }).notSortable());

    vm.dtOrderColumns.push(DTColumnBuilder.newColumn('Objects', 'Đơn hàng của bạn').withOption('sWidth', '250px').renderWith(function (data, type, full, meta) {
        //data = JSON.parse(data);
        //console.log(data);
        //if (data.isTicket == false)
        //    return '<div my-Bill model="All[' + meta.row + ']" ></div > ';
        //return '<div my-Ticket model="' + data.Id + '" ></div > ';
        data = JSON.parse(data);
        //console.log(data);
        if (data.isTicket == false)
            return '<div my-Bill model="All[' + meta.row + ']" ></div > ';
        return '<div my-Ticket model="All[' + meta.row + ']" ></div > ';
    }).notSortable());
    vm.dtOrderColumns.push(DTColumnBuilder.newColumn('Date', 'Ngày đặt').withOption('sWidth', '40px').renderWith(function (data, type) {
        return data;
    }).notSortable());
    vm.dtOrderColumns.push(DTColumnBuilder.newColumn('id','Chức nâng').withOption('sWidth', '40px').renderWith(function (data, type,full,meta) {
        return '<button class="btn btn-success" ng-click="InVe(All[' + meta.row + '])">In vé</button>';
    }).notSortable());
    $scope.modelrt = function (id) {
        $scope.TotalPrice = $scope.List.find(x => x.Id == a.Id).TotalPrice;
    }
    $scope.init = function () {
        $("#my_order").addClass("current-menu-item");
    }
    $scope.init();
    $scope.InVe = function (model) {
        model.Objects = JSON.parse(model.Objects);
        $http.defaults.headers.common.Authorization = '87c21eb5-0a4a-4b31-bba8-a8593992b110'; //replace with our API key from portal.api2pdf.com

        var endpoint = "https://v2018.api2pdf.com/chrome/html"
        var payload;

        var html;

        if (model.Objects.isTicket) {
            html = HtmlTicket(model);
        }
        else html = HtmlBill(model);
        console.log(html);
            //$http.get("https://localhost:44350/staffs/Home/Ticket/11").then(function (rs) {
            //console.log(rs);

        payload = {
            html: html, //HtmlTicket(model),//"<p>Chó Hải dm</p>" //set your HTML here!
            inlinePdf: true
        }
       

        $http.post(endpoint, payload).then(
            function (response) {
                //your PDF is in this response. Do something with it!
                $scope.pdf = response.data.pdf;
                console.log($scope.pdf)
                $scope.$apply;

                window.open($scope.pdf, '_blank').focus();
            },
            function (error) {

            });
        }
    vm.dtInstance = {};

    vm.reloadData = function (resetPaging) {
        vm.dtInstance.reloadData(callback, resetPaging);
    }
    function callback(json) {

    }
    $scope.controller = "YourOrder";
    $scope.choseService = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: ctxfolderurl + "/chose-services.html",
            controller: "choseService",
            size: 'xl'
        });

        modalInstance.result.then(function (res) {
            //xử lý payment và cancel
        });
    }
});

app.controller('choseService', function ($scope, service,$http) {
    
    $scope.init = function () {
        service.GetAllSevice(function (rs) {
            rs = rs.data;
            console.log(rs);
            if (rs.error) {
                console.log(rs.title);
            } else {

                $scope.sevices = rs.object;

                $scope.foods = $scope.sevices.filter(a => a.isFood);
                $scope.waters = $scope.sevices.filter(a => !a.isFood);
                console.log($scope.foods);

                $scope.$apply;
            }

        });
    }
    $scope.init();

    $scope.ListBilldetail = [];

    $scope.minus = function (index) {
        if ($scope.ListBilldetail[index].amout <= 1)
            if (confirm("Bạn muốn xóa ?"))
                $scope.ListBilldetail.splice(index, 1);
            else { }
        else
            $scope.ListBilldetail[index].amout--;
        $scope.$apply;
    }
    $scope.addinList = function (index) {
        $scope.ListBilldetail[index].amout++;
        $scope.$apply;
    }
    $scope.add = function (food) {
        var item = {
            id: food.id,
            name: food.name,
            size: food.size.find(x => x.id == food.choosesize),
            amout: 1
        }
        var bool = $scope.ListBilldetail.find(x => x.id == item.id && x.size.id == food.choosesize)
        if (bool != null) {
            var a = $scope.ListBilldetail.indexOf(bool);
            $scope.ListBilldetail[a].amout++;
        }
        else {
            $scope.ListBilldetail.push(item);
        }
        console.log($scope.ListBilldetail);
        $scope.$apply;
    }
    $scope.selected = [];

    $scope.Total = function (arr) {
        var total = 0;
        arr.forEach(function (item, ind) {
            total += item.size.price * item.amout;
        });
        return total;
    }
    $scope.Successs = function () {
        var Billdetails = [];
        $scope.ListBilldetail.forEach(function (data) {
            var billdt = {
                "id": 0,
                "idSeviceSeviceCategories": data.size.id,
                "name": "",
                "amount": 1,
                "unitPrice": 0,
                "billId": 0,
                "bill": null,
                "seviceSeviceCategories": null
            }
            Billdetails.push(billdt);
        });
        service.BuyBill(Billdetails, function (rs) {
            rs = rs.data;
            if (rs.error) {
                alert(rs.title);
            } else {
                $http.defaults.headers.common.Authorization = '87c21eb5-0a4a-4b31-bba8-a8593992b110'; //replace with our API key from portal.api2pdf.com

                var endpoint = "https://v2018.api2pdf.com/chrome/html"
                var payload;

                var html = "";
                var model = {
                    Objects: JSON.parse(rs.object) 
                };
                html += HtmlBill(model);
                console.log(html);
                ////$http.get("https://localhost:44350/staffs/Home/Ticket/11").then(function (rs) {
                ////console.log(rs);

                payload = {
                    html: html, //HtmlTicket(model),//"<p>Chó Hải dm</p>" //set your HTML here!
                    inlinePdf: true
                }


                $http.post(endpoint, payload).then(
                    function (response) {
                        //your PDF is in this response. Do something with it!
                        $scope.pdf = response.data.pdf;
                        console.log($scope.pdf)
                        $scope.$apply;

                        window.open($scope.pdf, '_blank').focus();
                        location.reload();
                    },
                    function (error) {

                    });
            }
        });
    }
})


app.directive('myTicket', function () {
    function link($scope, element, attributes) {
        $scope.data = JSON.parse($scope.model.Objects);
        console.log($scope.data);
    }

    return {
        restrict: 'A',
        scope: {
            model: '='
        },
        templateUrl: '/View/front-end/your_order/Ticket.htm',
        link: link
    };
});
app.directive('myBill', function (DTColumnDefBuilder, DTOptionsBuilder) {
    function link($scope, element, attributes, DTColumnDefBuilder, DTOptionsBuilder) {
        $scope.data = JSON.parse($scope.model.Objects);
        console.log($scope.data);
    }

    return {
        restrict: 'A',
        scope: {
            model: "=",
        },
        templateUrl: '/View/front-end/your_order/Bill.html',
        link: link
    };
});
function HtmlTicket(data) {
    console.log(data);
    data = data.Objects;
    var rs = '<div style="height: 220px; width: fit-content; display:flex"><div style="height: 190px; width: 400px; background: linear-gradient(to bottom, #e84c3d 0%, #e84c3d 26%, #ecedef 26%, #ecedef 100%); border-radius: 8px; ">' +
        '<div class="row"><div class="col-sm-12"><p style="display: flex; align-items: center; height: 100%; margin-left: 15px; color: #ffffff; font-size: 20px;">' +
        'My Cinema</p></div></div><div class="row" style="height: fit-content; margin-top: 10px;"><div class="col-sm-12" style="height: 40px;">' +
        '<p style=" text-transform: uppercase;margin-bottom: 0px; height: fit-content; font-size: 15px; color: #000000; font-weight: bold; margin-left: 15px; margin-top: 5px;">' +
        data.Title +
        '</p><p style="margin-left: 15px; text-transform: uppercase; font-size: 13px;">PHIM</p></div></div><div class="row"><div class="col-sm-6">' +
        '<p style="text-transform: uppercase; margin-bottom: 0px; height: fit-content; font-size: 15px; color: #000000; font-weight: bold; margin-left: 15px; margin-top: 5px;">' +
        data.Name +
        '</p><p style="margin-left: 15px; text-transform: uppercase; font-size: 13px;margin-bottom: 5px">Người mua</p></div><div class="col-sm-6">' +
        '<p style="text-transform: uppercase; margin-bottom: 0px; height: fit-content; font-size: 15px; color: #000000; font-weight: bold; margin-left: 15px; margin-top: 5px;">' +
        data.Room +
        '</p><p style="margin-left: 15px; text-transform: uppercase; font-size: 13px;margin-bottom: 5px">Phòng</p></div></div>' +
        '<div class="row"><div class="col-2"><p style="font-size: 13px; margin-left: 15px; color: #000000; font-weight: bold; margin-bottom: 0px; ">' +
        data.Seat.X + data.Seat.Y +
        '</p><p style="font-size: 13px; margin-left: 15px;  text-transform: uppercase; ">Ghế</p></div><div class="col-5">' +
        '<p style="font-size: 13px; margin-left: 15px; color: #000000; font-weight: bold; margin-bottom: 0px; ">' +
        data.Time +
        '</p><p style="font-size: 13px; margin-left: 15px;  text-transform: uppercase; ">Giờ chiếu</p>' +
        '</div><div class="col-5"><p style="font-size: 13px; margin-left: 15px; color: #000000; font-weight: bold; margin-bottom: 0px; ">' +
        data.Date +
        '</p><p style="font-size: 13px; margin-left: 15px;  text-transform: uppercase; ">Ngày chiếu</p></div></div></div>' +
        '<div class="col-sm-4" style="background: linear-gradient(to bottom, #e84c3d 0%, #e84c3d 26%, #ecedef 26%, #ecedef 100%); border-radius: 8px;  border-left: 3px dashed #fff; height: 190px; display: flex; align-items: center; justify-content: center;">' +
        '<div class="row" style="margin-top: 20px; width: fit-content; margin-left: -20px;  height: fit-content;">' +
        '<div class="col-sm-12"><p style="margin-bottom: 0px; align-items: center; color: #e84c3d; font-size: 40px; font-weight: bold; display: flex; justify-content: center;">' +
        data.Seat.X + data.Seat.Y +
        '</p><p class="text-center" style="font-size: 20px;text-transform: uppercase; ">Số ghế</p></div></div></div></div>' +
        '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">' +
        '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js">' + '<' + '/script>' +
        '<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js">' + '<' + '/script>' +
        '<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js">' + '<' + '/script>';
        
   // console.log(rs);
    return rs;
}

function HtmlBill(data) {
    data = data.Objects;
    var rs = '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">' +
        '<h1>Hóa đơn</h1>'+
        '<div><style>.Hd td,.Hd tr,.Hd th{border:none;}</style>' +
        '<table class="Hd" style="background:white;width:100%">' +
        '<tr style="border-bottom:DarkGray dashed">' +
        '<th>Tên</th><th width="200px">Số lượng</th><th width="200px">Đơn giá</th></tr>';
    data.BillDetails.forEach(function (x, index) {
        rs += '<tr style="background:white;border:none">' +
            '<td>'+x.Name +'</td> <td>'+x.Amount+'</td><td>'+ x.UnitPrice +'</td></tr>';
    })
    rs += '</table><hr /><div><spa>Tổng tiền:  '+data.TotalPrice +'</spa></div></div>';

    return rs;
}