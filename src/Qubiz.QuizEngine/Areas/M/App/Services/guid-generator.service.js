(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("guidGenerator", guidGenerator);

    guidGenerator.$inject = ['$http'];

    function guidGenerator($http) {
        
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