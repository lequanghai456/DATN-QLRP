

var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ui.bootstrap', 'ngRoute', 'checklist-model']);

app.factory('dataservice', function ($http) {

    return {
        getSeviceCategory: function (data,callback) {
            $http.get('/Admin/Sevices/getSeviceCategory/'+data).then(callback);
        },
        getSeviceCategoryUpdate: function (data, callback) {
            $http.get('/Admin/Sevices/getSeviceCategoryUpdate/' + data).then(callback);
        },
        addCategory: function (data,callback) {
            $http.post('/Admin/Sevices/addCategory?name=' + data).then(callback);
        },
        addCategorySevice: function (idSevice, price, idSeviceCategory, callback) {
            $http.get('/Admin/Sevices/addCategorySevice?idSevice=' + idSevice + '&price=' + price + '&idSeviceCategory=' + idSeviceCategory).then(callback);
        },
        checkCategory: function (data, callback) {
            $http.post('/Admin/Sevices/checkCategory?name=' + data).then(callback);
        },
        updateCategorySevice: function (idSevice, price, idSeviceCategory, callback) {
            $http.get('/Admin/Sevices/updateCategorySevice?idSevice=' + idSevice + '&price=' + price + '&idSeviceCategory=' + idSeviceCategory).then(callback);
        },
        deleteSeviceCategory: function (data, callback) {
            $http.post('/Admin/Sevices/deleteSeviceCategory?id=' + data).then(callback);
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
            var Name = "";
            console.log(data);
            angular.forEach(JSON.parse(data), function (value, key) {
                Name += value.Name + " - " + value.Price + ' |<button class="btn btn-primary" data-toggle="modal" data-target="#myModal" ng-click="deleteSeviceCategory(' + value.Id + ')">Xóa</button>';
            });
            return Name.slice(0, Name.length - 1);
            
        }));

        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Tùy chọn').withClass('Center').notSortable().withOption('searchable', false).renderWith(function (data, stt, full, type) {
            console.log(full)
            return '<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/Sevices/Index/' + data + '#! > Cập nhật</a >|<button class="btn btn-primary" data-toggle="modal" data-target="#myModal" ng-click="delete(' + data + ')">Xóa</button> </br> <input type="button" value="Thêm kích thước" class="btn btn-info" ng-click="addPrice(' + full.Id + ')" style="margin: 15px;" />';

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

            //if (id != null) {
            //    dataservice.editSevice(id.value, function (rs) {
            //        $scope.seviceName = rs.data[0].name;
            //        $scope.seviceIsFood = rs.data[0].IsFood;
            //        rs = rs.data[0].listCategorysevice;

            //        console.log(rs);
            //        angular.forEach(rs, function (value, key) {
            //            $scope.ListSeviecs.push({
            //                "id": value.id,
            //                "name": value.name,
            //                "price": value.price
            //            });    
            //        });

            //        console.log($scope.ListSeviecs);
            //    });
            //}
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
    
    $scope.addPrice = function (id) {
        $scope.id = id;
        $scope.$apply;
            var modalInstance = $uibModal.open({
                templateUrl: "/View/admin/Sevices/addPrice.html",
                controller: "ModalContent",
                size: 'lg',
                scope: $scope,
                windowClass: 'show',
            });


    };
    $scope.update = function (id) {
        $scope.idupdate = id;
        var modalInstance = $uibModal.open({
            templateUrl: "/View/admin/Sevices/updatePrice.html",
            controller: "ModalUpdate",
            size: 'lg',
            scope: $scope,
            windowClass: 'show',
        });


    };
    $scope.sevicesCategories = function (id) {
        dataservice.getSeviceCategory(id, function (rs) {
            rs = rs.data;
            console.log(rs);
            $scope.sevicesCategories = rs.object;
        });
    }
    $scope.getSeviceCategoryUpdate = function (id) {
        dataservice.getSeviceCategoryUpdate(id, function (rs) {
            rs = rs.data;
            console.log(rs);
            $scope.sevicesCategories = rs.object;
        });
    }
    $scope.deleteSeviceCategory = function (id) {
        dataservice.deleteSeviceCategory(id, function (rs) {
            if (rs) {
                $scope.notification = "Xóa thành công";
                $scope.$apply;
            }
        });
    }
});
app.controller('ModalContent', function ($scope, $uibModalInstance, dataservice, $uibModal) {

    $scope.sevicesCategories($scope.id);
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
        if ($scope.model.Price <= 0 || $scope.model.Price == undefined || $scope.model.IdSeviceCategory == undefined) {
            $scope.messApiPrice = "Vui lòng kiểm tra thông tin";
            $scope.$apply;
        }
        else {
            $('#addprice').submit();
        }

    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    }

});

app.controller('ModalContentSize', function ($scope, $uibModalInstance, dataservice) {
    $scope.addSeviceCategory = function () {
        dataservice.checkCategory($scope.nameSize, function (rs1) {
            rs1 = rs1.data;
            $scope.flag = rs1;
        
        console.log($scope.flag);
        if ($scope.flag) {
            $scope.messApi = "Kích thước đã tồn tại";
        }
            else {
            dataservice.addCategory($scope.nameSize, function (rs) {
                rs = rs.data;

                if (rs.error) {
                    $scope.messApi = rs.title;
                } else {
                    /*$scope.sevicesCategories($scope.id);*/
                    $uibModalInstance.close("Ok");
                }
            });
            }
        });

    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    }

});
app.controller('ModalUpdate', function ($scope, $uibModalInstance, dataservice) {

    $scope.getSeviceCategoryUpdate($scope.idupdate);
    $scope.update = function () {
        if ($scope.model.Price <= 0 || $scope.model.Price == undefined || $scope.model.IdSeviceCategory == undefined) {
            $scope.messApiPrice = "Vui lòng kiểm tra thông tin";
            $scope.$apply;
        }
        else {
            $('#update').submit();
        }

    }
    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    }

});
