(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('AdminsController', AdminsController)
    AdminsController.$inject = ['adminsService', '$scope', '$mdDialog'];
    function AdminsController(adminsService,scope,mdDialog) {
        var vm = this;
        vm.deleteAdmin = deleteAdmin;
        getAllAdmins();

        function getAllAdmins() {
            adminsService.getAllAdmins()
                .then(function (result) {
                    vm.admins = result;
                })
                .catch();
        }
        scope.showConfirm = function (ev,Admin) {
            var confirm = mdDialog.confirm()
                  
                  .title('Are you sure you want to delete this admin?')
                  .textContent('This action cannot be undone.')
                  .ariaLabel('Lucky day')
                  .targetEvent(ev)
                  .cancel('No')
                  .ok('Yes');


            mdDialog.show(confirm).then(function () {
                deleteAdmin(Admin.ID);
            }, function () {
                scope.status = 'Deletion aborted.';
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
