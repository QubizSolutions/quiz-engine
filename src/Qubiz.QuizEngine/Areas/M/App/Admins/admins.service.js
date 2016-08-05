(function () {
    'use strict';

    angular.module('quizEngineMaterial')
            .service('adminsService', adminsService);

    adminsService.$inject = ['$http', '$q'];

    function adminsService($http, $q) {
        this.getAllAdmins = getAllAdmins;
        this.AddAdmin = AddAdmin;
        this.deleteAdmin = deleteAdmin;
        this.EditAdmin=EditAdmin;
        this.GetById=GetById;
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
            return $http({
                method: 'POST',
                url: 'api/NewAdmin/AddAdmin',
                data: admin
            })
            .then()
            .catch(errorCallback);
        }
        function EditAdmin(admin)
        {
            return $http({
                method: 'PUT',
                url: 'api/NewAdmin/UpdateAdmin',
                data: admin
            })
            .then()
            .catch(errorCallback);
        }
        function GetById(id)
        {
            return $http({
                method: 'GET',
                url: 'api/NewAdmin/GetAdmin',
                data: id

            })
            .then()
            .catch();
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