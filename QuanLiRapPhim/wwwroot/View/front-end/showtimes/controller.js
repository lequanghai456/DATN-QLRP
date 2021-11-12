var ctxfolderurl = "/View/front-end/showtimes";

var app = angular.module('App', ['ngRoute']);

app.factory('dataservice', function ($http) {
    var headers = {
    "Content-Type": "application/json;odata=verbose",
    "Accept": "application/json;odata=verbose",
    }

    return {
        GetListShowTime: function (Date, callback) {
            $http.get('/Showtimes/GetListShowtime?date=' + Date).then(callback);
        },
    }
});

app.controller('Ctroller', function ($scope, dataservice) {
    $(".listday .day").click(function () {
        $(".listday .current").removeClass("current");
        $(this).addClass("current");
    });
    $scope.GetDate = function (date) {
        dataservice.GetListShowTime(date, function (rs) {
            $scope.data = null;
            $scope.mess = null;
            rs = rs.data;
                console.log(rs);
                $scope.data = rs;
                $scope.mess = rs.title;
            $scope.$apply;
        });
    }
    $scope.init = function () {
        $("#showtimes").addClass("current-menu-item");
        $scope.GetDate();
    }
    $scope.init();
    
});