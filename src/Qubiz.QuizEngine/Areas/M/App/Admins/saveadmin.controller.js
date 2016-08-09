(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('SaveAdminController', SaveAdminController);

    SaveAdminController.$inject = ['adminsService', '$location', '$mdDialog','$routeParams'];

    function SaveAdminController(adminsService, location, mdDialog,$routeParams) {

        var vm = this;
        var originalAdmin;
        adminsService.GetById($routeParams.id)
        .then(function (result) {
            vm.Admin = result.data;
            if (vm.Admin != null) {
                originalAdmin = vm.Admin.Name;
                vm.Reset = function () {
                    vm.Admin.Name = originalAdmin;
                }
                vm.Save = editAdmin;
            }
            else {
                vm.Save = addAdmin;
                vm.Admin = {};
                vm.Admin.ID = $routeParams.id;
                vm.Reset = reset;
                console.log(vm.Admin);

            }
        }
        );
        
    


        function reset() {
            vm.Admin = {};
        }

         
        function addAdmin() {

            console.log(vm.Admin);
            if (vm.Admin.Name == "")
            {
                mdDialog.show(mdDialog
                    .alert()
                    .title('Error')
                    .textContent('Admin name cannot be empty!')
                    .ok('Ok!'));
            }
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
        function editAdmin()
        {
            adminsService.EditAdmin(vm.Admin)
                .then(function () {
                    location.path('/administrators');
                });

          }

     }
    
})();