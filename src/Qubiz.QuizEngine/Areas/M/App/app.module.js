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
                templateUrl: "Template/Questions",
                controller: "QuestionListController",
                controllerAs: "questionCtrl"
                })
                .when("/sections", {
                    templateUrl: "Template/Sections"
                })
                .when("/administrators", {
                	templateUrl: "Template/Administrators",
                	controller: "AdminsController",
                	controllerAs: "AdminCtrl"
                })
                .when('/addadmin', {
                	templateUrl: "Template/AddAdmin",
                	controller: "AddAdminController",
                	controllerAs: "AddAdminCtrl"
                })
                .when('/editadmin/:id',
                {
                	templateUrl: "Template/EditAdmin",
                	controller: "EditAdminController",
                	controllerAs: "EditCtrl"
                })
		        .otherwise({ redirectTo: "/tests" });
        });
})();