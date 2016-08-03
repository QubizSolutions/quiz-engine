(function () {
    'use strict';

   angular
      .module('quizEngineMaterial')
      .controller('AdminsController', AdminsController)
   AdminsController.$inject = ['adminsService'];
   function AdminsController(adminsService)
   {
          var vm = this;
          vm.getAllAdmins = getAllAdmins;

          //getAllAdmins();



          function getAllAdmins() {
              return adminsService.getAllAdmins();
          }
          
     }

})();
