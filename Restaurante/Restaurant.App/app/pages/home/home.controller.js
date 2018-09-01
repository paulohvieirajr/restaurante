(function () {
    'use strict';

    angular.module('app').controller('homeController', controller);

    controller.inject = ['$state', '$sce', 'URLBASE', 'toastr'];

    function controller($state, $sce, URLBASE, toastr) {
        var vm = this;
        vm.salariominimo = {};
        vm.contribuintes = [];

        vm.init = init;
        vm.restaurants = restaurants;
        vm.dishes = dishes;

        function init() {
            try {
                
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function restaurants() {
            try {
                $state.go(URLBASE + '/restaurant');
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function dishes() {
            try {
                $state.go(URLBASE + '/dish');
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }
    }
})();