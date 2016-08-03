(function () {
    'use strict';

    angular.module('quizEngineMaterial')
            .service('adminsService', adminsService);

    adminsService.$inject = ['$http', '$q'];

    function adminsService($http, $q) {
        this.getAllAdmins = getAllAdmins;

        function getAllAdmins() {
            return $http({
                method: 'GET',
                url: "api/Admin/Get"
            })
            .then(getAllAdminsSuccess)
            .catch(errorCallback);

            //return [1,2,3,4,5,6,7,8,9];
        }

        function getAllAdminsSuccess(response) {
            return response.data;
        }

        function errorCallback(response) {
            return $q.reject('HTTP status: ' + response.status + ' - ' + response.statusText + '!');
        }
    }
})()