var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ngRoute', 'checklist-model']);
app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            
            controller: 'Ctroller'
        })
      
});
app.factory('dataservice', function ($http) {
    return {
       
    }
});

app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile, dataservice) {
    
    
    
});