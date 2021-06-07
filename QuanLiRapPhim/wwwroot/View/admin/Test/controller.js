var ctxfolderurl = "/View/Admin/Test";

var app = angular.module('App', ['datatables', 'ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            controller: 'index1'
        })
        .when('/create', {
            templateUrl: 'https://localhost:44350/admin/Test/CreateEdit',
            controller: 'create'
        })
        .when('/:id*', {
            templateUrl: function (urlattr) { return 'https://localhost:44350/admin/Test/CreateEdit/' + urlattr.id },
            controller: 'edit'
        })
});
app.controller('create', function ($scope) {
    $scope.action = 'Create';
});
app.controller('edit', function ($scope) {
    $scope.action = 'Edit';
});
app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile) {
    var vm = $scope;
    
    //$scope.selected = [];
    //$scope.selectAll = false;
    //$scope.toggleAll = toggleAll;
    //$scope.toggleOne = toggleOne;

    //var titleHtml = '<label class="mt-checkbox"><input type="checkbox" ng-model="selectAll" ng-change="toggleAll(selectAll, selected)"/><span></span></label>';
    vm.dtEnglishOptions = DTOptionsBuilder.newOptions()
        .withOption('ajax', {
            url: "/Admin/Test/GetAll"
            , beforeSend: function (jqXHR, settings) {
                //resetCheckbox();
                $.blockUI({
                    boxed: true,
                    message: 'loading...'
                });
            }
            , type: 'GET'
            //,data: function (d) {
            //    d.Name = $scope.model.Name;
            //    d.Number = $scope.model.Number;
            //}
            , complete: function (data) {
                $.unblockUI();
                console.log(JSON.stringify(data.data));
            }
        })
        .withPaginationType('full_numbers')
        //.withDOM("<'table-scrollable't>ip")
        .withDataProp('data')
        .withDisplayLength(10)
        .withOption('createdRow', function (row) {
            $compile(angular.element(row).contents())($scope);
        });;


    vm.dtEnglishColumns = [];
    vm.dtEnglishColumns.push(DTColumnBuilder.newColumn('id', 'ID').withOption('sWidth', '60px').renderWith(function (data, type) {
        return data;
    }));
    vm.dtEnglishColumns.push(DTColumnBuilder.newColumn('name', 'Name').withOption('sWidth', '60px').renderWith(function (data, type) {
        return data;
    }));
    vm.dtEnglishColumns.push(DTColumnBuilder.newColumn('number', 'Number').withOption('sWidth', '60px').renderWith(function (data, type) {
        return data;
    }));
    vm.dtEnglishColumns.push(DTColumnBuilder.newColumn('id', 'abc').withOption('sWidth', '60px').renderWith(render).withOption('searchable', false).notSortable());
    vm.reloadData = reloadData;
    vm.dtInstance = {};

    $scope.del = function (id) {
        alert("deleted " + id);
    }
    function render (data) {
        var html = '<button class="btn btn-success" ng-click="del(' + data + ')" >delete</button>|<a href="/admin/Test#!/' + data + '">edit</a>';
        return html;
    }
    function reloadData(resetPaging) {
        vm.dtEnglishInstance.reloadData(callback, resetPaging);
    }
    function callback(json) {

    }
    //$scope.Search = function () { reloadData(true) };
    //function toggleAll(selectAll, selectedItems) {
    //    for (var id in selectedItems) {
    //        if (selectedItems.hasOwnProperty(id)) {
    //            selectedItems[id] = selectAll;
    //        }
    //    }
    //}
    //function toggleOne(selectedItems, evt) {
    //    $(evt.target).closest('tr').toggleClass('selected');
    //    for (var id in selectedItems) {
    //        if (selectedItems.hasOwnProperty(id)) {
    //            if (!selectedItems[id]) {
    //                vm.selectAll = false;
    //                return;
    //            }
    //        }
    //    }
    //    vm.selectAll = true;
    //}
    //function resetCheckbox() {
    //    $scope.selected = [];
    //    vm.selectAll = false;
    //}
});