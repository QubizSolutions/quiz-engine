(function () {
	angular.module('quizEngineMaterial')
        .service('testsService', testsService)

	testsService.$inject = ['$http'];

	function testsService(http) {
	    this.getMessage = getMessage;
	    this.getMessageById = getMessageById;

	    function getMessage() {
	        return http.get('api/tests/getMessage')
                    .then(function (result) {
                        return result;
                    });
	    }

	    function getMessageById(id) {
	        return http.get('api/tests/getMessage/'+ id)
                    .then(function (result) {
                        return result;
                    });
	    }
	}
})();