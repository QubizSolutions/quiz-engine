(function () {
	'use strict'

	quizApp.factory("testData", testData);

	testData.$inject = ['$resource'];

	function testData($resource) {
		return $resource("api/Test", {}, { save: { method: 'POST' }, query: { method: 'GET', isArray: false } });
	}
})()