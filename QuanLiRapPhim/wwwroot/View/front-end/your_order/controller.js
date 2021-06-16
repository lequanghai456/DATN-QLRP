var ctxfolderurl = "/View/front-end/your_order";

var app = angular.module('App', ['ui.bootstrap', 'ngRoute', 'ngAnimate']);

app.controller('Ctroller', function () {

});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
});

app.controller('index', function ($scope, $uibModal) {
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