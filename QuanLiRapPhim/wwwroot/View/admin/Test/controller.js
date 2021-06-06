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
            ,beforeSend: function (jqXHR, settings) {
                //resetCheckbox();
                $.blockUI({
                    target: "#contentMain",
                    boxed: true,
                    message: 'loading...'
                });
            }
            , type: 'GET'
            //,data: function (d) {
            //    d.Name = $scope.model.Name;
            //    d.Number = $scope.model.Number;
            //}
            ,complete: function (data) {
                $.unblockUI("#contentMain");
                console.log(JSON.stringify(data.data));
            }
        })
        .withPaginationType('full_numbers')
        //.withDOM("<'table-scrollable't>ip")
        .withDataProp('data')
        .withDisplayLength(10)
        //.withOption('initComplete', function (settings, json) {
        //})
        //.withOption('createdRow', function (row, data, dataIndex) {
        //    const contextScope = $scope.$new(true);
        //    contextScope.data = data;
        //    contextScope.contextMenu = $scope.contextMenu;
        //    $compile(angular.element(row).contents())($scope);
        //    $compile(angular.element(row).attr('context-menu', 'contextMenu'))(contextScope);
        //});
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
    vm.dtEnglishColumns.push(DTColumnBuilder.newColumn('id','abc').withOption('sWidth', '60px').renderWith(function (data, type) {
        return '<a href="/admin/Test#!/' + data + '">detail</a>|<a href="/admin/Test#!/' + data +'">edit</a>';
    }));
    vm.reloadData = reloadData;
    vm.dtInstance = {};

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