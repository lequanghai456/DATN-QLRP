var ctxfolderurl = "http://haile123-001-site1.ctempurl.com";

var app = angular.module('App', ['datatables', 'ngRoute', 'checklist-model']);

app.factory('dataservice', function ($http) {
    return {
       
    }
});

app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile, dataservice, $filter) {
    var vm = $scope;
    var LengthPage = 6;
   
    
    
    $scope.init = function () {
        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withOption('ajax', {
                url: "/Admin/bills/JtableBillModel"
                , beforeSend: function (jqXHR, settings) {
                    $.blockUI({
                        boxed: true,
                        message: 'loading...'
                    });
                }
                , type: 'GET'
                , data: function (d) {
                    d.Price = $scope.valuePrice;
                    d.Date = !$scope.valueDate?"":$filter('date')($scope.valueDate,'yyyy-MM-dd');
                    d.UserName = $scope.valueUserName;
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
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Id').withClass('Center').notSortable().renderWith(function (data, type) {
            return data;
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('Date', 'Ngày lập').notSortable().withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('TotalPrice', 'Tổng giá').notSortable().withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable()); 
        vm.dtColumns.push(DTColumnBuilder.newColumn('UserName', 'Tài khoản').notSortable().withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable()); 
    }
    $scope.init();
    
    
    $scope.delete = function (idDelete) {
        dataservice.deleteCategory(idDelete, function (rs) {
            rs = rs.data;
            $scope.notification = rs;
            reloadData(true);
        });
    }
    vm.reloadData = reloadData;
    vm.dtInstance = {};
    function reloadData(resetPaging) {
        vm.dtInstance.reloadData(callback, resetPaging);
    }
    function callback(json) {

    }
    $scope.Search = function () { reloadData(true) };
    vm.Rest = function () {
        vm.valueDate = "";
        vm.valuePrice = "";
        vm.valueUserName = "";
        reloadData(true);
    };
   
    
    
});