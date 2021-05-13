var ctxfolderurl = "/View/front-end/your_order";

var app = angular.module('App', ['ngRoute']);

app.controller('Ctroller', function () {

});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
});

app.controller('index', function ($scope) {
    $scope.init = function () {
        $("#my_order").addClass("current-menu-item");
    }
    $scope.init();

});