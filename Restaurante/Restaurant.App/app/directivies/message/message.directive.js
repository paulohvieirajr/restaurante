(function () {
    'use strict';

    angular.module('app').directive('message', directive);
    
    function directive () {
        var link = function (scope, el, attr) {
            scope.show = false;

            scope.$watch('text', function (newValue, oldValue) {                    
                scope.show = (newValue);
            });
        };

        var controller = ['$scope', function ($scope) {
            $scope.close = function () {
                $scope.show = false;
                $scope.text = null;
            }
        }];

        return {
            restrict: 'E',
            scope: {
                text: '='
            },
            templateUrl: 'app/directivies/message/message.html',
            link: link,
            controller: controller
        };
    }
})();
