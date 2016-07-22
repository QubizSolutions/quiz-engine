(function () {
	'use strict'

	quizApp.directive("answersDetails", answersDetails);

	function answersDetails() {
		var directive = {
			restrict: 'E',
			replace: true,
			templateUrl: "Templates/AnswersDetails",
			scope: {
				exam: "=",
				expanded: "="
			}
		};

		return directive;
	}
})();
