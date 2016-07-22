(function () {
	'use strict'

	quizApp.directive("questionEditor", questionEditor);

	function questionEditor() {
		var directive = {
			restrict: 'E',
			replace: true,
			templateUrl: "Templates/QuestionEditor",
			scope: { question: "=" },
			controller: "QuestionDetailController"
		};

		return directive;
	};
})();