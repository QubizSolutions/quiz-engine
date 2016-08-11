(function () {
    'use strict';

    angular
       .module('quizEngineMaterial')
       .controller('SaveAdminController', SaveAdminController);

    SaveAdminController.$inject = ['adminsService', '$location', '$mdDialog','$routeParams'];

    function SaveAdminController(adminsService, location, mdDialog,$routeParams) {

        var vm = this;

        var originalAdmin;

        adminsService.getById($routeParams.id)
            .then(function (result) {
                vm.admin = result.data;
                if (vm.admin != null) {
                 originalAdmin = vm.admin.Name;
                    vm.reset = function () {
                        vm.admin.Name = originalAdmin;
                    }
                    vm.save = editAdmin;
                }
                else {
                    vm.save = addAdmin;
                    vm.admin = {};
                    vm.admin.ID = $routeParams.id;
                    vm.admin.Name = "";
                    vm.reset = function () {
                        vm.admin.Name = "";
                    }
                }
                });
        
        function addAdmin() {
            if (vm.admin.Name =="")
            {
                mdDialog.show(mdDialog
                    .alert()
                    .title('Error')
                    .textContent("Empty Name")
                    .ok('Ok!'));
                return;
            }
            adminsService.addAdmin(vm.admin)
                .then(function () {
                            location.path('/administrators');
                })
                .catch(function () {
                   mdDialog.show(mdDialog
                  .alert()
                  .title('Error')
                  .textContent(result.data.Message)
                  .ok('Ok!'));
                });
        }

        function editAdmin() {
            if (vm.admin.Name == "") {
                mdDialog.show(mdDialog
                    .alert()
                    .title('Error')
                    .textContent("Empty Name")
                    .ok('Ok!'));
                return;
            }
            adminsService.editAdmin(vm.admin)
                .then(function () {
                    location.path('/administrators');
                })
                .catch(function (result) {
                    mdDialog.show(mdDialog
                        .alert()
                        .title('Error')
                        .textContent(result.data.Message)
                        .ok('Ok!'));
                });
         }
     }
    
})();