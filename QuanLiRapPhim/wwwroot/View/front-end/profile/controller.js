var ctxfolderurl = "/View/front-end/profile";

var app = angular.module('App', ['ngRoute']);

app.controller('Ctroller', function ($scope) {
    $scope.init = function () {
        $scope.EditPassword = false;
    }
    $scope.init();
    $scope.EditPass = function () {
        $scope.EditPassword = !$scope.EditPassword;
    };
    
});



