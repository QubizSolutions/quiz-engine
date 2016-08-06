(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('EditAdminController',EditAdminController)
    EditAdminController.$inject = ['adminsService','$location','$mdDialog','$routeParams','$scope'];
    function EditAdminController(adminsService, location, routeParams,$scope) {

        var vm = this;
        vm.Admin = {};
        vm.Save = Save;
        vm.Reset = Reset;
        adminsService.GetById($scope.id).then(function (response) {
            vm.Admin = response.data;
            console.log(vm.Admin);
        }).catch();

        function Save() {
            adminsService.EditAdmin(vm.Admin).then(SavedSucces).catch();
        }
        function Reset() {
            vm.Admin = {};

        }
        function SavedSucces()
        {
            
            location.path('/administrators');
        }
    }
})();