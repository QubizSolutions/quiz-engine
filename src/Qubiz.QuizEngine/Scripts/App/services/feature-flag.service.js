(function () {
	'use strict'

	quizApp.factory("featureFlagData", featureFlagData);

	featureFlagData.$inject = ['$http'];

	function featureFlagData($http) {
		return {
			getByID: function (data) {
				var request = $http.get("api/FeatureFlag/id", {
					params: { id: data }
				}).then(function (response) {
					var result = response.data;
					return result;
				});
				return request;
			},
			get: function () {
				var request = $http.get("api/FeatureFlag");
				return request;
			}
		}
	}
})()