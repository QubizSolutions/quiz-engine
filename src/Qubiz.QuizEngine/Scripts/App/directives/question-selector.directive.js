(function () {
	'use strict'

	quizApp.directive("questionSelector", questionSelector);

	function questionSelector() {
		var directive = {
			restrict: 'E',
			replace: true,
			templateUrl: "Templates/QuestionSelector",
			scope: { selectedQuestions: "=", visible: "=" },
			controller: "QuestionSelectorController"
		};

		return directive;
	};
})();