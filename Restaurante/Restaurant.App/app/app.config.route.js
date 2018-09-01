(function () {
    'use strict';

    angular.module('app').config(routeConfigurator);

    routeConfigurator.$inject = ['$urlRouterProvider', '$stateProvider', '$locationProvider', 'URLBASE', 'HTML5MODE'];

    function routeConfigurator($urlRouterProvider, $stateProvider, $locationProvider, URLBASE, HTML5MODE) {
        $urlRouterProvider.otherwise(URLBASE + '/home');

        $locationProvider.html5Mode(HTML5MODE == 'true');
    }
})();