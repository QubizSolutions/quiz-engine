(function () {
    angular.module('quizEngineMaterial')
        .controller('TestsController', TestsController)

    TestsController.$inject = ['testsService'];

    function TestsController(testsService) {
        var vm = this;
        vm.message = 'hello';

        testsService.getMessage()
            .then(function (result) {
                vm.message = result.data;
            })
            .catch(function (error) { 
                console.log('Error: ' + error.statusText);
            })

    }
})();