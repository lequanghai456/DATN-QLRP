var ctxfolderurl = "/View/front-end/about";

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
        $("#about").addClass("current-menu-item");
    }
    $scope.init();
    
});