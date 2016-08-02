
(function(){
	'use strict'

	angular.module('quizEngineMaterial', ['ngRoute', 'ngAnimate', 'ngAria', 'ngMaterial']);


	angular.module('quizEngineMaterial').config(function ($routeProvider) {

	    $routeProvider.when("tests", {
            controller: "TestsController",
		    controllerAs: "tests",
		    templateUrl: "M/Templates/Test"  
            })          
	    .otherwise({ redirectTo: "/tests" });
}); 
})()