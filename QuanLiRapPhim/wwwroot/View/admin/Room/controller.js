var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ngRoute']);
app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            controller: 'Ctroller'
        })
      
});
app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile) {
    var vm = $scope;
    var id = document.getElementById('idEdit');
   
    $scope.init = function () {
        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withOption('ajax', {
                url: "/Admin/Rooms/JsonRoom"
                , beforeSend: function (jqXHR, settings) {

                    $.blockUI({
                        target: "#contentMain",
                        boxed: true,
                        message: 'loading...'
                    });
                }
                , type: 'GET'

                , complete: function (data) {
                    $.unblockUI("#contentMain");
                  
                }
            })
            .withPaginationType('full_numbers')

            .withDataProp('data')
            .withDisplayLength(10)

        vm.dtColumns = [];
        vm.dtColumns.push(DTColumnBuilder.newColumn('id', 'Id').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('name', 'Name').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('row', 'Row').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('col', 'Col').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('staff.fullName', 'Staff').renderWith(function (data, type) {
            return data;
        }));
       
        vm.dtColumns.push(DTColumnBuilder.newColumn('id', 'Option').notSortable().withOption('searchable', false).renderWith(function (data, type) {
            return '<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/Rooms/Index/' + data + '#! > Edit</a >|<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/Rooms/Index/' + data +'#! > Delete</a >';
        }));
        //vm.reloadData = reloadData;
        //vm.dtInstance = {};

        //function reloadData(resetPaging) {
        //    vm.dtEnglishInstance.reloadData(callback, resetPaging);
        //}
        //function callback(json) {

        //}
        if (id != null) {
            vm.create = true;
            
        }
        else {
            vm.create = false;
        }
        
    }
    $scope.init();

    vm.Show = function () {
        vm.create = !vm.create;
    }
   

});