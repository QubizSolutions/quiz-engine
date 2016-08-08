(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("guidsService", guidsService);

    guidsService.$inject = ['$http'];

    function guidsService($http) {
        
        this.getGuid = getGuid;
        this.loadGuids = loadGuids;

        var guidsCache = [];

        generateNewGuids();

        function getGuid() {
            if (guidsCache.length <= 2) {
                loadGuids();
            }
            return guidsCache.shift();
        }

        function loadGuids() {
            $http({
                method: 'GET',
                url: 'api/guids/'
            }).then(function (guids) {
                guidsCache = guids;
            });
        }
    }
})();