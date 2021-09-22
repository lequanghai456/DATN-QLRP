var ctxfolderurl = "/View/front-end/showtimes";

var app = angular.module('App', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
});
app.factory('dataservice', function ($http) {
    var headers = {
    "Content-Type": "application/json;odata=verbose",
    "Accept": "application/json;odata=verbose",
    }

    return {
        GetListShowTime: function (id, Date, callback) {
            $http.get('/moviereview/GetListShowTime?idmovie=' + id + '&date=' + Date).then(callback);
        },
    }
});

app.controller('Ctroller', function ($scope) {
    $scope.init = function () {
        $("#showtimes").addClass("current-menu-item");
    }
    $scope.init();
});
app.controller('index', function () {

});