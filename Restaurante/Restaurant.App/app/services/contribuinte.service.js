(function () {
    'use strict';

    angular.module('app').service('contribuinteService', service);

    service.$inject = ['$http', 'URLAPI'];

    function service($http, URLAPI) {

        function listar() {
            return $http.get(URLAPI + 'api/contribuinte/listar');
        }

        function inserir(data) {
            return $http.post(URLAPI + 'api/contribuinte', data);
        }

        function alterar(data) {
            return $http.put(URLAPI + 'api/contribuinte', JSON.stringify(data));
        }

        return {
            listar: listar,
            inserir: inserir,
            alterar: alterar
        };
    }
})();