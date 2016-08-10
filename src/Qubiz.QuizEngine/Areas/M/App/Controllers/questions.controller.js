﻿(function () {
    'use strict'
    angular
        .module('quizEngineMaterial')
        .controller('QuestionListController', QuestionListController)

    QuestionListController.$inject = ['questionData'];

    function QuestionListController(questionData) {
        var vm = this;

        vm.itemsPerPage = 8;

        vm.addPage = addPage;
        vm.nextPage = nextPage;
        vm.prevPage = prevPage;
        vm.updatePage = updatePage;
        vm.selectQuestion = selectQuestion;
        vm.deleteQuestion = deleteQuestion;

        vm.pageNumber = 0;

        getQuestions();

        function getQuestions() {
            questionData.getQuestionsPaged(vm.pageNumber, vm.itemsPerPage).then(function (result) {
                vm.Questions = result.data;
                vm.maxPages = Math.ceil(vm.Questions.TotalCount / vm.itemsPerPage) - 1;
            });
        }

        function selectQuestion(question) {
            vm.selectedQuestion = angular.copy(question);
        }

        function deleteQuestion(question) {
            questionData.deleteQuestion(question).then(function (result) {
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

        function addPage(){

        }

        function updatePage() {
            if (isNaN(vm.pageNumber)) { vm.pageNumber = 0 }
            if (vm.pageNumber > vm.maxPages) { vm.pageNumber = vm.maxPages; }
            if (vm.pageNumber < 0) { vm.pageNumber = 0; }
            getQuestions();
        }
    }
})();