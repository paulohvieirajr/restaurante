(function () {
    'use strict';

    angular.module('app').controller('dishListController', controller);

    controller.inject = ['$scope', '$state', 'URLBASE', 'dishService', 'toastr'];

    function controller($scope, $state, URLBASE, dishService, toastr) {
        var vm = this;
        vm.dishes = [];
        $scope.URLBASE = URLBASE;

        vm.init = init;
        vm.search = search;
        vm.new = newDish;
        vm.edit = edit;
        vm.delete = deleteItem;

        function init() {
            try {
                vm.dishes = [];
                dishService.list()
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
                        toastr.error('Could not get the dishes list', 'Error');
                    });
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }

        function search() {
            try {
                vm.dishes = [];
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
                        toastr.error('Could not get the dishes search', 'Error');
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

        function deleteItem(id) {
            try {
                dishService.delete(id)
                    .then(function (response) {
                        if (!response.data.result) {
                            angular.forEach(response.data.messages, function (message) {
                                toastr.error(message, 'Error');
                            });
                            return;
                        }

                        toastr.success('Dish deleted.');
                        vm.search();
                    })
                    .catch(function (e) {
                        toastr.error('Could not delete the dish', 'Error');
                    });
            } catch (e) {
                toastr.error(e, 'Error');
            }
        }
    }
})();