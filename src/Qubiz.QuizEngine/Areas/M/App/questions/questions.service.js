﻿(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("questionData", questionData);

    questionData.$inject = ['$http'];

    function questionData($http) {

        this.getQuestionsPaged = getQuestionsPaged;
        this.deleteQuestion = deleteQuestion;
        this.getQuestionByID = getQuestionByID;
        this.updateQuestion = updateQuestion;
        this.addQuestion = addQuestion;

        function getQuestionsPaged(pageNumber, itemsPerPage) {
            return $http({
                method: 'GET',
                url: 'api/Question/GetQuestionsPaged/',
                params: {
                    pageNumber: pageNumber,
                    itemsPerPage: itemsPerPage
                }
            });
        }

        function deleteQuestion(question) {
            var id = question.ID;
            return $http({
                method: 'DELETE',
                url: 'api/Question/DeleteQuestion/' + id
            });
        }

        function getQuestionByID(ID){
            return $http({
                method: 'GET',
                url: 'api/Question/GetQuestion/',
                params: {
                    ID: ID
                }
            });
        }

        function updateQuestion(question) {
            return $http({
                method: 'PUT',
                url: 'api/Question/PutQuestion/',
                data: question
            });
        }

        function addQuestion(question) {
            return $http({
                method: 'POST',
                url: 'api/Question/PostQuestion/',
                data: question
            });
        }

    }
})();