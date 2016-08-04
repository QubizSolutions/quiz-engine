(function () {
    'use strict'
    angular.module('quizEngineMaterial', ['ngRoute', 'ngAnimate', 'ngAria', 'ngMaterial']);
    angular.module('quizEngineMaterial').config(function ($routeProvider) {
        $routeProvider.when("/tests", {
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
                templateUrl: "Template/Administrators",
                controller: "AdminsController",
                controllerAs:"AdminCtrl"

            })
            .when('/addadmin', {
                templateUrl: "Template/AddAdmin",
                controller: "AddAdminController",
                controllerAs:"AddAdminCtrl"
            })
		.otherwise({ redirectTo: "/tests" });
    });
})();
