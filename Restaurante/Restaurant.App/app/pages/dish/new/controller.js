(function () {
    'use strict';

    angular.module('app').controller('dishNewController', controller);

    controller.inject = ['$state', '$stateParams', 'URLBASE', 'dishService', 'restaurantService', 'toastr'];

    function controller($state, $stateParams, URLBASE, dishService, restaurantService, toastr) {
        var vm = this;

        vm.model = {};
        vm.restaurants = [];

        vm.init = init;
        vm.back = back;
        vm.save = save;

        function init() {
            try {
                var id = $stateParams.id;
                if(id) {
                    dishService.get(id)
                        .then(function(response) {
                            if(!response.data.result) {
                                angular.forEach(response.data.messages, function (message) {
                                    toastr.error(message, 'Error');
                                });
                                return;
                            }

                            vm.model = response.data.object;
                        }).catch(function(e) {
                            toastr.error(e, 'Error');
                        });
                }

                restaurantService.list()
                    .then(function(response) {
                        if(!response.data.result) {
                            angular.forEach(response.data.messages, function (message) {
                                toastr.error(message, 'Error');
                            });
                            return;
                        }

                        vm.restaurants = response.data.object;
                    }).catch(function(e) {
                        toastr.error(e, 'Error');
                    });
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function back() {
            try {
                $state.go(URLBASE + '/dish');
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function save() {
            try {
                vm.model.idRestaurant = vm.model.restaurant.idRestaurant;
                
                dishService.save(vm.model)
                    .then(function(response) {
                        if(!response.data.result) {
                            angular.forEach(response.data.messages, function (message) {
                                toastr.error(message, 'Error');
                            });

                            return;
                        }

                        toastr.success('Dish saved succefull.');
                        vm.back();
                    }).catch(function(e) {
                        toastr.error(e, 'Error');
                    });
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }
    }
})();