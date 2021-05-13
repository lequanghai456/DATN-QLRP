var ctxfolderurl = "/View/front-end/movie_review";

var app = angular.module('App', ['ngRoute']);

app.controller('Ctroller', function () {

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

});
app.controller('payment', function ($scope) {

});