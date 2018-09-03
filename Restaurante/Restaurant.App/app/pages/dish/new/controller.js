(function () {
    'use strict';

    angular.module('app').controller('dishNewController', controller);

    controller.inject = ['$scope', '$state', '$stateParams', 'URLBASE', 'dishService', 'restaurantService', 'toastr'];

    function controller($scope, $state, $stateParams, URLBASE, dishService, restaurantService, toastr) {
        var vm = this;
        $scope.URLBASE = URLBASE;

        vm.model = {};
        vm.restaurants = [];

        vm.init = init;
        vm.back = back;
        vm.save = save;

        function init() {
            try {
                var id = $stateParams.id;
                vm.text = id ? 'Edit' : 'New';

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
                if(!vm.model.restaurant) {
                    toastr.error('Please, select a restaurant');
                    return;
                }

                if(!vm.model.name) {
                    toastr.error('Please, put the dish name');
                    return;
                }

                if(!vm.model.price) {
                    toastr.error('Please, put the dish price');
                    return;
                }

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