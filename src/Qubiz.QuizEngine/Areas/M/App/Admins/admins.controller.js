(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('AdminsController', AdminsController)
    AdminsController.$inject = ['adminsService','$scope','$mdDialog'];
    function AdminsController(adminsService,scope,mdDialog) {
        var vm = this;
        vm.deleteAdmin = deleteAdmin;
        console.log("Orice");
        getAllAdmins();

        function getAllAdmins() {
            adminsService.getAllAdmins()
                .then(function (result) {
                    vm.admins = result;
                    console.log(vm.admins);
                })
                .catch();
        }
        scope.showConfirm = function (ev) {
            // Appending dialog to document.body to cover sidenav in docs app
            var confirm = mdDialog.confirm()
                  .title('Would you like to delete your debt?')
                  .textContent('All of the banks have agreed to forgive you your debts.')
                  .ariaLabel('Lucky day')
                  .targetEvent(ev)
                  .ok('Please do it!')
                  .cancel('Sounds like a scam');

            mdDialog.show(confirm).then(function () {
                scope.status = 'You decided to get rid of your debt.';
            }, function () {
                scope.status = 'You decided to keep your debt.';
            });
        };

        function deleteAdmin(id) {
            adminsService.deleteAdmin(id)
                .then(getAllAdmins())
                .catch();
        }

        

    }


    ///////////////////////










})();
