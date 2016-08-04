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
                url: 'api/Admin/Get'
            })
            .then(getAllAdminsSuccess)
            .catch(errorCallback);
        }

        function getAllAdminsSuccess(response) {
            return response.data;
        }

        function errorCallback(response) {
            return $q.reject('HTTP status: ' + response.status + ' - ' + response.statusText + '!');
        }
    }
})()