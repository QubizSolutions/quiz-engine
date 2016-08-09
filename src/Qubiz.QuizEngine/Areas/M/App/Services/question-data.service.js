(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("questionData", questionData);

    questionData.$inject = ['$http'];

    function questionData($http) {

        this.getQuestionsPaged = getQuestionsPaged;
        this.deleteQuestion = deleteQuestion;

        function getQuestionsPaged(pagenumber) {
            return $http({
                method: 'GET',
                url: 'api/NewQuestion/GetQuestionsPaged/' + pagenumber
            });
        }

        function deleteQuestion(question) {
            var id = question.ID;
            return $http({
                method: 'DELETE',
                url: 'api/NewQuestion/DeleteQuestion/' + id
            });
        }
    }
})();