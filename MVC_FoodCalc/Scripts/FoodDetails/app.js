
'use strict';

var foodApp = angular.module('foodApp', [    
    'ngRoute',
    'ui.bootstrap',
    'foodApp.controllers',
    'foodApp.services']).

    config(function ($routeProvider, $httpProvider) {
        $routeProvider.when('/', {
            templateUrl: '../../Content/foodApp/nutrition.html',
            controller: 'foodAppCtrl',
            controllerAs: 'vm'
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    });
    