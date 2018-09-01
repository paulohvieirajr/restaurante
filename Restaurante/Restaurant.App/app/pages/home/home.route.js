(function () {
    'use strict';

    angular.module('app').config(config);

    config.$inject = ['$stateProvider', 'URLBASE'];

    function config($stateProvider, URLBASE) {
        $stateProvider
           .state(URLBASE + '/home', {
               url: URLBASE + '/home',
               cache: false,
               templateUrl: 'app/pages/home/home.html',
               controller: 'homeController as vm'
           });
    }
})();