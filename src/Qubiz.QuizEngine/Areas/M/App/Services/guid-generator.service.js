(function () {
    'use strict'

    angular
        .module('quizEngineMaterial')
        .service("guidGenerator", guidGenerator);

    guidGenerator.$inject = ['$http'];

    function guidGenerator($http) {
        
        var unusedGuids = [];
        this.getNewGuid = getNewGuid;

        generateNewGuids(10);

        function generateNewGuids(number) {
            return $http({
                method: 'GET',
                url: 'api/GuidGenerator/GetGuids/',
                params: {
                    number: number
                }
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