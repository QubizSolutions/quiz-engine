(function () {

    'use strict';

    angular.module("quizApp").filter('formatTimer', formatTimer);

    function formatTimer() {
        return function (input) {
            function z(n) { return (n < 10 ? '0' : '') + n; }
            var seconds = input % 60;
            var minutes = Math.floor(input / 60) % 60;
            var hours = Math.floor(input / 3600);
            if (minutes == 60)
                minutes = 0;

            return (z(hours) + ':' + z(minutes) + ':' + z(seconds));
        };
    };
}());