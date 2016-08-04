(function () {
    'use strict';

   angular
      .module('quizEngineMaterial')
      .controller('AddAdminController', AddAdminController)
   AddAdminController.$inject = ['adminsService','$location','$mdDialog'];
   function AddAdminController(adminsService,location,mdDialog)
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
           adminsService.AddAdmin(vm.Admin).then(SavedSuccess).catch(SavedFailure);
       }
       function SavedSuccess()
       {
           location.path('/administrators');
       }
       function SavedFailure()
       {
           mdDialog.show(mdDialog.alert()
                     .title('Warning')
                     .textContent('Invalid Admin Name(Taken or Empty String)')
                     .ok('Ok!')
           );
           
       }
   }

})();
