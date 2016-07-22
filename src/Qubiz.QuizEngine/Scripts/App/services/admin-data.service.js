(function () {
	'use strict'

	quizApp.factory('adminData', adminData);

	adminData.$inject = ['$resource'];

	function adminData ($resource){
		return $resource("api/Admin", {}, { save: { method: 'POST', isArray: true } });
	}
})()