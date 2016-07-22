(function () {
	'use strict'

	quizApp.directive("examDetail", examDetail);

	function examDetail() {
		var directive = {
			restrict: 'E',
			replace: true,
			templateUrl: "Templates/ExamDetail",
			scope: { exam: "=" }			
		};

		return directive;
	}
})();
