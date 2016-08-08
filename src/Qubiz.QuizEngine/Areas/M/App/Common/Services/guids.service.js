(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("guidsService", guidsService);

    guidsService.$inject = ['$http'];

    function guidsService($http) {
        
        var unusedGuids = [];
        this.getNewGuid = getNewGuid;

        generateNewGuids();

        function generateNewGuids() {
            $http({
                method: 'GET',
                url: 'api/Guids/'
            }).then(function (result) {
                this.unusedGuids = result;
            });
        }

        function getNewGuid() {
            var result = unusedGuids.pop();
            if (unusedGuids.length == 0) {
                generateNewGuids();
            }
            return result;
        }
    }
})();