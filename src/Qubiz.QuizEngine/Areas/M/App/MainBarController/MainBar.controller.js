(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .controller('MainBarTabsController', MainBarTabsController)

    MainBarTabsController.$inject = ['$location'];

    function MainBarTabsController(location) {
        var vm = this;

        vm.testsTabClick = function () {
            location.path('/tests');
        }

        vm.examsTabClick = function () {
            location.path('/exams');
        }

        vm.questionsTabClick = function () {
            location.path('/questions');
        }

        vm.sectionsTabClick = function () {
            location.path('/sections');
        }

        vm.administratorsTabClick = function () {
            location.path('/administrators');
        }
    }
})();