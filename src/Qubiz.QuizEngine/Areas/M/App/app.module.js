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
                .when('/addSection/:id', {
                    templateUrl: "Template/AddSection",
                    controller: "AddEditSectionController",
                    controllerAs: "vm"
                })
		        .otherwise({ redirectTo: "/tests" });
        });
        
})();