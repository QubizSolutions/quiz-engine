(function () {
	'use strict'

    angular
        .module('quizEngineMaterial')
        .controller('SectionsController', SectionsController)
    
    SectionsController.$inject = ['sectionsDataService', '$mdDialog', '$location', 'guidsService'];

    function SectionsController(sectionsDataService, mdDialog, location, guidsService) {
        
        var vm = this;
        vm.sections = {};
        vm.getAllSections = getAllSections;
        vm.deleteSection = deleteSection;

        getAllSections();

        vm.addSection = function () {
            location.path('/addSection/' + guidsService.getGuid());
        }

        vm.editSection = function (sectionId) {
            location.path('/addSection/' + sectionId);
        }

        function getAllSections() {
            vm.sections = sectionsDataService.getAllSections()
                .then(function (sections) {
                    vm.sections = sections;
                })
                .catch(errorCallBack);
        }

        function deleteSection(id) {
            sectionsDataService.deleteSection(id)
                .then(function () {
                	vm.sections.splice(vm.sections.indexOf(id), 1);
                })
                .catch(errorCallBack);
        }

        function errorCallBack(errorMsg) {
            console.log('Error message: ' + errorMsg);
        }

        vm.showConfirm = function (ev, section) {
            var confirm = mdDialog.confirm()
                  .title('Are you sure you want to delete ' + section.Name + ' ?')
                  .textContent('This action cannot be undone.')
                  .targetEvent(ev)
                  .cancel('No')
                  .ok('Yes');

            mdDialog.show(confirm).then(function () {
                deleteSection(section.ID);
                vm.status = 'Section deleted successfuly.';
            }, function () {
                vm.status = 'Deletion aborted.';
            });
        };    
    }
})();