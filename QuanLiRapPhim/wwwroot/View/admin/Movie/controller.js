var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ngRoute', 'ui.bootstrap']);

app.factory('dataservice', function ($http) {
    return {
        deleteMovie: function (data, callback) {
            $http.get('/Admin/Movies/Kiemtradelete/' + data).then(function (rs) {
                console.log(rs);
                if (!rs.data)
                    $http.post('/Admin/Movies/DeleteMovie?id=' + data).then(callback);
                else
                    $(".modal-body .alert").html("Phim có lịch chiếu");
            });
        },
        deleteMovieCheckbox: function (data, callback) {
            $http.post('/Admin/Movies/DeleteMovieList?Listid=' + data).then(callback);
        },
        
        CreateAPI: function (data, callback) {
            $http.post('/Admin/Categories/CreateAPI?name=' + data).then(callback);
        },
        ListCategories: function (callback) {
            $http.post('/Admin/Categories/ListCategories').then(callback);
        },
    }
});

app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile, dataservice, $uibModal) {

    var vm = $scope;
    var id = document.getElementById('idEdit');
    
    $scope.selected = [];
    
    $scope.items = [];
    $scope.selectAll = false;
    var LengthPage = 3;
    var itam = LengthPage;
    $scope.toggleOne = toggleOne;



    $scope.init = function () {
        $('title').html("Quản lý phim");
        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withOption('ajax', {
                url: "/Admin/Movies/JtableMovieModel"
                , beforeSend: function (jqXHR, settings) {
                    $.blockUI({
                        boxed: true,
                        message: 'loading...'
                    });
                }
                , type: 'GET'
                , data: function (d) {
                    d.Title = $scope.valueName;
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
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Id').withClass('Center').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Title', 'Tên phim').withClass('Center').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('category', 'Thể loại').withClass('Center').renderWith(function (data, type) {
            var category = "";
            angular.forEach(JSON.parse(data), function (value, key) {
                category += value.Title + "/";
            });
            return category.slice(0, category.length-1);
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('mac', 'Mac').withClass('Center').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Describe', 'Mô tả').withClass('Center').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Poster', 'Ảnh').withClass('Center').renderWith(function (data, type) {
            return '<img id="imgPre" src="/admin/img/Poster/' + data + '" alt="Alternate Text" style="width:240px; height:250px;"/>';
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Tùy chọn').withClass('Center').notSortable().withOption('searchable', false).renderWith(function (data, type) {
            return '<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/Movies/Index/' + data + '> Cập nhật</a ><button class="btn btn-primary" data-toggle="modal" data-target="#myModal" ng-click="delete(' + data + ')">Xóa</button>';
        }));


        if (id != null) {
            vm.create = true;

        }
        else {
            vm.create = false;
        }
        $scope.LoadCategories();
    }
    $scope.LoadCategories = function () {
        dataservice.ListCategories(function (rs) {
            rs = rs.data;
            console.log(rs);
            $scope.categories = rs.object;
        });
    }

    $scope.init();
        vm.Show = function () {
        vm.create = !vm.create;
        if (vm.create) {
            $scope.LoadCategories();
        }
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
            dataservice.deleteMovie(idDelete, function (rs) {
                rs = rs.data;
                $scope.notification = rs;
                $scope.$apply;
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
    $scope.deleteMovieList = function () {
        if (id != null) {
            $scope.notification = "Không thể xóa khi đang cập nhật";
        } else {
            $("input:checkbox[name=type]:checked").each(function () {
                $scope.selected.push($(this).val());
            });
            console.log($scope.selected);
            dataservice.deleteMovieCheckbox($scope.selected, function (rs) {

                rs = rs.data;
                $scope.notification = rs;
                $scope.selected = [];
                reloadData(true);

            });

        }
    }
    
    $scope.open = function () {
        var modalInstance = $uibModal.open({
            templateUrl: "/View/admin/Movie/addCategories.html",
            controller: "ModalContent",
            size: '',
            scope: $scope,
            windowClass: 'show',
        });


    };
    $scope.Checkecategory = function (id) {

        var a = $scope.Lstcategori.find(function (data) {
            return data == id;
        });

        return a == undefined;
    }
});
app.controller('ModalContent', function ($scope, $uibModalInstance, dataservice) {
    $scope.ok = function () {
        dataservice.CreateAPI($scope.nameCategory, function (rs) {
            console.log(rs.data);
            if (rs.data == '0') {
                $scope.messApi = "Tạo không thành công";
                $scope.$apply;
            }
            else
            {
                $scope.LoadCategories();
                $uibModalInstance.close("Ok");
            }
            
        });
        
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    }

});