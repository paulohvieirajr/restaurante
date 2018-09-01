(function () {
    'use strict';

    angular.module('app').config(config);

    config.$inject = ['$stateProvider', 'URLBASE'];

    function config($stateProvider, URLBASE) {
        $stateProvider
           .state(URLBASE + '/restaurant', {
               url: URLBASE + '/restaurant',
               cache: false,
               templateUrl: 'app/pages/restaurant/list/view.html',
               controller: 'restaurantListController as vm'
            })
            .state(URLBASE + '/restaurant/new', {
                url: URLBASE + '/restaurant/new',
                templateUrl: 'app/pages/restaurant/new/view.html',
                controller: 'restaurantNewController as vm'
            })
            .state(URLBASE + '/restaurant/edit', {
                url: URLBASE + '/restaurant/edit',
                templateUrl: 'app/pages/restaurant/new/view.html',
                controller: 'restaurantNewController as vm',
                params: {
                    id: 0,
                }
            });
    }
})();