var ctxfolderurl = "/View/front-end/your_order";

var app = angular.module('App', ['ui.bootstrap', 'ngRoute', 'ngAnimate','datatables']);

app.controller('Ctroller', function () {

});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
});

app.controller('index', function ($scope, $uibModal, DTOptionsBuilder, DTColumnBuilder, $compile) {
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
            //, contentType: "application/json; charset=utf-8"
            , dataType: "json"
            , complete: function (rs) {
                $.unblockUI();
                console.log(rs);
                if (rs && rs.responseJSON && rs.responseJSON.Error) {
                    App.toastrError(rs.responseJSON.Title);
                }
            }
        })
        .withPaginationType('full_numbers').withDOM("<'table-scrollable't>ip")
        .withDataProp('data').withDisplayLength(3)
        .withOption('order', [1, 'desc'])
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
    }));
    vm.dtOrderColumns.push(DTColumnBuilder.newColumn('Objects', 'Your Order').withOption('sWidth', '100px').renderWith(function (data, type) {
        return $scope.render(data);
    }));
    vm.dtOrderColumns.push(DTColumnBuilder.newColumn('Date', 'Date').withOption('sWidth', '60px').renderWith(function (data, type) {
        return data;
    }));


    $scope.render = function (data) {
        var a= JSON.parse(data);
        return data;
    }


    $scope.init = function () {
        $("#my_order").addClass("current-menu-item");
    }
    $scope.init();
    $scope.choseService = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: ctxfolderurl + "/chose-service.html",
            controller: "choseService",
            size: 'xl'
        });

        modalInstance.result.then(function (res) {
            //xử lý payment và cancel
        });
    }
});
app.controller('choseService', function ($scope, $uibModalInstance, $uibModal) {
    $scope.init = function () {
        $scope.sevices = [];
        for (var i = 0; i < 6; i++) {
            $scope.sevices[i] = {
                id: i,
                kind: 1,
                title: "food " + i,
                price: i * 1000,
            }
        }
        for (var i = 6; i < 15; i++) {
            $scope.sevices[i] = {
                id: i,
                kind: 2,
                title: "water " + i,
                price: i * 1000,
            }
        }
        $scope.foods = $scope.sevices.filter(a => a.kind==1);
        $scope.waters = $scope.sevices.filter(a => a.kind == 2);
    }
    $scope.init();


    $scope.minus = function (id) {
        var food = $scope.foods.findIndex(a => a.id == id);
        if (food != null && $scope.foods[food].amout > 0) {
            if ($scope.foods[food].amout == 1) {
                if (confirm("Bạn muốn xoá dịch vụ này?")) {
                    $scope.foods[food].select = false;
                    $scope.foods[food].amout = 0;
                }
            }
            else
            $scope.foods[food].amout -= 1;
        }
    }
    $scope.add = function (id) {
        var food = $scope.foods.findIndex(a => a.id == id);
        if (food != null) {
            $scope.foods[food].amout += 1;
        }
    }
    $scope.selected = [];

    $scope.cancel = function () {
        console.log($scope.foods.filter(a => a.select));
        $uibModalInstance.close('cancel');
    }

    $scope.payment = function () {
        $uibModalInstance.close('ok');
    }

});