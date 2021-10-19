

var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ui.bootstrap', 'ngRoute', 'checklist-model']);

app.factory('dataservice', function ($http) {
    return {
        getSeviceCategory: function (callback) {
            $http.get('/Admin/Sevices/getSeviceCategory').then(callback);
        },
        addCategory: function (data,callback) {
            $http.post('/Admin/Sevices/addCategory?name=' + data).then(callback);
        },
        addCategorySevice: function (data, callback) {
            $http.post('/Admin/Sevices/addCategorySevice', data).then(callback);
        },



        deleteSevice: function (data, callback) {
            $http.post('/Admin/Sevices/DeleteSevice?id='+data).then(callback);
        },
        deleteSeviceCheckbox: function (data, callback) {
            $http.post('/Admin/Sevices/DeleteSeviceList?Listid=' + data).then(callback);
        },
        editSevice: function (data, callback) {
            $http.post('/Admin/Sevices/editSevice?id=' + data).then(callback);
        },
        editCategorySevice: function (data, callback) {
            $http.post('/Admin/Sevices/editCategorySevice', data).then(callback);
        },
        //addCategorySevice: function (data, callback) {
        //    $http.post('/Admin/Sevices/addCategorySevice', data).then(callback);
        //},
        delCategorySevice: function (data, callback) {
            $http.post('/Admin/Sevices/delCategorySevice?id='+ data).then(callback);
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
        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withOption('ajax', {
                url: "/Admin/Sevices/JtableSeviceModel"
                , beforeSend: function (jqXHR, settings) {
                    $.blockUI({
                        boxed: true,
                        message: 'loading...'
                    });
                }
                , type: 'GET'
                , data: function (d) {
                    d.Name = $scope.valueName;
                    d.Price = $scope.valueName;
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
        vm.dtColumns.push(DTColumnBuilder.newColumn('Name', 'Tên dịch vụ').withClass('Center').renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Size', 'Kích thước - giá').withClass('Center').renderWith(function (data, type) {
            //var Name = "";
            //angular.forEach(JSON.parse(data), function (value, key) {
            //    Name += value.Name + " - " + value.price +'</br>';
            //});
            //return Name.slice(0, Name.length - 1);
            return '<input type="button" value="Thêm kích thước" class="btn btn-info" ng-click="addPrice('+1+')" style="margin: 15px;" />'
        }));

        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Tùy chọn').withClass('Center').notSortable().withOption('searchable', false).renderWith(function (data, type) {
            return '<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/Sevices/Index/' + data + '#! > Cập nhật</a >|<button class="btn btn-primary" data-toggle="modal" data-target="#myModal" ng-click="delete(' + data + ')">Xóa</button>';
        }));

        vm.addCategory = true;
        vm.editCategory = false;
        if (id != null) {
            vm.create = true;

        }
        else {
            vm.create = false;
        }
        $scope.buttonCategorySevices = "addCategorySevice()";

            if (id != null) {
                dataservice.editSevice(id.value, function (rs) {
                    $scope.seviceName = rs.data[0].name;
                    $scope.seviceIsFood = rs.data[0].IsFood;
                    rs = rs.data[0].listCategorysevice;

                    console.log(rs);
                    angular.forEach(rs, function (value, key) {
                        $scope.ListSeviecs.push({
                            "id": value.id,
                            "name": value.name,
                            "price": value.price
                        });    
                    });

                    console.log($scope.ListSeviecs);
                });
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
                    $scope.notification = "Không thể xóa khi đang cập nhật";
                    flag = false;
                }
            }
            if (flag) {
                dataservice.deleteSevice(idDelete, function (rs) {
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
        $scope.deleteSeviceList = function () {
            if (id != null) {
                $scope.notification = "Không thể xóa khi đang cập nhật";
            } else {
                $("input:checkbox[name=type]:checked").each(function () {
                    $scope.selected.push($(this).val());
                });
                console.log($scope.selected);
                dataservice.deleteSeviceCheckbox($scope.selected, function (rs) {

                    rs = rs.data;
                    $scope.notification = rs;
                    $scope.selected = [];
                    reloadData(true);

                });
            }

        }
        $scope.ListSeviecs = [];
        //Danh sách loại
        $scope.addCategorySevice = function () {
            if (($scope.name == null || $scope.price == null || $scope.name == "")) {
                $scope.Mess = "Tên và giá không được bỏ trống";
            }
            else {
                if (id != null) {
                    $scope.categorySevice.name = $scope.name;
                    $scope.categorySevice.price = $scope.price;
                    $scope.categorySevice.id = $scope.idCategorySevice;
                    $scope.categorySevice.idSevice = id.value;
                    dataservice.addCategorySevice($scope.categorySevice, function (rs) {
                        if (rs.data != 0) {
                            $scope.ListSeviecs.push({
                                "id": rs.data,
                                "name": $scope.name,
                                "price": $scope.price
                            });
                            $scope.Mess = null;
                            $scope.name = null
                            $scope.price = null;
                            reloadData(true);
                        }
                        else {
                            $scope.Mess = "Thêm thất bại";
                        }

                    });
                } else {
                    if ($scope.itam == null) {
                        $scope.ListSeviecs.push({
                            "name": $scope.name,
                            "price": $scope.price
                        });
                        $scope.Mess = null;
                        $scope.name = null
                        $scope.price = null;
                        console.log($scope.ListSeviecs);
                    }
                    else {
                        $scope.ListSeviecs[$scope.itam].name = $scope.name;
                        $scope.ListSeviecs[$scope.itam].price = $scope.price;
                        $scope.Mess = null;
                        $scope.name = null;
                        $scope.price = null;
                        $scope.itam = null;
                        vm.addCategory = true;
                        vm.editCategory = false;
                        console.log($scope.ListSeviecs);
                        $scope.addCategoryButton = "Thêm loại";
                    }
                }
            }
        }
        $scope.categorySevice = {
            id: "",
            name: "",
            price: "",
            idSevice: "",

        }
        $scope.editCategorySevice = function () {
            $scope.categorySevice.name = $scope.name;
            $scope.categorySevice.price = $scope.price;
            $scope.categorySevice.id = $scope.idCategorySevice;
                dataservice.editCategorySevice($scope.categorySevice, function (rs) {
                rs = rs.data;
                if (rs) {
                    $scope.ListSeviecs[$scope.itam].name = $scope.name;
                    $scope.ListSeviecs[$scope.itam].price = $scope.price;
                    $scope.Mess = null;
                    $scope.name = null;
                    $scope.price = null;
                    $scope.itam = null;
                    vm.addCategory = true;
                    vm.editCategory = false;
                    console.log($scope.ListSeviecs);
                    $scope.addCategoryButton = "Thêm loại";
                    $scope.Mess = "Cập nhật thành công";
                    reloadData(true);
                }
                else {
                    $scope.Mess = "Cập nhật thất bại";
                }

            });
        }
        $scope.delCategorySevice = function (id, index) {
            dataservice.delCategorySevice(id, function (rs) {
                if (rs.data) {
                    $scope.ListSeviecs.splice(index, 1);
                    $scope.$apply;
                    console.log($scope.ListSeviecs);
                    $scope.name = "";
                    $scope.price = "";
                    reloadData(true);
                }
                else {
                    $scope.Mess = "Có lỗi xảy ra không thể xóa đối tượng này";
                }
            });
        }
        $scope.cancelCategorySevice = function () {
            $scope.Mess = null;
            $scope.name = null;
            $scope.price = null;
            $scope.itam = null;
            vm.addCategory = true;
            vm.editCategory = false;
            $scope.addCategoryButton = "Thêm loại";
        }
        $scope.action = {
            del: function (index) {
                if ($scope.itam == null) {
                    $scope.ListSeviecs.splice(index, 1);
                    $scope.$apply;
                    console.log($scope.ListSeviecs);
                    $scope.name = "";
                    $scope.price = "";
                }
                else {
                    $scope.Mess = "Không thể xóa khi đang sử dụng tính năng cập nhật"
                }


            },
            edit: function (index) {
                $scope.name = $scope.ListSeviecs[index].name;
                $scope.price = $scope.ListSeviecs[index].price;
                $scope.idCategorySevice = $scope.ListSeviecs[index].id;
                $scope.itam = index;
                vm.addCategory = false;
                vm.editCategory = true;
                $scope.addCategoryButton = "Cập nhật";
                console.log($scope.ListSeviecs);
            },
            //add: function () {
            //    if ($scope.selected > 0) {
            //        $scope.LitSeviecs.push($scope.selected);
            //        $scope.selected = '-1';
            //        $scope.$apply;
            //    }
            //},



        }
    $scope.model = {
        
        idSevice: "",
        price: "",
        idSeviceCategory: "",

    }
    $scope.addPrice = function (id) {

        $scope.model.idsevice = id;
            var modalInstance = $uibModal.open({
                templateUrl: "/View/admin/Sevices/addPrice.html",
                controller: "ModalContent",
                size: 'lg',
                scope: $scope,
                windowClass: 'show',
            });


        };
    
});
app.controller('ModalContent', function ($scope, $uibModalInstance, dataservice, $uibModal) {
    $scope.initModal = function () {
        dataservice.getSeviceCategory(function (rs) {
            rs = rs.data;
            console.log(rs);
            $scope.sevicesCategories = rs.object;
        });
    }
    $scope.initModal();
    $scope.addSize = function () {
        var modalInstance = $uibModal.open({
            templateUrl: "/View/admin/Sevices/addSize.html",
            controller: "ModalContentSize",
            size: '',
            scope: $scope,
            windowClass: 'show',
        });


    };
    $scope.ok = function () {
        console.log($scope.model);
        dataservice.addCategorySevice($scope.model, function (rs) {
            console.log(rs.data);
            rs = rs.data;
            if (rs.error) {
                $scope.messApiPrice == rs.title;
            }
            else {
                $scope.initModal();
                $uibModalInstance.close("Ok");
            }

        });

    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    }

});

app.controller('ModalContentSize', function ($scope, $uibModalInstance, dataservice) {
    $scope.addSeviceCategory = function () {
        dataservice.addCategory($scope.nameSize, function (rs) {
            rs = rs.data;
            console.log(rs);
            if (rs.error) {
                $scope.messApi = rs.title;
            } else {
                $scope.initModal();
                $uibModalInstance.close("Ok");
            }
        });

    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    }

});
