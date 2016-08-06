(function () {
    'use strict'

    angular.module('quizEngineMaterial').service("questionData", questionData);

    questionData.$inject = ['$http', '$sce'];

    function questionData($http, $sce) {
        
        this.getQuestionsPaged = getQuestionsPaged;
        this.deleteQuestion = deleteQuestion;

        function getQuestionsPaged(pagenumber){
            var id = pagenumber;
            return $http({
                method: 'GET',
                url: 'api/NewQuestion/GetQuestionsPaged/' + id
            });
        }

        function deleteQuestion(question){
            var id = question.ID;
            return $http({
                method: 'DELETE',
                url: 'api/NewQuestion/DeleteQuestion/' + id
            });
        }
	}
})()