(function () {
    'use strict';

    angular.module('app').service('restaurantService', service);

    service.$inject = ['$http', 'URLAPI'];

    function service($http, URLAPI) {
        return {
            list: list,
            search: search,
            get: get,
            save: save,
            insert: insert,
            update: update
        };

        function search(query) {
            return $http.get(URLAPI + 'api/restaurant/search?name=' + query);
        }

        function list() {
            return $http.get(URLAPI + 'api/restaurant');
        }

        function get(id) {
            return $http.get(URLAPI + 'api/restaurant/' + id);
        }

        function save(data) {
            if(data.idRestaurant)
                return $http.put(URLAPI + 'api/restaurant', data);
            else 
                return $http.post(URLAPI + 'api/restaurant', data);
        }   

        function insert(data) {
            return $http.post(URLAPI + 'api/restaurant', data);
        }

        function update(data) {
            return $http.put(URLAPI + 'api/restaurant', data);
        }
    }
})();