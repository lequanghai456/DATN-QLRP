var ctxfolderurl = "/View/Admin/Roles";

var app = angular.module('App', ['ngRoute','ui.bootstrap']);

app.controller('Ctroller', function () {

});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
});

app.controller('index', function ($scope,$uibModal) {
    //$scope.init = function () {
     
    //}
    //$scope.init();
    $scope.show = function () {
        $scope.modalInstance = $uibModal.open({
            ariaLabelledBy: "modal-title",
            ariaDescribedBy: "modal-body",
            templateUrl: '/Admin/Roles/Create',
            controller: 'index',
            backdrop: 'static',
            size: '50'
        });

    }
    
});