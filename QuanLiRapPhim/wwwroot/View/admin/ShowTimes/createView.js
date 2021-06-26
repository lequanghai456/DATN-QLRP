var app = angular.module('App', []);

app.factory('dataservice', function ($http) {
    return {
        getListMovie: function (callback) {
            $http.get('/Admin/ShowTimes/ListMovie').then(callback);
        }
    }
});

app.controller('Ctroller', function ($scope, dataservice) {
    $scope.ListMovie = [];
    $scope.init = function () {
        dataservice.getListMovie(function (rs) {
            rs = rs.data;
            $scope.List = rs;
            console.log(rs);           
        });
    }

    $scope.del = function (index) {
        $scope.ListMovie.splice(index, 1);
        $scope.$apply;
    }

    var item = $scope.selected;

    $scope.init();
    $scope.total = $scope.ListMovie.length;

    $scope.add = function () {
        if ($scope.selected > 0) {
            $scope.ListMovie.push({
                id: $scope.total,
                data: $scope.selected,
                title: $scope.List.find(x => x.id == $scope.selected).title
            });
            $scope.total += 1;
            if ($scope.time == null) {
                $scope.time = 0;
            }
            $scope.selected = "-1";
            $scope.$apply;
        }
    }

});