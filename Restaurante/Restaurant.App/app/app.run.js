(function () {
    'use strict';

    angular.module('app').run(run);

    function run($http) {
        Date.prototype.yyyymmdd = function () {
            var mm = this.getMonth() + 1;
            var dd = this.getDate();

            return [this.getFullYear(),
                    (mm > 9 ? '' : '0') + mm,
                    (dd > 9 ? '' : '0') + dd
            ].join('-');
        };

        Date.prototype.ddmmyyyy = function () {
            var mm = this.getMonth() + 1;
            var dd = this.getDate();

            return [(dd > 9 ? '' : '0') + dd,
                    (mm > 9 ? '' : '0') + mm,
                    this.getFullYear()].join('/');
        };
    }
})();