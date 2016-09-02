(function () {
    'use strict'
    angular
        .module('quizEngineMaterial')
        .controller('QuestionAddController', QuestionAddController)

    QuestionAddController.$inject = ['sectionsDataService', 'questionData', 'guidsService', '$location'];

    function QuestionAddController(sectionsDataService, questionData, guidsService, $location) {
        var vm = this;

        vm.resetFields = resetFields;
        vm.saveEdit = saveEdit;
        vm.cancelEdit = cancelEdit;
        vm.addOption = addOption;
        vm.removeOption = removeOption;
        vm.setCorrectOption = setCorrectOption;
        vm.typeChanged = typeChanged;

        sectionsDataService.getAllSections().then(function (response) {
            vm.Sections = response.data;
        });

        resetFields();

        function resetFields() {
            var guid = guidsService.getGuid();
            vm.editedQuestion = {
                Options: [],
                ID: guid,
                QuestionText: '',
                Type: 0
            };
        }

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

        function saveEdit() {
            questionData.addQuestion(vm.editedQuestion).then(function (){
                $location.path('/questions');
            });
        }

        function cancelEdit() {
            $location.path('/questions');
        }
    }

})();