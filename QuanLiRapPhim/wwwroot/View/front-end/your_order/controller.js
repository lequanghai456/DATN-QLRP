var ctxfolderurl = "/View/front-end/your_order";

var app = angular.module('App', ['ui.bootstrap', 'ngRoute', 'ngAnimate','datatables']);

app.controller('Ctroller', function () {

});
app.factory("datasevice", function ($http) {
    return {
        
    }
})
app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
});

app.directive('myTicket', function () {
    function link($scope, element, attributes) {
        $scope.data = JSON.parse($scope.model.Objects);
        console.log($scope.data);
    }

    return {
        restrict: 'A',
        scope: {
            model:'='
        },
        templateUrl: ctxfolderurl + '/Tickets.html',
        link:link
    };
});
app.directive('myBill', function (DTColumnDefBuilder, DTOptionsBuilder) {
    function link($scope, element, attributes, DTColumnDefBuilder, DTOptionsBuilder) {
        $scope.data = JSON.parse($scope.model.Objects);
        
    }

    return {
        restrict: 'A',
        scope: {
            model: "=",
            
        },
        templateUrl: ctxfolderurl + '/Bill.html',
        link: link
    };
});

app.controller('index', function ($scope, $uibModal, DTOptionsBuilder, DTColumnBuilder, $compile, datasevice) {
    
    var vm = $scope;

    vm.dtOrderhOptions = DTOptionsBuilder.newOptions()
        .withOption('ajax', {
            url: "/YourOrder/JtableTestModel"
            , beforeSend: function (jqXHR, settings) {
                $.blockUI({
                    boxed: true,
                    message: 'loading...'
                });
            }
            , type: 'GET'
            , dataType: "json"
            , complete: function (rs) {
                $.unblockUI();
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
    vm.dtOrderColumns.push(DTColumnBuilder.newColumn('Objects', 'Đơn hàng của bạn').withOption('sWidth', '320px').renderWith(function (data, type,full,meta) {
        data = JSON.parse(data);
        console.log(data);
        if (data.Date)
            return '<div my-Bill model="All[' + meta.row + ']" ></div > ';
        return '<div my-Ticket model="All[' + meta.row + ']" ></div > ';
    }).notSortable());
    vm.dtOrderColumns.push(DTColumnBuilder.newColumn('Date', 'Ngày đặt').withOption('sWidth', '40px').renderWith(function (data, type) {
        return data;
    }).notSortable());
    $scope.modelrt = function (id) {
        $scope.TotalPrice = $scope.List.find(x => x.Id == a.Id).TotalPrice;
    }
    $scope.init = function () {
        $("#my_order").addClass("current-menu-item");
    }
    $scope.init();

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
