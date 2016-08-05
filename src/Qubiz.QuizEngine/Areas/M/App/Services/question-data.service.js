(function () {
    'use strict'

    angular.module('quizEngineMaterial').service("questionData", questionData);

    questionData.$inject = ['$http', '$sce'];

    function questionData($http, $sce) {
        
        this.getQuestionsPaged = getQuestionsPaged;

        function getQuestionsPaged(pagenumber){
            var id = pagenumber;
            return $http({
                method: 'GET',
                url: 'm/api/NewQuestion/GetQuestionsPaged/' + id
            });
        }
	}
})()