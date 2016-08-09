(function () {
    quizApp.controller("PrintController", PrintController);

    PrintController.$inject = ['$scope', '$routeParams', 'examData', 'testData'];

    function PrintController($scope, $routeParams, examData, testData) {

        var vm = this;
        vm.getData = getData;
        getData();

        function getData() {
            examData.getExam($routeParams.examID).then(function (data) {
                $scope.SelectedExam = data;
            });
        }
        $scope.print = function (print) {
        	$window.print();
        };

    }
})();