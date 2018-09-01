(function () {
    'use strict';

    angular.module('app').directive('content', directive);

    function directive() {
        return {
            restrict: 'E',
            templateUrl: 'app/directivies/content/content.html'
        };
    }
})();
