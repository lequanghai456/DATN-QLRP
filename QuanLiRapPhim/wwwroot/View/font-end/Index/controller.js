var ctxfolderurl = "/View/font-end/Index";

var app = angular.module('App', ['ngRoute']);

app.controller('Ctroller', function () {

});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        })
});

app.controller('index', function ($scope) {
    $scope.mvs = [];
    for (var i = 0; i < 7; i++) {
        $scope.mvs[i] = {
            id: i,
            img: 'https://www.galaxycine.vn/media/c/o/cogai_2.jpg',
            trailer: 'https://www.youtube.com/embed/LggaymnzDjc'
        };
    }
    $scope.play = function (stt) {
        $scope.trailer = $scope.mvs[stt].trailer;
    }


    $scope.init = function () {
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
        
    }
    setTimeout(function () {
            $scope.init();
    }, 1000);
    
    $scope.prev = function () {
        $scope.viewedSlider.trigger('prev.owl.carousel');
    }
    $scope.next = function () {
        $scope.viewedSlider.trigger('next.owl.carousel');
    }
});