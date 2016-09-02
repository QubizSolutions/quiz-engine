(function () {
	'use strict'

	angular
        .module('quizEngineMaterial')
        .controller('AddEditSectionController', AddEditSectionController)

	AddEditSectionController.$inject = ['sectionsDataService', '$location', 'guidsService', '$routeParams'];

	function AddEditSectionController(sectionsDataService, location, guidsService, routeParams) {

		var vm = this;
		vm.section = {};
		vm.message = {};
		vm.save = save;
		vm.reset = reset;

		var clone = {};
		var isAdd = false;

		activate();

		function save() {
			if (!vm.section.Name)
				return;

			if (isAdd) {
				vm.section.ID = routeParams.id;
				sectionsDataService.addSection(vm.section)
					.then(function () {
						location.path("/sections");
					});
			}
			else {
				if (!angular.equals(vm.section, clone)) {
					sectionsDataService.editSection(routeParams.id, vm.section)
						.then(function () {
							location.path("/sections");
						});
				}
			}
		}

		function reset() {
			angular.copy(clone, vm.section);
		}

		function activate() {
			sectionsDataService.readSection(routeParams.id).then(function (section) {
				if (section == null) {
					isAdd = true;
					vm.message = "Add section"
					vm.Name = "Name";
				}
				else {
					vm.message = "Edit section";
					vm.section = section;
					clone = angular.copy(vm.section);
				}
			});
		}
	}
})();