(function () {
    'use strict'

    angular
        .module('quizEngineMaterial', ['ngRoute', 'ngAnimate', 'ngAria', 'ngMaterial'])
        .config(function ($routeProvider) {
            $routeProvider
                .when("/tests", {
                    templateUrl: "Template/Test"
                })
                .when("/exams", {
                    templateUrl: "Template/Exams"
                })
                .when("/questions", {
                    templateUrl: "Template/Questions"
                })
                .when("/sections", {
                    templateUrl: "Template/Sections"
                })
                .when("/administrators", {
                    templateUrl: "Template/Administrators"
                })
		        .otherwise({ redirectTo: "/tests" });
        });
})();