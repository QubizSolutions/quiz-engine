(function () {
	'use strict'

	angular
        .module('quizEngineMaterial')
        .service('sectionsDataService', sectionsDataService)

	sectionsDataService.$inject = ['$http', '$q'];

	function sectionsDataService($http, $q) {

		this.getAllSections = getAllSections;
		this.deleteSection = deleteSection;
		this.readSection = readSection;
		this.addSection = addSection;
		this.editSection = editSection;

		function getAllSections() {
		    return $http({
		        method: 'GET',
		        url: 'api/section'
		    });
		}

		function deleteSection(id) {
			return $http.delete('api/section/delete/' + id)
                .catch(errorCallBack);
		}

		function addSection(section) {
		    return $http({
		        method: 'POST',
		        url: 'api/section/post/' + section.ID,
                data: section
		    })
                .then(getSectionsSuccess)
		        .catch(errorCallBack)
		}

		function readSection(id) {
		    return $http({
		        method: 'GET',
		        url: 'api/section/get/' + id
		    })
                .then(getSectionsSuccess)
		        .catch(errorCallBack);
		}

		function editSection(id, section) {
		    return $http({
		        method: 'PUT',
		        url: 'api/section/put/' + id,
		        data: section
		    })
                .catch(getSectionsSuccess)
                .catch(errorCallBack);
		}

		function getSectionsSuccess(response) {
		    return response.data;
		}

		function errorCallBack(response) {
		    return $q.reject('HTTP status : ' + response.status + ' ' + response.statusText);
		}
	}
}) ();