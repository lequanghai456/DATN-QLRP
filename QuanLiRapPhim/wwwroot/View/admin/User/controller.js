﻿var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ngRoute', 'checklist-model']);
app.factory('dataservice', function ($http) {
    return {
       
    }
});

app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile, dataservice) {
    var vm = $scope;
    var LengthPage = 3;
   
    
    
    $scope.init = function () {
        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withOption('ajax', {
                url: "/Admin/Users/JtableUserModel"
                , beforeSend: function (jqXHR, settings) {
                    $.blockUI({
                        boxed: true,
                        message: 'loading...'
                    });
                }
                , type: 'GET'
                , data: function (d) {
                    d.FullName = $scope.valueName;
                    d.UserName = $scope.valueName;
                }
               
                , dataType: "json"
                , complete: function (rs) {
                    $.unblockUI();
                    console.log(rs.responseJSON);
                    if (rs && rs.responseJSON && rs.responseJSON.Error) {
                        App.toastrError(rs.responseJSON.Title);
                    }
                }
            })
            .withPaginationType('full_numbers').withDOM("<'table-scrollable't>ip")
            .withDataProp('data').withDisplayLength(LengthPage)
            
            .withOption('serverSide', true)
           
            .withOption('headerCallback', function (header) {
                if (!$scope.headerCompiled) {
                    $scope.headerCompiled = true;
                    $compile(angular.element(header).contents())($scope);
                }
            })
            .withOption('responsive', true)
            .withOption('initComplete', function (settings, json) {
            })
            .withOption('createdRow', function (row) {
                $compile(angular.element(row).contents())($scope);
            });

        vm.dtColumns = [];
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Id').withClass('Center').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('FullName', 'FullName').withClass('Center').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('UserName', 'UserName').withClass('Center').renderWith(function (data, type) {
            return data;
        })); 
        vm.dtColumns.push(DTColumnBuilder.newColumn('Img', 'Img').withClass('Center').renderWith(function (data, type) {
            
            return '<img id="imgPre" src="../admin/img/' + data + '" alt="Alternate Text" class="img-thumbnail" />';
        }));
    }
    $scope.init();
    vm.reloadData = reloadData;
    vm.dtInstance = {};
    function reloadData(resetPaging) {
        vm.dtInstance.reloadData(callback, resetPaging);
    }
    function callback(json) {

    }
    $scope.Search = function () { reloadData(true) };
  
   
    
    
});