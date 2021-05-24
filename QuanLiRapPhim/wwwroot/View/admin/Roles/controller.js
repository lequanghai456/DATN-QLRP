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

app.controller('index', function ($scope, $uibModal, $rootScope) {
    $rootScope.validate = {
        rules: {
            Name: {
                required: true
            }

        },
        message: {
            Name: {
                required: "Không được bỏ trống tên chức vụ"
            }
        }
    }
    $scope.show = function () {
        $scope.modalInstance = $uibModal.open({
            templateUrl: '/Admin/Roles/Create',
            controller: 'create',
            backdrop: 'static',
            size: '50'
        });


    }
    
});
app.controller('create', function ($scope, $uibModal, $uibModalInstance) {
    $scope.cancel = function () {
        $uibModalInstance.close('cancel');
    }
    $scope.Create = function () {

    }

});