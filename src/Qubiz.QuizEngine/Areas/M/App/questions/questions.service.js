(function () {
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
                url: 'api/Question/',
                params: {
                    pageNumber: pageNumber,
                    itemsPerPage: itemsPerPage
                }
            });
        }

        function deleteQuestion(question) {
        	return $http({
        		method: 'DELETE',
        		url: 'api/Question/',
        		params: {
					id : question.ID
        		}
            });
        }

        function getQuestionByID(id){
            return $http({
                method: 'GET',
                url: 'api/Question/',
                params: {
                    id: id
                }
            });
        }

        function updateQuestion(question) {
            return $http({
                method: 'PUT',
                url: 'api/Question/',
                data: question
            });
        }

        function addQuestion(question) {
            return $http({
                method: 'POST',
                url: 'api/Question/',
                data: question
            });
        }

    }
})();