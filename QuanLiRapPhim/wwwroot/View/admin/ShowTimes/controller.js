﻿var ctxfolderurl = "https://localhost:44350";

var app = angular.module('App', ['datatables', 'ngRoute', 'checklist-model']);

app.factory('dataservice', function ($http) {
    return {
        deleteShowTime: function (data, callback) {
            $http.post('/Admin/ShowTimes/DeleteShowTime?id='+data).then(callback);
        },
        deleteShowTimeCheckbox: function (data, callback) {
            $http.post('/Admin/ShowTimes/DeleteShowTimeList?Listid=' + data).then(callback);
        },
        getListMovie: function (callback) {
            $http.get('/Admin/ShowTimes/ListMovie').then(callback);
        },
        GetMovieTitle: function (data,callback) {
            $http.get('/Admin/ShowTimes/GetMovieTitle/'+data).then(callback);
        },
    }
});

app.controller('Ctroller', function ($scope, DTOptionsBuilder, DTColumnBuilder, $compile, dataservice, $filter) {
    var vm = $scope;
    var id = document.getElementById('idEdit');
    $scope.items = [];
    $scope.selectAll = false;
    var LengthPage = 3;
    var itam = LengthPage;
    $scope.toggleOne = toggleOne;
    $scope.ListMovie = [];
    $scope.listShowTime = [];
   
    $scope.init = function () {

        dataservice.getListMovie(function (rs) {
            rs = rs.data;
            $scope.List = rs;
            console.log(rs);
        });
        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withOption('ajax', {
                url: "/Admin/ShowTimes/JtableShowTimeModel"
                , beforeSend: function (jqXHR, settings) {
                    $.blockUI({
                        boxed: true,
                        message: 'loading...'
                    });
                }
                , type: 'GET'
                , data: function (d) {
                    d.date = !$scope.Date ? "" : $filter('date')($scope.Date, 'yyyy-MM-dd');
                    d.RoomId = $scope.RoomId == -1 ? "" : $scope.RoomId;
                    console.log(d);
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
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Id').withClass('Center').notSortable().renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('DateTime', 'DateTime').withClass('Center').notSortable().renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Price', 'Price').withClass('Center').notSortable().renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('NameRoom', 'NameRoom').withClass('Center').notSortable().renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('NameMovie', 'NameMovie').withClass('Center').notSortable().renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('StartTime', 'StartTime').withClass('Center').notSortable().renderWith(function (data, type) {
            return data;
        }));
        vm.dtColumns.push(DTColumnBuilder.newColumn('Id', 'Option').withClass('Center').notSortable().withOption('searchable', false).renderWith(function (data, type) {
            return '<a class="btn btn-primary" href=' + ctxfolderurl + '/Admin/ShowTimes/Index/' + data + '#! > Edit</a >|<button class="btn btn-danger" data-toggle="modal" data-target="#myModal" ng-click="delete('+data+')">Delete</button>';
        }));      

    }
    $scope.init();
    
    vm.Show = function () {
        vm.create = !vm.create;
    };

    $scope.delete = function (idDelete) {
        var flag = true;
        if (id != null) {
            if (id.value == idDelete) {
                $scope.notification = "Cannot delete object being edited";
                flag = false;
            }
        }
        if (flag) {
            dataservice.deleteShowTime(idDelete, function (rs) {
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
    $scope.deleteShowTimeList = function () {
        if (id != null) {
            $scope.notification = "This feature cannot be used while editing";
        } else {
            $("input:checkbox[name=type]:checked").each(function () {
                $scope.selected.push($(this).val());
            });
            console.log($scope.selected);
            dataservice.deleteShowTimeCheckbox($scope.selected, function (rs) {
                
                rs = rs.data;
                
                $scope.notification = rs;
                $scope.selected = [];
                reloadData(true);
                //more screenings
                
            });

        }
    }
    //more screenings
      
    $scope.showTimes = false;

    $scope.init();
    $scope.total = $scope.ListMovie.length;

    $scope.add = function () {
        if ($scope.selected > 0) {
            $scope.ListMovie.push({
                id: $scope.total,
                data: $scope.selected,
            });
            if ($scope.time == null) {
                $scope.time = 0;
            }
            $scope.selected = "-1";
            $scope.$apply;
        }
    }

    $scope.StartTime = "";
    $scope.EndTime = "";
    $scope.showTimes = true;

    $scope.formSubmit = function (formid) {
        if ($scope.Date == null) {
            var mess='Bạn chưa chọn ngày';
            if ($scope.RoomId <= 0) {
                mess='Bạn chưa chọn phòng';
            }
            alert(mess);
        }
        else
        $(formid).submit();
    }
    $scope.resetform = function () {
        $scope.Date = null;
        reloadData(true);
    }

    $scope.Copy = function () {
        if ($scope.Date == null) {
            alert('Bạn chưa chọn ngày');
        }
        else {
            $('#myModalCopyform').modal('show');
        }
    }
    $scope.sussesCopy = function () {
        if ($scope.to == null) {
            alert('Bạn chưa chọn ngày copy đến');
            if ($scope.idTo == null)
                alert('Bạn chưa chọn phòng copy đến');
        } else {
            $scope.formSubmit("#CopyShowTimes");
        }
    }
    $scope.action = {
        del:function(index) {
            $scope.LitIdMovie.splice(index, 1);
            $scope.$apply;
            console.log($scope.LitIdMovie);
        },
        
        add: function () {
            if ($scope.selected > 0) {
                $scope.LitIdMovie.push($scope.selected);
                $scope.selected = '-1';
                $scope.$apply;
            }
        },

        getTimeEnd: function (data) {
            var time = $(".TimeStart").val();
            var dt = new Date();
            dt.setHours(time.split(':')[0]);
            dt.setMinutes(time.split(':')[1]);
            dt.setSeconds(time.split(':')[2]);
            dt.setMinutes(dt.getMinutes() + $scope.action.getTotalTime(data));
            return dt.getHours() + ":" + dt.getMinutes();
        },

        getMovie: function (id) {
            if ($scope.List != null)
                return $scope.List.find(x => x.id == id);
        },
        getTotalTime: function (data) {
            var init = 0;
            data.forEach(function (item, index) {
                if ($scope.List != null)
                init += $scope.List.find(x => x.id == item).time+30;
            });
            return init;
        },
        ShowTime: function (index) {
            if ($scope.List != null) {
                $scope.LitIdMovie;
            }
        }
    }
});

app.directive('listMovie', function () {
    return {
        restrict: 'E',
        template: '<input type="number" hidden ng-repeat="x in data track by $index" name="ListMivie[{{$index}}]" value="{{x}}"/>',
        scope: {
            data: "="
        },
        link: function (scope) {
            scope.init = function () {
                console.log(scope.data);
                var total = 0;
                scope.list = [];
                scope.data.forEach(function (item, index) {
                    scope.list.push({ id: total, data: item });
                    total += 1;
                    scope.$apply;
                });
            }
            scope.init();
        }
    }
});


app.directive('listTitleMovie', function (dataservice) {
    return {
        restrict: 'E',
        template:
            '<div class="Movie" ng-repeat="x in data track by $index">'
                + '<div>'
                    + '<span>Tên phim: {{action.getMovie(x).title}}</span>'
                    + '<br><span>Thời lượng: {{action.getMovie(x).time}} phút </span>'
                + '</div>'
                + '<div>{{action.ShowTime($index)}} </div>'
                + '<a class="btn btn-danger" ng-click="action.del($index)"> delete </a>'
            + '</div>',
        scope: {
            data: "=",
            action: "="
        },
        link: function ($scope) {
            $scope.total = 0;
            $scope.list = [];
        }
    }
});