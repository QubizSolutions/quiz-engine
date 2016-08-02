(function () {
	'use strict'

	quizApp.directive("answersDetailsPrint", answersDetailsPrint);

	function answersDetailsPrint() {
		var directive = {
			restrict: 'E',
			replace: true,
			templateUrl: "Templates/AnswersDetailsPrint",
			scope: {
				exam: "=",
				expanded: "="
			}
		};

		return directive;
	}
})();
