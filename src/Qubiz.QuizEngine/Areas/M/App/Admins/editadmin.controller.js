(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('EditAdminController', EditAdminController);

    EditAdminController.$inject = ['adminsService', '$location', '$mdDialog', '$routeParams', '$scope'];

    function EditAdminController(adminsService, location, routeParams, $scope) {

        var vm = this;
        vm.Admin = {};
        vm.Save = save;
        vm.Reset = reset;

        adminsService.GetById($scope.id).then(function (response) {
            vm.Admin = response.data;
        }).catch();

        function save() {
            adminsService.EditAdmin(vm.Admin).then(SavedSucces).catch();
        }

        function reset() {
            vm.Admin = {};
        }

        function SavedSucces() {
            location.path('/administrators');
        }
    }
})();