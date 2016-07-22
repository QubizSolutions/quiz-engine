(function () {
    'use strict'

    quizApp.factory("questionData", questionData);

    questionData.$inject = ['$resource', '$sce'];

    function questionData($resource, $sce) {
        return $resource("api/Question", {}, {
            save: { method: 'POST' },
            examQuestion: {
                url: "api/ExamQuestion",
                method: 'GET',
                interceptor: {
                    response: function (response) {
                        response.data.QuestionText = $sce.trustAsHtml(response.data.QuestionText);
                        for (var idx in response.data.Options)
                            response.data.Options[idx].Answer = $sce.trustAsHtml(response.data.Options[idx].Answer);
                        return response.data;
                    }
                }
            },
            filteredQuestion: {
                url: "api/QuestionsFiltered",
                method: 'GET',
                response: function (response) {
                    return response.data;
                }
            }
	    });
	}
})()