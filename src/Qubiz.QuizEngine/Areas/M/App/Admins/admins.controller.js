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
        vm.Guid = guidsService.getGuid();
        console.log(vm.Guid);

        function getAllAdmins() {
            adminsService.getAllAdmins()
                .then(function (result) {
                    vm.admins = result;
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
        
    }
})();