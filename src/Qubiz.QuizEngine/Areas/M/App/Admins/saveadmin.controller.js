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
        })
        vm.Save = save;
        vm.Reset = reset;

        function reset() {
            vm.Admin = {};
        }

        function save() {
            if (vm.Admin == undefined) {
                console.log("sds");
                adminsService.addAdmin(vm.Admin).then(function () {
                    location.path('/administrators');
                })
            }
            else {
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

    }
})();