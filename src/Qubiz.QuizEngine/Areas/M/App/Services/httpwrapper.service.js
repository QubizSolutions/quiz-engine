(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("wrapperService", wrapperService);

    wrapperService.$inject = ['$http', '$q'];

    function wrapperService($http, $q) {

        this.get = get;
        this.getById = getById;
        this.post = post;
        this.put = put;
        this.delete = newDelete;
        var deferred = $q.defer();

        function get(getUrl) {
            return $http.get(getUrl)
            .then(function (response) {
                deferred.resolve(response.data);
                return deferred.promise;
            }, function (response) {
                deferred.reject(response);
                return deferred.promise;
            });
        };

        function getById(getUrl, data) {
            return $http.get(getUrl + data.ID)
                .then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
        }

        function post(postUrl, data) {
            return $http.post(postUrl + data.ID)
           .then(function (response) {
               deferred.resolve(response.data);
               return deferred.promise;
           }, function (response) {
               deferred.reject(response);
               return deferred.promise;
           });
        }

        function put(puttUrl, data) {
            return $http.put(putUrl + data.ID)
             .then(function (response) {
                 deferred.resolve(response.data);
                 return deferred.promise;
             }, function (response) {
                 deferred.reject(response);
                 return deferred.promise;
             });
        }

        function newDelete(deletetUrl, data) {
            return $http.delete(deletetUrl + data.ID)
             .then(function (response) {
                 deferred.resolve(response.data);
                 return deferred.promise;
             }, function (response) {
                 deferred.reject(response);
                 return deferred.promise;
             });
        }
    }
})();