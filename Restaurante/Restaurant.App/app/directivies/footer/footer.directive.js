(function () {
    'use strict';

    angular.module('app').directive('footer', directive);

    function directive() {
        var controller = ['$scope', function ($scope) {

            
        }];

        return {
            restrict: 'E',
            templateUrl: 'app/directivies/footer/footer.html',
            controller: controller
        };
    }
})();
