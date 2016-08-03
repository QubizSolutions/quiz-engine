
(function () {
	'use strict'

	angular.module('quizEngineMaterial', ['ngRoute', 'ngAnimate', 'ngAria', 'ngMaterial']);

	angular.module('quizEngineMaterial').controller('mainController', function ($location, $scope) {

	    $scope.testsTabClick = function () {
	        $location.path('/tests');
	    }
	    $scope.examsTabClick = function () {
	        $location.path('/exams');
	    }
	    $scope.questionsTabClick = function () {
	        $location.path('/questions');
	    }
	    $scope.sectionsTabClick = function () {
	        $location.path('/sections');
	    }
	    $scope.administratorsTabClick = function () {
	        $location.path('/administrators');
	    }
	    
	});
	angular.module('quizEngineMaterial').config(function ($routeProvider) {

		$routeProvider.when("/tests", {
			templateUrl: "Template/Test"
		})
            .when("/exams", {
                templateUrl:"Template/Exams"
            })
            .when("/questions", {
                templateUrl:"Template/Questions"
            })
            .when("/sections", {
                templateUrl:"Template/Sections"
            })
            .when("/administrators", {
                templateUrl:"Template/Administrators"
            })
		.otherwise({ redirectTo: "/tests" });
	});
})();
