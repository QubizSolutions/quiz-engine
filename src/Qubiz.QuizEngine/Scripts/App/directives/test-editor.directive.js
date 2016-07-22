(function () {
	'use strict'

	quizApp.directive("testEditor", testEditor);

	function testEditor() {
		var directive  ={
			restrict: 'E',
			replace: true,
			templateUrl: "Templates/TestEditor",
			scope: { test: "=" },
			controller: "TestDetailController"
		};

		return directive;
	}
})();