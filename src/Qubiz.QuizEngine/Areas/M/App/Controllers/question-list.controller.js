(function () {
    'use strict'
    angular
        .module('quizEngineMaterial')
        .controller('QuestionListController', QuestionListController)

    QuestionListController.$inject = ['questionData'];

    function QuestionListController(questionData) {
        var vm = this;

        vm.nextPage = nextPage;
        vm.prevPage = prevPage;
        vm.updatePage = updatePage;
        vm.selectQuestion = selectQuestion;
        vm.deleteSelected = deleteSelected;

        vm.pageNumber = 0;

        getQuestions();

        function getQuestions() {
            questionData.getQuestionsPaged(vm.pageNumber).then(function (result) {
                vm.Questions = result.data;
                vm.maxPages = Math.ceil(vm.Questions.TotalCount / 20) - 1;
            });
        }

        function selectQuestion(question) {
            vm.selectedQuestion = angular.copy(question);
        }

        function deleteSelected() {
            questionData.deleteQuestion(vm.selectedQuestion).then(function (result) {
                vm.selectedQuestion = null;
                updatePage();
            });
        }

        function nextPage() {
            vm.pageNumber++;
            updatePage();
        }
        function prevPage() {
            vm.pageNumber--;
            updatePage();
        }

        function updatePage() {
            if (isNaN(vm.pageNumber)) { vm.pageNumber = 0 }
            if (vm.pageNumber > vm.maxPages) { vm.pageNumber = vm.maxPages; }
            if (vm.pageNumber < 0) { vm.pageNumber = 0; }
            getQuestions();
        }
    }
})();