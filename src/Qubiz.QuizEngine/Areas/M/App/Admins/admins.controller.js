(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('AdminsController', AdminsController);

    AdminsController.$inject = ['adminsService', '$scope', '$mdDialog','guidsService','$location'];

    function AdminsController(adminsService, scope, mdDialog, guidsService, location) {

        var vm = this;

        vm.deleteAdmin = deleteAdmin;

        getAllAdmins();

        vm.goToSave = goToSave;

        function getAllAdmins() {
            adminsService.getAllAdmins()
                .then(function (result) {
                    displayList(result.data);
                });
        }

        scope.showConfirm = function (ev, Admin) {
            var confirm = mdDialog.confirm()
                  .title('Are you sure you want to delete the admin '+ Admin.Name +' ?')
                  .textContent('This action cannot be undone.')
                  .ariaLabel('Lucky day')
                  .targetEvent(ev)
                  .cancel('No')
                  .ok('Yes');

            mdDialog.show(confirm).then(function () {
                deleteAdmin(Admin.ID);
            });
        };

        function deleteAdmin(id) {
            adminsService.deleteAdmin(id)
                .then(function () {
                    scope.status = 'Admin deleted successfuly.';
                    getAllAdmins();
                })
                .catch(function () {
                    scope.status = 'You cannot delete yourself.';
                });
        }

        function displayList(withDomain) {
            vm.admins = [];
            for (var i = 0; i < withDomain.length; i++)
            {
                vm.admins[i] = withDomain[i];
                vm.admins[i].Name=vm.admins[i].Name.substring(6);

            }
        }

        function goToSave(){

            vm.guid = guidsService.getGuid();
            location.path('/saveadmin/' + vm.guid);
        }
        
    }
})();