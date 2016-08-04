(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('AdminsController', AdminsController)
    AdminsController.$inject = ['adminsService','$scope','$mdDialog'];
    function AdminsController(adminsService,scope,mdDialog) {
        var vm = this;
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
            var confirm = mdDialog.confirm()
                  .ok('Yes')
                  .cancel('No')
                  .title('Are you sure you want to delete this admin?')
                  .textContent('This action cannot be undone.')
                  .ariaLabel('Lucky day')
                  .targetEvent(ev);


            mdDialog.show(confirm).then(function () {

                scope.status = 'Admin deleted successfuly.';
            }, function () {

                scope.status = 'Deletion aborted.';
            });
        };
    }


})();
