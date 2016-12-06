'use strict';

angular.module('foodApp.services', ['ngResource']).
    service('api', function api($resource, $http) {

        function handleError(err, fn) {
            if (!fn) {
                notificationService.error(err);
            } else {
                fn(err);
            }
        }

        return {            
            getFood: function (foodId, serving, measurement, callback, error) {
                $http.get('/home/GetFood/', { foodId: foodId, serving: serving, measurement: measurement })
                    .success(callback)
                    .error(function (e) { handleError(e, error); });
            },
        }

    });