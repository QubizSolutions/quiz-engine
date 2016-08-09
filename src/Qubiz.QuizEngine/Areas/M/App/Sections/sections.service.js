(function () {
	'use strict'

	angular
        .module('quizEngineMaterial')
        .service('sectionsDataService', sectionsDataService)

	sectionsDataService.$inject = ['$http', '$q'];

	function sectionsDataService($http, $q) {

		this.getAllSections = getAllSections;
		this.deleteSection = deleteSection;

		function getAllSections() {
			return $http({
				method: 'GET',
				url: 'api/Section'
			})
				.then(getSectionsSuccess)
				.catch(errorCallBack)
		}

		function errorCallBack(response) {
			return $q.reject('HTTP status : ' + response.status + ' ' + response.statusText);
		}

		function getSectionsSuccess(response) {
			return response.data;
		}

		function deleteSection(id) {
			return $http.delete('api/Section/DeleteSection/' + id)
                .then(deletedSuccess)
                .catch(errorCallBack);
		}

		function deletedSuccess(response) {
			return console.log("Section Deleted !");
		}
	}
}) ();