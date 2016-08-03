(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('AdminsController', AdminsController);

    AdminsController.$inject = ['adminsService'];

    function AdminsController(adminsService) {
        var vm = this;

        adminsService.getAllAdmins().then(function (result) {
            vm.admins = result;
        }).catch();

    }
})();