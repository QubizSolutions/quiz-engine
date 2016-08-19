(function () {
    'use strict'

    angular
        .module('quizEngineMaterial', ['ngRoute', 'ngAnimate', 'ngAria', 'ngMaterial'])
        .factory('httpRequestInterceptor', function () {
            return {
                request: function (config) {
                    config.headers['Version'] = "M";
                    return config;
                }
            };
        })
        .config(function ($routeProvider, $httpProvider) {
            $httpProvider.interceptors.push('httpRequestInterceptor');
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
                .when("/editquestion/:id", {
					templateUrl: "Template/QuestionsEdit",
					controller: "QuestionEditController",
					controllerAs: "questionCtrl"
                })
                .when("/addquestion", {
                    templateUrl: "Template/QuestionsAdd",
                    controller: "QuestionAddController",
                    controllerAs: "questionCtrl"
                    })
                .when("/sections", {
                    templateUrl: "Template/Sections",
                    controller: "SectionsController",
                    controllerAs: "sectionsCtrl"
                })
                .when("/administrators", {
                    templateUrl: "Template/Administrators",
                    controller: "AdminsController",
                    controllerAs: "AdminCtrl"
                })
                .when('/saveadmin/:id', {
                	templateUrl: "Template/SaveAdmin",
                	controller: "SaveAdminController",
                	controllerAs: "saveCtrl"
                })
                .when('/addSection/:id', {
                    templateUrl: "Template/AddSection",
                    controller: "AddEditSectionController",
                    controllerAs: "vm"
                })
		        .otherwise({ redirectTo: "/tests" });
        });
        
})();