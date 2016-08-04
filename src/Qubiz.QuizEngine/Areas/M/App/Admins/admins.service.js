(function () {
    'use strict';

    angular.module('quizEngineMaterial')
            .service('adminsService', adminsService);

    adminsService.$inject = ['$http', '$q'];

    function adminsService($http, $q) {
        this.getAllAdmins = getAllAdmins;
        this.AddAdmin = AddAdmin;
        function getAllAdmins() {
            return $http({
                method: 'GET',
                url: 'api/NewAdmin/GetAdmins'
            })
            .then(getAllAdminsSuccess)
            .catch(errorCallback);
        }
        function AddAdmin(admin)
        {
            console.log(admin);
            return $http({
                method: 'POST',
                url: 'api/NewAdmin/AddAdmin',
                data: admin
            })
            .then()
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