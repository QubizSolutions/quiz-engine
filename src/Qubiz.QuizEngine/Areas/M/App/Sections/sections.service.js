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

		function getSectionsSuccess(response) {
		    return response.data;
		}

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

		function deleteSection(id) {
			return $http.delete('api/Section/Delete/' + id)
                .then(function () {
                    console.log('Section Deleted !');
                    getAllSections();
                })
                .catch(errorCallBack);
		}

		function addSection(section) {
		    return $http({
		        method: 'POST',
		        url: '/section' + section.ID,
                data: section,
		    })
                .then(getSectionsSuccess)
		        .catch(errorCallBack)
		}

		function readSection(id) {
		    return $http({
		        method: 'GET',
		        url: '/section/get/' + id,

		    })
                .then(getSectionsSuccess)
		        .catch(errorCallBack);
		}

		function editSection(id, section) {
		    return $http({
		        method: 'PUT',
		        url: '/section/edit/' + id,
		        data: section,
		    })
                .catch(getSectionsSuccess)
                .catch(errorCallBack);
		}
	}
}) ();