var ctxfolderurl = "/View/font-end/Index";

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
    $scope.mvs = [];
    for (var i = 0; i < 7; i++){
        $scope.mvs[i] = {
            id: i,
            img: 'https://www.galaxycine.vn/media/c/o/cogai_2.jpg',
            trailer: 'https://www.youtube.com/embed/LggaymnzDjc'
        };

    }
    $scope.play = function (stt) {
        $scope.trailer = $scope.mvs[stt].trailer;
    }
});