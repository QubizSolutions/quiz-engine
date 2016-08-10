(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("questionData", questionData);

    questionData.$inject = ['$http'];

    function questionData($http) {

        this.getQuestionsPaged = getQuestionsPaged;
        this.deleteQuestion = deleteQuestion;

        function getQuestionsPaged(pageNumber, itemsPerPage) {
            return $http({
                method: 'GET',
                url: 'api/Questions/GetQuestionsPaged/',
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
                url: 'api/Questions/DeleteQuestion/' + id
            });
        }
    }
})();