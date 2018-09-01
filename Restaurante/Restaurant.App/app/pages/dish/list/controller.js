(function () {
    'use strict';

    angular.module('app').controller('dishListController', controller);

    controller.inject = ['$state', 'URLBASE', 'dishService', 'toastr'];

    function controller($state, URLBASE, dishService, toastr) {
        var vm = this;
        vm.dishes = [];

        vm.init = init;
        vm.search = search;
        vm.new = newDish;
        vm.edit = edit;

        function init() {
            try {
                
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function search() {
            try {
                dishService.search(vm.query)
                    .then(function (response) {
                        if (!response.data.result) {
                            angular.forEach(response.data.messages, function (message) {
                                toastr.error(message, 'Error');
                            });
                            return;
                        }

                        vm.dishes = response.data.object;
                    })
                    .catch(function (e) {
                        toastr.error('Could not get the restaurants search', 'Error');
                    });
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function newDish() {
            try {
                $state.go(URLBASE + '/dish/new');
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function edit(id) {
            try {
                $state.go(URLBASE + '/dish/edit', {id: id});
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }
    }
})();