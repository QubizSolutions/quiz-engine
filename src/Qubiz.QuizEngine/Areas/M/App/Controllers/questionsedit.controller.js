(function () {
    'use strict'
    angular
        .module('quizEngineMaterial')
        .controller('QuestionEditController', QuestionEditController)

    QuestionEditController.$inject = ['sectionsDataService', 'questionData', 'guidsService', '$routeParams', '$location'];

    function QuestionEditController(sectionsDataService, questionData, guidsService, $routeParams, $location) {
        var vm = this;

        vm.resetFields = resetFields;
        vm.saveEdit = saveEdit;
        vm.cancelEdit = cancelEdit;
        vm.addOption = addOption;
        vm.removeOption = removeOption;
        vm.setCorrectOption = setCorrectOption;
        vm.typeChanged = typeChanged;

        sectionsDataService.getAllSections().then(function (sections) {
            vm.Sections = sections;
        });

        questionData.getQuestionByID($routeParams.id).then(function (result) {
            vm.originalQuestion = result.data;
            resetFields();
        });

        function typeChanged() {
            if (vm.editedQuestion.Type == 0) {
                for (var i = 0; i < vm.editedQuestion.Options.length; i++) {
                    vm.editedQuestion.Options[i].IsCorrectAnswer = false;
                }
            }
        }

        function removeOption(option) {
            var index = vm.editedQuestion.Options.indexOf(option);
            vm.editedQuestion.Options.splice(index, 1);
        }

        function setCorrectOption(option) {
            if (vm.editedQuestion.Type == 0) {
                for (var i = 0; i < vm.editedQuestion.Options.length; i++) {
                    vm.editedQuestion.Options[i].IsCorrectAnswer = false;
                }
            }
            option.IsCorrectAnswer = !option.IsCorrectAnswer;
        };

        function addOption() {
            var guid = guidsService.getGuid();
            vm.editedQuestion.Options.push({
                Answer: '',
                ID: guid,
                IsCorrectAnswer: false,
                Order: vm.editedQuestion.Options.length,
                QuestionID: vm.editedQuestion.ID
            });
        }

        function resetFields(){
            vm.editedQuestion = angular.copy(vm.originalQuestion);
        }

        function saveEdit() {
            questionData.updateQuestion(vm.editedQuestion);
            $location.path('/questions');
        }

        function cancelEdit() {
            $location.path('/questions');
        }

    }
})();