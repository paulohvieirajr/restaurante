(function () {
    'use strict';

    angular.module('app').controller('restaurantNewController', controller);

    controller.inject = ['$state', '$stateParams', 'URLBASE', 'restaurantService',  'toastr'];

    function controller($state, $stateParams, URLBASE, restaurantService, toastr) {
        var vm = this;
        vm.model = {};

        vm.init = init;
        vm.back = back;
        vm.save = save;

        function init() {
            try {
                var id = $stateParams.id;
                if(id) {
                    restaurantService.get(id)
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
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function back() {
            try {
                $state.go(URLBASE + '/restaurant');
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function save() {
            try {
                restaurantService.save(vm.model)
                    .then(function(response) {
                        if(!response.data.result) {
                            angular.forEach(response.data.messages, function (message) {
                                toastr.error(message, 'Error');
                            });

                            return;
                        }

                        toastr.success('Restaurant saved sucefull.');
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