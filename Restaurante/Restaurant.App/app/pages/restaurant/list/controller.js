(function () {
    'use strict';

    angular.module('app').controller('restaurantListController', controller);

    controller.inject = ['$state', '$sce', 'URLBASE', 'restaurantService', 'toastr'];

    function controller($state, $sce, URLBASE, restaurantService, toastr) {
        var vm = this;
        vm.restaurants = [];

        vm.init = init;
        vm.search = search;
        vm.new = newRestaurant;
        vm.edit = edit;
        vm.delete = deleteItem;

        function init() {
            try {
                
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function search() {
            try {
                restaurantService.search(vm.query)
                    .then(function (response) {
                        if (!response.data.result) {
                            angular.forEach(response.data.messages, function (message) {
                                toastr.error(message, 'Error');
                            });
                            return;
                        }

                        vm.restaurants = response.data.object;
                    })
                    .catch(function (e) {
                        toastr.error('Could not get the restaurants search', 'Error');
                    });
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function newRestaurant() {
            try {
                $state.go(URLBASE + '/restaurant/new');
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function edit(id) {
            try {
                $state.go(URLBASE + '/restaurant/edit', {id: id});
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function deleteItem(id) {
            try {
                restaurantService.delete(id)
                    .then(function (response) {
                        if (!response.data.result) {
                            angular.forEach(response.data.messages, function (message) {
                                toastr.error(message, 'Error');
                            });
                            return;
                        }

                        toastr.success('Restaurant deleted.');
                        vm.search();
                    })
                    .catch(function (e) {
                        toastr.error('Could not delet the restaurant', 'Error');
                    });
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }
    }
})();