(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('SaveAdminController', SaveAdminController);

    SaveAdminController.$inject = ['adminsService', '$location', '$mdDialog','$routeParams'];

    function SaveAdminController(adminsService, location, mdDialog,$routeParams) {

        var vm = this;
        adminsService.GetById($routeParams.id).then(function(result)
        {
            vm.Admin = result.data;
            if (vm.Admin == null)
                vm.Save = addAdmin;
            else
                vm.Save = editAdmin;
        })
        vm.Reset = reset;

        function reset() {
            vm.Admin = {};
        }

         
        function addAdmin()
        {
          adminsService.AddAdmin(vm.Admin).then(function () {
                        location.path('/administrators');
               })
                }
        function editAdmin()
        {
            adminsService.EditAdmin(vm.Admin)
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