(function () {

    'use strict';

    angular.module("quizApp").filter("questionType", questionType);

    function questionType() {
        return function (type) {
            return type == 0 ? "radio" : "checkbox";
        };
    };
}());