(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("httpWrapperService", httpService);

    httpWrapperService.$inject = ['$http', '$q'];

    function httpService($http, $q) {

        this.get = get;
        this.post = post;
        this.put = put;
        this.del = del;

        function get(url, data) {
            var defer = $q.defer();

            $http.get(url)
                .then(function (response) {
                    defer.resolve(response.data);
                }, function (response) {
                    defer.reject(response);
                });

            return defer.promise;
        }

        function post(url, data) {
            var defer = $q.defer();

            $http.post(url + data.ID)
                 .then(function (response) {
                     defer.resolve(response.data);
                 }, function (response) {
                     defer.reject(response);
                 });

            return defer.promise;
        }

        function put(url, data) {
            var defer = $q.defer();

            $http.put(url + data.ID)
                .then(function (response) {
                    defer.resolve(response.data);
                }, function (response) {
                    defer.reject(response);
                });

            return defer.promise;
        }

        function del(url, data) {
            var defer = $q.defer();

            $http.delete(url + data.ID)
              .then(function (response) {
                  defer.resolve(response.data);
              }, function (response) {
                  defer.reject(response);
              });

            return defer.promise;
        }
    }
})();