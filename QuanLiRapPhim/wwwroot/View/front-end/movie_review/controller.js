var ctxfolderurl = "/View/front-end/movie_review";

var app = angular.module('App', ['ui.bootstrap', 'ngRoute', 'ngAnimate']);

app.controller('Ctroller', function ($scope) {
    $scope.init = function () {
        $("#movie_review").addClass("current-menu-item");
    }
    $scope.init();
});

app.factory('dataservice', function ($http) {
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }

    return {
        GetMovie: function (callback) {
            $http.get('/moviereview/getmovie').then(callback);
       },
        GetListShowTime: function (id, Date, callback) {
            $http.get('/moviereview/GetListShowTime?idmovie=' + id + '&date=' + Date).then(callback);
        },
        GetMovieByName: function (name, callback) {
            $http.get('/moviereview/GetMovieByName?Name=' + name).then(callback);
        },
        GetListShowTimeByMovieAndDay: function (data,callback) {
            $.ajax({
                method: Get,
                url: '/moviereview/GetListShowTimeByMovieAndDay',
                data: data,
                success: callback
            })
        }
    }
});

app.config(function ($routeProvider) {
    
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        }).when('/:NameMovie', {
            templateUrl: ctxfolderurl + '/moviedetail.html',
            controller: 'moviedetail',
        }).when('/:NameMovie/bookticket/:id', {
            templateUrl: ctxfolderurl + '/bookticket.html',
            controller: 'bookticket'
        }).when('/:NameMovie/bookticket/:id/payment', {
            templateUrl: ctxfolderurl + '/payment.html',
            controller: 'payment'
        });

});

app.controller('index', function ($scope, $rootScope, dataservice) {
    $scope.init = function () {
        dataservice.GetMovie(function (rs) {
            $rootScope.data = rs.data;

            if ($rootScope.data == null) {
                console.log('Không có phim');
            } else {
                $scope.totalItems = $rootScope.data.length;
                $scope.currentPage = 1;
                $scope.numPerPage = 8;
                $scope.maxSize = 5; //Number of pager buttons to show

                $scope.setPage = function (pageNo) {
                    $scope.currentPage = pageNo;
                };

                $scope.numPages = function () {
                    return Math.ceil($rootScope.data.length / $scope.numPerPage);
                }

                $scope.pageChanged = function () {
                    console.log('Page changed to: ' + $scope.currentPage);
                };

                $scope.setItemsPerPage = function (num) {
                    $scope.itemsPerPage = num;
                    $scope.currentPage = 1;
                }
                $scope.$watch('currentPage + numPerPage', function () {
                    var begin = (($scope.currentPage - 1) * $scope.numPerPage)
                        , end = begin + $scope.numPerPage;

                    $scope.movies = $rootScope.data.slice(begin, end);
                });
                $(document).ready(function () {
                    $(".pagination li").addClass("page-item");
                    $(".pagination li a").addClass("page-link");
                });
                    }
        });
    }
    $scope.init();
});

app.controller('moviedetail', function ($scope, $routeParams, dataservice, $uibModal, $sce) {
    $scope.NameMovie = $routeParams.NameMovie;

    function Rated(rate,se) {
        if (rate <= 5 && rate >= 0) {
            $(se + " span.fa-star").removeClass("checked");
            var temp = Math.round(rate);
            for (var i = 0; i < temp; i++) {
                $(se+" span.fa-star").eq(i).addClass("checked");
            }
        }
    }
    $scope.init = function () {
        $scope.date = "";
        dataservice.GetMovieByName($scope.NameMovie, function (rs) {
            rs= rs.data;
            $scope.model = rs.object;
            if (rs.error) {
                $scope.message = rs.title;
                $scope.renderHtml = function (html_code) {
                    return $sce.trustAsHtml(html_code);
                };
            } else {
                $scope.GetListShowTime("");
                var total = $scope.model.totalRating == 0 ? 0 : $scope.model.totalRating / $scope.model.totalReviewers;
                $scope.Rated(total, ".Rated");
            }
        });

    }

    $scope.GetListShowTime = function (date) {
        dataservice.GetListShowTime($scope.model.id, date, function (rs) {
            $scope.List = rs.data;
            console.log(rs.data);
            $scope.$apply;
        });
    }

    $scope.init();

    $scope.BookTicket = function () {
        var modalInstance = $uibModal.open({
            scope: $scope,
            animation: true,
            backdrop: true,
            templateUrl: ctxfolderurl + "/showTimePopup.htm",
            controller: "Popupmodal",
            size: 'lg',
        });
    }
    $scope.btnRate = function () {
        var modalInstance = $uibModal.open({
            scope: $scope,
            animation: true,
            backdrop: true,
            templateUrl: ctxfolderurl + "/ratePopup.html",
            controller: "Popupmodal",
            size: 'lg',
        });
        $scope.Rate(0);
    }




    $scope.Rate = function (index) {
        Rated(index, ".Rate");
        $scope.star = index;
    };
    $scope.Rated = Rated;

    $scope.OK = function () {
        alert($scope.star);
    }
    $scope.close = function () {
        $scope.star = 0;
    }

    $scope.traloi = function (id) {
        $scope.comment = id;
    }
});

app.controller('Popupmodal', function ($scope, $uibModalInstance, $uibModal, dataservice) {
   

    $scope.close = function () {
        console.log($scope.List);
        $uibModalInstance.close('cancel');
    }



});