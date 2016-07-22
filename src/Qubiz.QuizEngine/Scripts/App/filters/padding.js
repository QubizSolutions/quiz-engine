(function () {

    'use strict';

    angular.module("quizApp").filter("padding", padding);

    function padding() {
        return function (number, padding) {
            if (isNaN(number))
                return "";

            var formatedNumber = "";

            for (var i = 0; i < padding - number.toString().length; i++)
                formatedNumber += "0";

            formatedNumber += number.toString();

            return formatedNumber;
        };
    };
}());