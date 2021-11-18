app.factory('service', function ($http) {
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }

    return {
        GetAllSevice: function (callback) {
            $http.get("http://haile123-001-site1.ctempurl.com/YourOrder/GetAllServices").then(callback);
        }
    }
});

app.controller('choseService', function ($scope, $uibModalInstance, $uibModal, service) {

    $scope.init = function () {
        service.GetAllSevice(function (rs) {
            rs = rs.data;
            console.log(rs);
            if (rs.error) {
                console.log(rs.title);
            } else {

                $scope.sevices = rs.object;

                $scope.foods = $scope.sevices.filter(a => a.isFood);
                $scope.waters = $scope.sevices.filter(a => !a.isFood);
                console.log($scope.foods);

                $scope.$apply;
            }

        });
    }
    $scope.init();

    $scope.ListBilldetail = [];

    $scope.minus = function (index) {
        if ($scope.ListBilldetail[index].amout <= 1)
            if (confirm("Bạn muốn xóa ?"))
                $scope.ListBilldetail.splice(index, 1);
            else { }
        else
            $scope.ListBilldetail[index].amout--;
        $scope.$apply;
    }
    $scope.addinList = function (index) {
        $scope.ListBilldetail[index].amout++;
        $scope.$apply;
    }
    $scope.add = function (food) {
        var item = {
            id: food.id,
            name: food.name,
            size: food.size.find(x => x.id == food.choosesize),
            amout: 1
        }
        var bool = $scope.ListBilldetail.find(x => x.id == item.id && x.size.id == food.choosesize)
        if (bool != null) {
            var a = $scope.ListBilldetail.indexOf(bool);
            $scope.ListBilldetail[a].amout++;
        }
        else {
            $scope.ListBilldetail.push(item);
        }
        console.log($scope.ListBilldetail);
        $scope.$apply;
    }
    $scope.selected = [];

    $scope.cancel = function () {
        console.log($scope.foods.filter(a => a.select));
        $uibModalInstance.close('cancel');
    }

    $scope.payment = function () {
        $uibModalInstance.close('ok');
    }
    $scope.Total = function (arr) {
        var total = 0;
        arr.forEach(function (item, ind) {
            total += item.size.price * item.amout;
        });
        return total;
    }
})