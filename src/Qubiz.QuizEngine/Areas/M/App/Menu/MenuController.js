(function () {
    'use strict';

    angular.module('quizEngineMaterial')
        .controller('MenuController', MenuController);

    function MenuController($scope) {
        $scope.currentNavItem = 'page1';
    }
})();