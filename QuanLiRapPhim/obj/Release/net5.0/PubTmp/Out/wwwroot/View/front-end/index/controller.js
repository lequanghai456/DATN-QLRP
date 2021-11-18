var ctxfolderurl = "/View/front-end/Index";

var app = angular.module('App', ['ngRoute', 'ui.bootstrap','ngAnimate']);

app.controller('Ctroller', function () {

});
app.factory('dataservice', function ($http) {
    var headers = {
        "Content-Type": "application/json;odata=verbose",
        "Accept": "application/json;odata=verbose",
    }

    return {
        GetMovieSelection: function (callback) {
            $http.get('/Home/GetMovieSelection').then(callback);
        }
    }
});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
});

app.controller('index', function ($scope, $uibModal, dataservice) {
    $scope.init = function () {
        dataservice.GetMovieSelection(function (rs) {
            rs = rs.data;
            if (!rs.error) {
                $scope.mvs = rs.object;

                setTimeout(function () {
                    $scope.sliderloop();
                }, 1000);
            }
            //console.log(rs.object);
        });
    }
    $scope.trailer = "hi";
    $scope.init();

    $scope.playTrailer = function (stt) {
        $scope.trailer = $scope.mvs[stt].trailer;
        console.log($scope.trailer);
        $scope.loadEditForm();
    }

    $scope.loadEditForm = function () {
        var modalInstance = $uibModal.open({
            scope: $scope,
            animation: true,
            backdrop: true,
            templateUrl: ctxfolderurl + "/popuptrailer.html",
            controller: "Popupmodal",
            size: 'lg',
        });
    }

    $scope.slider = [
        "https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/r/s/rsz_t_j_ctkc_rollingbanner_980x448px_1.jpg",
        "https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/h/a/happy-new-year-980x448_1.png",
        "https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/k/v/kv_980x448.jpg",
    ];

    $scope.sliderloop = function () {
        $("#home").addClass("current-menu-item");
        
        $scope.viewedSlider = $('.bbb_viewed_slider');
        $scope.viewedSlider.owlCarousel({
            loop: true,
            margin: 280,
            autoplay: true,
            autoplayTimeout: 6000,
            dots: false,

            responsive:
            {
                0: { items: 1 },
                575: { items: 2 },
                768: { items: 3 },
                991: { items: 4 },
                1199: { items: 6 }
            }
        });
        $(".carousel-item:first").addClass("active");
    }
    
    
    $scope.prev = function () {
        $scope.viewedSlider.trigger('prev.owl.carousel');
    }
    $scope.next = function () {
        $scope.viewedSlider.trigger('next.owl.carousel');
    }
    
});

app.controller('Popupmodal', function ($scope, $uibModalInstance, $uibModal, dataservice) {


    $scope.close = function () {
        $uibModalInstance.close('cancel');
    }
});

