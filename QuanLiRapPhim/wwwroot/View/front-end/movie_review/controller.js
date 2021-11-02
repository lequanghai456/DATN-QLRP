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
        GetMovie: function (data,callback) {
            $http.get('/moviereview/getmovie?search='+data).then(callback);
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
        },
        Rate: function (id, star,callback) {
            $http.get('/moviereview/Rate/' + id + '?star=' + star).then(callback);
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
        });

});

app.controller('index', function ($scope, dataservice) {
    $scope.Search = "";
    $scope.search = function () {
        dataservice.GetMovie($scope.Search, function (rs) {
            $scope.data = rs.data;
            console.log(rs.data);
            if ($scope.data == null) {

                console.log('Không có phim');

            } else {

                $scope.totalItems = $scope.data.length;
                $scope.currentPage = 1;
                $scope.numPerPage = 8;
                $scope.maxSize = 5; //Number of pager buttons to show

                $scope.setPage = function (pageNo) {
                    $scope.currentPage = pageNo;
                };

                $scope.numPages = function () {
                    return Math.ceil($scope.data.length / $scope.numPerPage);
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

                    $scope.movies = $scope.data.slice(begin, end);
                });

                $scope.reset = function () {

                }

                $(document).ready(function () {
                    $(".pagination li").addClass("page-item");
                    $(".pagination li a").addClass("page-link");
                });
            }

        });
    }

    $scope.search();
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
                console.log($scope.model);
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
        if (!$scope.model.isRated) {
            var modalInstance = $uibModal.open({
                scope: $scope,
                animation: true,
                backdrop: true,
                templateUrl: ctxfolderurl + "/ratePopup.htm",
                controller: "Popupmodal",
                size: 'lg',
            });
            $scope.Rate(0);
        }
    }

    $scope.Rate = function (index) {
        Rated(index, ".Rate");
        $scope.star = index;
    };
    $scope.Rated = Rated;

    $scope.OK = function () {
        if ($scope.star > 0 && $scope.star <= 5) {
            dataservice.Rate($scope.model.id, $scope.star, function (rs) {
                rs = rs.data;
                if (rs.error) {
                    alert(rs.title);
                }
                else {
                    alert("Bạn đã dánh giá phim " + $scope.star + " sao");
                    $scope.model.isRated = true;
                }
            });
        }
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
        console.log($scope.model);
        $uibModalInstance.close('cancel');
    }



});