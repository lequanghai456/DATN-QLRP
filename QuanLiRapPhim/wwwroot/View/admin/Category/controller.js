var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ngRoute', 'checklist-model']);

app.factory('dataservice', function ($http) {
    return {
        deleteCategory: function (data, callback) {
            $http.post('/Admin/Categories/DeleteCategories?id='+data).then(callback);
        },
        deleteCategoryCheckbox: function (data, callback) {
            $http.post('/Admin/Categories/DeleteCategoriesAll?Listid=' + data).then(callback);
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
                url: "/Admin/Categories/JtableCategoryModel"
                , beforeSend: function (jqXHR, settings) {
                    $.blockUI({
                        boxed: true,
                        message: 'loading...'
                    });
                }
                , type: 'GET'
                , data: function (d) {
                    d.Title = $scope.valueTitle;
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
        var titleHtml = '<input ng-model="selectAll" name="typeOption" type="checkbox" "/>';
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', titleHtml).withClass('Center').notSortable().withOption('searchable', false).renderWith(function (data, type) {
            $("input:checkbox[name=type]:checked").removeAttr('checked');
            return '<input id="checkbox" style="margin: 0 auto;" value=' + data + ' ng-checked="selectAll" name="type" type="checkbox" ng-click="toggleOne(' + data + ',$event)">';
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Id').withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('Title', 'Tên thể loại').withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Tùy chọn').withClass('Center').notSortable().withOption('searchable', false).renderWith(function (data, type) {
            return '<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/Categories/Index/' + data + '#! > Cập nhật</a >|<button class="btn btn-primary" data-toggle="modal" data-target="#myModal" ng-click="delete('+data+')">Xóa</button>';
        }).notSortable());
        
       
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
        var flag = true;
        if (id != null) {
            if (id.value == idDelete) {
                $scope.notification = "Không thể xóa khi đang sử dụng chức năng edit";
                flag = false;
            }
        }
        if (flag) {
            dataservice.deleteCategory(idDelete, function (rs) {
                rs = rs.data;
                $scope.notification = rs;
                reloadData(true);
            });
        }
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
    $scope.deleteCategoryList = function () {
        if (id != null) {
            $scope.notification = "This feature cannot be used while editing";
        } else {
            $("input:checkbox[name=type]:checked").each(function () {
                $scope.selected.push($(this).val());
            });
            console.log($scope.selected);
            dataservice.deleteCategoryCheckbox($scope.selected, function (rs) {

                rs = rs.data;
                $scope.notification = rs;
                $scope.selected = [];
                reloadData(true);

            });

        }
    }
    
    
});