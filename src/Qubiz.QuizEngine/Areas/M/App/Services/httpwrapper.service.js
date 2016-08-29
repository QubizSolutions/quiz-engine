(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("httpWrapperService", httpWrapperService);

    httpWrapperService.$inject = ['$http', '$q'];

    function httpWrapperService($http, $q) {

        this.get = get;
        this.post = post;
        this.put = put;
        this.delete = del;

        function get(getUrl, data) {
            var defer = $q.defer();
            $http.get(getUrl)
                .then(function(response) {
                    defer.resolve(response.data);
                },function(response) {
                    defer.reject(response);
                });
            return defer.promise;
            }

        function post(postUrl, data) {
            var defer = $q.defer();
            $http.post(postUrl + data.ID)
                 .then(function(response) {
                     defer.resolve(response.data);
                 },function (response) {
                     defer.reject(response);
                 });
            return defer.promise;
        }

        function put(putUrl, data) {
            var defer = $q.defer();
            $http.put(putUrl + data.ID)
                .then(function(response) {
                    defer.resolve(response.data);
                },function (response) {
                    defer.reject(response);
                });
            return defer.promise;
        }

        function del(deleteUrl, data) {
            var defer = $q.defer();
            $http.delete(deleteUrl + data.ID)
              .then(function(response) {
                  defer.resolve(response.data);
              },function (response) {
                  defer.reject(response);
              });
            return defer.promise;
        }
    }
})();