(function () {
	'use strict'

	quizApp.factory('examData', examData);

	examData.$inject = ['$http', '$q', '$timeout'];

	function examData($http, $q, $timeout) {
		return {
			startExam: function (data) {
				var deferred = $q.defer();
				$http.post('api/Exam/TakeExam', data)
					.success(function (data) { deferred.resolve(data); })
					.error(function (data, status) { deferred.reject(status) });
				return deferred.promise;
			},
			endExam: function (data) {
				var deferred = $q.defer();
				$http.post('api/Exam/EndExam', data)
					.success(function (data) { deferred.resolve(data); })
					.error(function (data, status) { deferred.reject(status) });
				return deferred.promise;
			},
			get: function (nameFilter, dateSort, scoreSort, testIDFilter, pageNumber) {
                var request = $http({
					method: "get",
					url: "api/Exam/Get",
					params: {
					    action: "get",
					    dateSort: dateSort,
					    scoreSort: scoreSort,
						nameFilter: nameFilter,					
						testIDFilter: testIDFilter,
                        pageNumber: pageNumber
					}
				}).then(function (response) {
					return response.data;
				});
				return request;
			},
			getExam: function (data) {
				var request = $http({
					method: "get",
					url: "api/Exam/GetExam",
					params: {
						action: "get",
						id: data
					}
				}).then(function (response) {
					return response.data;
				});
				return request;
			}
		}
	}
})()