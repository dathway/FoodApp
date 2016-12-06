'use strict';

angular.module('foodApp.controllers', []).
    controller('foodAppCtrl', function ($routeParams, api) {
        var vm = this;
        vm.loaded = false;

        api.getFood({ foodId: $routeParams.id, serving: 1, measurement: "0" }, function (result) {
            vm.food = result;
            vm.loaded = true;
        });
    });