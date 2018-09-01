(function () {
    'use strict';

    angular.module('app').config(config);

    config.$inject = ['$stateProvider', 'URLBASE'];

    function config($stateProvider, URLBASE) {
        $stateProvider
           .state(URLBASE + '/dish', {
               url: URLBASE + '/dish',
               cache: false,
               templateUrl: 'app/pages/dish/list/view.html',
               controller: 'dishListController as vm'
            })
            .state(URLBASE + '/dish/new', {
                url: URLBASE + '/dish/new',
                templateUrl: 'app/pages/dish/new/view.html',
                controller: 'dishNewController as vm'
            })
            .state(URLBASE + '/dish/edit', {
                url: URLBASE + '/dish/edit',
                templateUrl: 'app/pages/dish/new/view.html',
                controller: 'dishNewController as vm',
                params: {
                    id: 0,
                }
            });
    }
})();