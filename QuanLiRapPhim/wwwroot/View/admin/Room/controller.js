

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
        deleteRoom: function (data, callback) {
            $http.post('/Admin/Rooms/DeleteRoom?id='+data).then(callback);
        },
    }
});

app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile, dataservice) {
    var vm = $scope;
    var id = document.getElementById('idEdit');
    $scope.selected = [];
    $scope.items = [];
    $scope.selectAll = false;
    var LengthPage = 3;
    var itam = LengthPage;
    $scope.toggleOne = toggleOne;
   
    
    
    $scope.init = function () {
        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withOption('ajax', {
                url: "/Admin/Rooms/JtableRoomModel"
                , beforeSend: function (jqXHR, settings) {
                    $.blockUI({
                        boxed: true,
                        message: 'loading...'
                    });
                }
                , type: 'GET'
                , data: function (d) {
                    d.NameRoom = $scope.valueName;
                }
               
                , dataType: "json"
                , complete: function (rs) {
                    $.unblockUI();
                    
                    if (rs && rs.responseJSON && rs.responseJSON.Error) {
                        App.toastrError(rs.responseJSON.Title);
                    }
                }
            })
            .withPaginationType('full_numbers').withDOM("<'table-scrollable't>ip")
            .withDataProp('data').withDisplayLength(LengthPage)
            .withOption('order', [1, 'desc'])
            .withOption('serverSide', true)
           
            .withOption('headerCallback', function (header) {
                if (!$scope.headerCompiled) {
                    $scope.headerCompiled = true;
                    $compile(angular.element(header).contents())($scope);
                }
            })
            .withOption('initComplete', function (settings, json) {
            })
            .withOption('createdRow', function (row) {
                $compile(angular.element(row).contents())($scope);
            });

        vm.dtColumns = [];
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Id').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Name', 'Name').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Row', 'Row').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Col', 'Col').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('FullName', 'Staff').renderWith(function (data, type) {
            
            return data;
        }));
        var titleHtml = '<input ng-model="selectAll" name="typeOption" type="checkbox" "/>';
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Option').notSortable().withOption('searchable', false).renderWith(function (data, type) {
            return '<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/Rooms/Index/' + data + '#! > Edit</a >|<button class="btn btn-primary" ng-click="delete('+data+')">Delete</button>';
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', titleHtml).notSortable().withOption('searchable', false).renderWith(function (data, type) {
            $("input:checkbox[name=type]:checked").removeAttr('checked');
            return '<input id="checkbox" value=' + data + ' ng-checked="selectAll" name="type" type="checkbox" ng-click="toggleOne(' + data + ',$event)">';
        }));
       
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
    };
    $scope.delete = function (idDelete) {
        dataservice.deleteRoom(idDelete, function (rs) {
            rs = rs.data;
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
    function toggleOne(item, $event) {
        if (angular.element($event.currentTarget).prop('checked')) {
            angular.element($event.currentTarget).attr("checked", "true");
            
        } else

            angular.element($event.currentTarget).removeAttr("checked");
    }
    $scope.deleteAll = function () {
        $("input:checkbox[name=type]:checked").each(function () {
            $scope.selected.push($(this).val());
        });
        console.log($scope.selected);
        $scope.selected = [];
    }
    
    
});