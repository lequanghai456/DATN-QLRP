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
            alert(res);
        });
    }
});
app.controller('choseService', function ($scope, $uibModalInstance, $uibModal) {

    $scope.cancel = function () {
        $uibModalInstance.close('cancel');
    }

    $scope.payment = function () {
        $uibModalInstance.close('ok');
    }

});