(function () {
    'use strict';

    angular.module('app').factory('interceptorFactory', factory);

    factory.$inject = ['$templateCache', '$location', '$q', 'URLBASE', 'APPVERSION', 'URLAPI'];

    function factory($templateCache, $location, $q, URLBASE, APPVERSION, URLAPI) {
        return {
            'request': function (request) {
                return request;
            },
            'responseError': function (rejection) {
                return $q.reject(rejection);
            }
        }
    }
})();