(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('AdminsController', AdminsController);

    AdminsController.$inject = ['adminsService', '$scope', '$mdDialog','guidsService'];

    function AdminsController(adminsService, scope, mdDialog,guidsService) {
        var vm = this;
        vm.deleteAdmin = deleteAdmin;
        getAllAdmins();

        function getAllAdmins() {
            adminsService.getAllAdmins()
                .then(function (result) {
                    vm.admins = result;
                    displayList();
                    getGuid();
                });
        }
        scope.showConfirm = function (ev, Admin) {
            var confirm = mdDialog.confirm()
                  .title('Are you sure you want to delete the admin '+Admin.Name+' ?')
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
        function displayList() {
            for (var i = 0; i < vm.admins.length; i++)
                vm.admins[i].Name = vm.admins[i].Name.substring( 6);
        }
        function getGuid()
        {

            vm.Guid =guidsService.getGuid();
        }
        
    }
})();