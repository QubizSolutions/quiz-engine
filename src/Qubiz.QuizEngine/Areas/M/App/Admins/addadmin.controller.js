(function () {
    'use strict';

   angular
      .module('quizEngineMaterial')
      .controller('AddAdminController', AddAdminController)
   AddAdminController.$inject = ['adminsService'];
   function AddAdminController(adminsService)
   {
       var vm = this;
       vm.Movie = {};
       vm.Save = Save;
       vm.Reset = Reset;
       function Reset()
       {
           vm.Movie = {};
       }
       function Save()
       {
           //Need Admin Service Add
           console.log("Saving works in controller");
       }
   }

})();
