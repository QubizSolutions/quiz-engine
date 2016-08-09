(function () {
    'use strict';

    angular
		.module('quizEngineMaterial')
		.service('adminsService', adminsService);

    adminsService.$inject = ['$http', '$q'];

    function adminsService($http, $q) {
        this.getAllAdmins = getAllAdmins;
        this.AddAdmin = addAdmin;
        this.deleteAdmin = deleteAdmin;
        this.EditAdmin = editAdmin;
        this.GetById = getById;

        function getAllAdmins() {
            return $http({
                method: 'GET',
                url: 'api/NewAdmin/GetAdmins'
            })
            .then(getAllAdminsSuccess)
            .catch(errorCallback);
        }

        function addAdmin(admin) {
            return $http({
                method: 'POST',
                url: 'api/NewAdmin/AddAdmin',
                data: admin
            })
            .catch(errorCallback);
        }

        function editAdmin(admin) {
            return $http({
                method: 'PUT',
                url: 'api/NewAdmin/UpdateAdmin',
                data: admin
            })
            .catch(errorCallback);
        }

        function getById(id) {
            return $http({
                method: 'GET',
                url: 'api/NewAdmin/GetAdmin/' + id
            });
        }

        function getAllAdminsSuccess(response) {
            return response.data;
        }

        function errorCallback(response) {
            return $q.reject('HTTP status: ' + response.status + ' - ' + response.statusText + '!');
        }

        function deleteAdmin(id) {
            return $http.delete('api/NewAdmin/DeleteAdmin/' + id);
        }
    }
})()