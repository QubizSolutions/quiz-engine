(function () {
    'use strict';

    angular
		.module('quizEngineMaterial')
		.service('adminsService', adminsService);

    adminsService.$inject = ['$http', '$q'];

    function adminsService($http, $q) {
        this.getAllAdmins = getAllAdmins;
        this.addAdmin = addAdmin;
        this.deleteAdmin = deleteAdmin;
        this.editAdmin = editAdmin;
        this.getById = getById;

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
            }).then(function (result) {
                return result;
            })
        }

        function editAdmin(admin) {
            return $http({
                method: 'PUT',
                url: 'api/NewAdmin/UpdateAdmin',
                data: admin
            }).then(function (result) {
                return result;
            })
        }

        function getById(id) {
            return $http({
                method: 'GET',
                url: 'api/NewAdmin/GetAdmin/' + id
            });
        }

        function getAllAdminsSuccess(response) {
            console.log(response);
            return response.data;
        }

        function errorCallback(response) {
            return response;
        }

        function deleteAdmin(id) {
            return $http.delete('api/NewAdmin/DeleteAdmin/' + id);
        }
    }
})()