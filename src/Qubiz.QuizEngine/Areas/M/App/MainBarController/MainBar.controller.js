(function () {
	'use strict'
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
})();