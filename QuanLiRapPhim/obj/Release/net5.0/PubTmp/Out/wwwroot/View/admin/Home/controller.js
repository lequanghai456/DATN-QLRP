
var app = angular.module('App', ['ngRoute']);

app.factory("Thongke", function ($http) {
    return {
        ThongKeTheoNgay: function (callback) {
            $http.post('/Admin/Home/ThongKeTheoNgay').then(callback);
        }, ThongKeTheoThang: function (callback) {
            $http.post('/Admin/Home/ThongKeTheoThang').then(callback);
        }, ThongKeTheoQuy: function (callback) {
            $http.post('/Admin/Home/ThongKeTheoQuy').then(callback);
        },
    }

})

app.controller('Ctroller', function ($scope, Thongke) {
    $scope.init = function () {
        $scope.EditPassword = false;
        $scope.capnhatmatkhau = "capnhatprofile";
        $scope.xacnhancapnhatmatkhau = "capnhatprofile";
    }
    $scope.init();
    $scope.Thongke = function () {
        Thongke.ThongKeTheoNgay(function (rs) {
            rs = rs.data;
            $scope.TheoNgay = rs;
        });
        Thongke.ThongKeTheoThang(function (rs) {
            rs = rs.data;
            $scope.TheoThang = rs;
        });
        Thongke.ThongKeTheoQuy(function (rs) {
            rs = rs.data;
            $scope.TheoQuy = rs;
        });
    }
    $scope.EditPass = function () {
        $scope.EditPassword = !$scope.EditPassword;
        if ($scope.EditPassword == false) {
            $scope.capnhatmatkhau = "capnhatprofile";
            $scope.xacnhancapnhatmatkhau = "capnhatprofile";
        } else
            $scope.capnhatmatkhau = null;
        $scope.xacnhancapnhatmatkhau = null;
    };

});