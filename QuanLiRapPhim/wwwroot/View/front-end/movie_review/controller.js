﻿var ctxfolderurl = "/View/front-end/movie_review";

var app = angular.module('App', ['ngRoute']);

app.controller('Ctroller', function ($scope) {
    $scope.init = function () {
        $("#movie_review").addClass("current-menu-item");
    }
    $scope.init();
});

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: ctxfolderurl + '/index.html',
            controller: 'index'
        }).when('/:NameMovie', {
            templateUrl: ctxfolderurl + '/moviedetail.html',
            controller: 'moviedetail'
        })
        .when('/:NameMovie/bookticket', {
            templateUrl: ctxfolderurl + '/bookticket.html',
            controller: 'bookticket'
        })
        .when('/:NameMovie/bookticket/payment', {
            templateUrl: ctxfolderurl + '/payment.html',
            controller: 'payment'
        })

});

app.controller('index', function ($scope) {
    
});

app.controller('moviedetail', function ($scope) {

});
app.controller('bookticket', function ($scope) {
    var socket = io.connect('http://localhost:3000');
    socket.on('connect', function (data) {
        socket.emit('join', 'Hello from client');
    });

    $scope.room = {
        row: 8, 
        col: 9,
    }

    $scope.seats = [];
    for (var i = 0; i < $scope.room.col; i++) {
        $scope.seats[i] = [];
        $scope.seats[i].name = String.fromCharCode(65+i);
        for (var j = 0; j < $scope.room.row; j++) {
            $scope.seats[i][j] = {
                id: i * 10 + j,
                status: true
            };
        }
    }
    $scope.seats[0][0].status = false;


    $scope.Click = function ($event,seat) {
        if (seat.status) {
            socket.emit('chonghe', seat.id);
        }
    }

    socket.on('dachonghe', function (data) {
        if (data.isemit) {
            $('#' + data.id).attr('src', 'images/seatdadat.png');
            $(".seatchon").attr("src", "images/seattrong.png");
            $(".seatchon").removeClass("seatchon");
            angular.element($event.currentTarget).attr("class", "seatchon");
        }
        else {
            $('#' + data.id).attr("src", "images/seatchon.png");
        }
    });
    socket.on('huydachon', function (data) {
        $('#' + data).attr("src", "images/seattrong.png");
    });

});
app.controller('payment', function ($scope) {
    $scope.init = function () {
        $scope.minuted = 1;
        $scope.second = 10;
        $scope.timeID = setInterval(function () {
            $scope.second -= 1;
            if ($scope.second == 0) {
                if ($scope.minuted == 0) {
                    clearInterval($scope.timeID);
                    history.back();
                    socket.on('huychon', '4');
                } else
                    $scope.minuted -= 1;
                $scope.second = 60;
            }
            $scope.$apply();
        }, 1000);
    }
    $scope.init();
    $scope.back = function () {
        clearInterval($scope.timeID);
        history.back();
    }
    $scope.payment = function () {}
});