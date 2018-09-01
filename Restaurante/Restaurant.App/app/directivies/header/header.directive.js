(function () {
    'use strict';

    angular.module('app').directive('header', directive);

    function directive() {
        var controller = ['$scope', 'URLBASE', function ($scope, URLBASE) {
            function init() {
                $scope.URLBASE = URLBASE;
            }

            init();
        }];

        return {
            restrict: 'E',
            templateUrl: 'app/directivies/header/header.html',
            controller: controller
        };
    }
})();
