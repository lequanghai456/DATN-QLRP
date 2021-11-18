
var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ngRoute', 'checklist-model']);

app.factory('dataservice', function ($http) {
    return {
        deleteStaff: function (data, callback) {
            $http.post('/Admin/Staffs/DeleteStaff?id=' + data).then(callback);
        },
        deleteStaffCheckbox: function (data, callback) {
            $http.post('/Admin/Staffs/DeleteStaffList?Listid=' + data).then(callback);
        },
        getRole: function (callback) {
            $http.post('/Admin/Staffs/Role').then(callback);
        },
    }
});

app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile, dataservice,dataservice,) {
    var vm = $scope;
    var id = document.getElementById('idEdit');
    $scope.selected = [];
    $scope.items = [];
    $scope.selectAll = false;
    var LengthPage = 3;
    var itam = LengthPage;
    $scope.toggleOne = toggleOne;
    console.log(id);
    $scope.init = function () {
        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withOption('ajax', {
                url: "/Admin/Staffs/JtablestaffModel"
                , beforeSend: function (jqXHR, settings) {
                   
                }
                , type: 'GET'
                , data: function (d) {
                    d.FullName = $scope.valueName;
                    d.Role = $scope.valueRole;
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
        vm.dtColumns.push(DTColumnBuilder.newColumn('FullName', 'Tên đầy đủ').withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('date', 'Ngày sinh').withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('UserName', 'Tài khoản').withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('RoleName', 'Chức vụ').withClass('Center').renderWith(function (data, type) {
            return data;
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('Img', 'Ảnh').withClass('Center').renderWith(function (data, type) {

            return '<img id="imgPre" src="/admin/img/' + data + '" alt="Alternate Text" class="img-thumbnail" />';
        }).notSortable());
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Tùy chọn').withClass('Center').notSortable().withOption('searchable', false).renderWith(function (data, type) {
            return '<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/Staffs/Index/' + data + '> Cập nhật</a >|<button class="btn btn-primary" data-toggle="modal" data-target="#myModal" ng-click="delete(' + data + ')">Xóa</button>';
        }).notSortable());


        if (id != null) {
            vm.create = true;

        }
        else {
            vm.create = false;
        }
        dataservice.getRole(function (rs) {
            rs = rs.data;
            rs.unshift({
                'id': 0,
                'name': 'All'
            }
            );
            $scope.dataRole = rs;
            $scope.valueRole = rs[0].id;
            console.log($scope.valueRole);
            
        });
    }
    $scope.init();

    vm.Show = function () {
        vm.create = !vm.create;
    };
    $scope.delete = function (idDelete) {
        var flag = true;
        if (id != null) {
            if (id.value == idDelete) {
                $scope.notification = "Không thể xóa khi đang cập nhật";
                flag = false;
            }
        }
        if (flag) {
            dataservice.deleteStaff(idDelete, function (rs) {
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
    $scope.deleteStaffList = function () {
        if (id != null) {
            $scope.notification = "Không thể xóa khi đang cập nhật";
        } else {
            $("input:checkbox[name=type]:checked").each(function () {
                $scope.selected.push($(this).val());
            });
            console.log($scope.selected);
            dataservice.deleteStaffCheckbox($scope.selected, function (rs) {

                rs = rs.data;
                $scope.notification = rs;
                $scope.selected = [];
                reloadData(true);

            });
        }

    }
    

});
