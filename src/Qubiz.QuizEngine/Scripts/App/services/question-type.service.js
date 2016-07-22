(function () {

    'use strict';
    
    function QuestionType() {
        return {
            MultipleSelect: 1,
            SingleSelect : 0
        }
    };

    quizApp.factory("QuestionType", QuestionType);

}())