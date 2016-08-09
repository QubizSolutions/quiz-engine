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
                	templateUrl: "Template/Administrators",
                	controller: "AdminsController",
                	controllerAs: "AdminCtrl"
                })
                .when('/saveadmin/:id', {
                	templateUrl: "Template/SaveAdmin",
                	controller: "SaveAdminController",
                	controllerAs: "saveCtrl"
                })
		        .otherwise({ redirectTo: "/tests" });
        });
})();