(function () {
    'use strict';

   angular
      .module('quizEngineMaterial')
      .controller('AddAdminController', AddAdminController)
   AddAdminController.$inject = ['adminsService','$location'];
   function AddAdminController(adminsService,location)
   {

       var vm = this;
       vm.Admin = {};
       vm.Save = Save;
       vm.Reset = Reset;


       function Reset()
       {
           vm.Admin = {};
       }
       function Save()
       {
           //Need Admin Service Add
           console.log("Saving works in controller");
           adminsService.AddAdmin(vm.Admin).then(SavedSuccess).catch();

       }
       function SavedSuccess()
       {
           location.path('/administrators');
       }
   }

})();
