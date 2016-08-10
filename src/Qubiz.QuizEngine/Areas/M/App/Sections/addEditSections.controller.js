(function () {
	'use strict'

    angular
        .module('quizEngineMaterial')
        .controller('AddEditSectionController', AddEditSectionController)
    
    AddEditSectionController.$inject = ['sectionsDataService', '$location', 'guidsService', '$routeParams'];

    function AddEditSectionController(sectionsDataService, location, guidsService, routeParams) {
      
    	var vm = this;
    	var clone = {};
    	vm.section = {};
    	vm.message = {};

    	activate();

        vm.goBack = function (path) {
            location.path(path);
        }

        vm.reset = function () {
            vm.section = vm.clone;
        }
            
        function add() {
            vm.section.ID = routeParams.id;
            if (vm.section.Name == null) {
               return console.log("Name can't be empty");
            }
        	sectionsDataService.addSection(vm.section)
        	    .then(function () {
        	        location.path("/sections");
        	    });
        }

        function edit() {
            if (vm.section.Name == "") {
                return console.log("Name can't be empty");
            }
            if (!angular.equals(vm.section, clone)) {
                sectionsDataService.editSection(routeParams.id, vm.section)
                    .then(function () {
                        location.path("/sections");
                    });
                }
        }

        function activate() {
            sectionsDataService.readSection(routeParams.id).then(function (response) {

                if (response == null) {
                    vm.message = "Add section"
                    vm.Name = "Name";
                }
                else {
                    vm.message = "Edit section";
                    vm.section = response;
                }

                vm.save = ((vm.message == "Add section") ? add : edit );

                clone = angular.copy(vm.section);
            });
           
        }
    }
})();