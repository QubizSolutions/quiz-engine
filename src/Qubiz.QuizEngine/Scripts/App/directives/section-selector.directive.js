(function () {
	'use strict'

	quizApp.directive("sectionSelector", sectionSelector);

	function sectionSelector() {
		var directive = {
			restrict: 'E',
			replace: true,
			templateUrl: "Templates/SectionSelector",
			scope: { selectedSections: "=", visible: "=" },
			controller: "SectionSelectorController"
		};

		return directive;
	}
})();