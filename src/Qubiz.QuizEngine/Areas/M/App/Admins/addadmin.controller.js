(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('AddAdminController', AddAdminController);

    AddAdminController.$inject = ['adminsService', '$location', '$mdDialog'];

    function AddAdminController(adminsService, location, mdDialog) {

        var vm = this;
        vm.Admin = {};
        vm.Save = save;
        vm.Reset = reset;

        function reset() {
            vm.Admin = {};
        }

        function save() {
            adminsService.AddAdmin(vm.Admin)
                .then(function () {
                    location.path('/administrators');
                })
                .catch(function () {
                    mdDialog.show(mdDialog
                        .alert()
                        .title('Error')
                        .textContent('This admin already exists.')
                        .ok('Ok!'));
                });
        }
    }
})();