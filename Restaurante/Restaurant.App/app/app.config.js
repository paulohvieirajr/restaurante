(function () {
    'use strict';

    angular.module('app').config(config)

    config.$inject = ['$httpProvider', 'cfpLoadingBarProvider', 'APPVERSION', 'LOADTEMPLATE'];

    function config($httpProvider, cfpLoadingBarProvider, APPVERSION, LOADTEMPLATE) {

        //cfpLoadingBarProvider.spinnerTemplate = LOADTEMPLATE;
        $httpProvider.interceptors.push('interceptorFactory');
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
    }    
})();