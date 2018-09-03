(function () {
    'use strict';

    angular.module('app').service('dishService', service);

    service.$inject = ['$http', 'URLAPI'];

    function service($http, URLAPI) {

        return {
            search: search,
            get: get,
            list: list,
            save: save,
            insert: insert,
            update: update,
            delete: deleteItem
        };

        function search(query) {
            return $http.get(URLAPI + 'api/dish/search?name=' + query);
        }

        function get(id) {
            return $http.get(URLAPI + 'api/dish/' + id);
        }

        function list() {
            return $http.get(URLAPI + 'api/dish');
        }

        function save(data) {
            if(data.idDish)
                return $http.put(URLAPI + 'api/dish', data);
            else 
                return $http.post(URLAPI + 'api/dish', data);
        }   

        function insert(data) {
            return $http.post(URLAPI + 'api/dish', data);
        }

        function update(data) {
            return $http.put(URLAPI + 'api/dish', data);
        }

        function deleteItem(id) {
            return $http.delete(URLAPI + 'api/dish', {params: {id: id}});
        }
    }
})();