(function () {
	'use strict'

    angular
        .module('quizEngineMaterial')
        .controller('AddEditSectionController', AddEditSectionController)
    
    AddEditSectionController.$inject = ['sectionsDataService', '$location', 'guidsService', '$routeParams'];

    function AddEditSectionController(sectionsDataService, location, guidsService, routeParams) {
      
    	var vm = this;
    	vm.section = {};
    	var clone = {};

    	activate();

        vm.goBack = function (path) {
            location.path(path);
        }

        vm.reset = function () {
            vm.section = vm.clone;
        }

        vm.save = ((vm.Message == "Add section") ? add : edit);
            
        function add () {
            vm.section.ID = routeParams.id;
        	sectionsDataService.addSection(vm.section)
        	console.log(vm.section);
        	location.path('/sections');
                
        }

        function edit() {
            if (!angular.equals(vm.section, clone)) {
                dataService.editSection(routeParams.id, vm.section)
                .then(function () {
                    location.path("/sections");
                });
            }
        }

        function activate() {
            sectionsDataService.readSection(routeParams.id).then(function () {
                vm.section = section;
                clone = angular.copy(section);
            });
            ((vm.section == null) ? vm.Message = "Add section" : vm.Message = "Edit section");
        }
    }
})();