(function () {
	'use strict'

	quizApp.factory("sectionData", sectionData);

	sectionData.$inject = ['$resource'];

	function sectionData($resource) {
		return $resource("api/Sections", {}, { save: { method: 'POST', isArray: true } });
	}
})()