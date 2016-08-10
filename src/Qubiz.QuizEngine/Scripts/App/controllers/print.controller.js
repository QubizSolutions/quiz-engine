(function () {
	quizApp.controller("PrintController", PrintController);

	PrintController.$inject = ['$scope', '$routeParams', '$window', 'examData', 'testData'];

	function PrintController($scope, $routeParams, $window, examData, testData) {

		var vm = this;
		vm.exam = undefined;
		vm.getData = getData;
		vm.print = print;

		getData();

		function print() {
			$window.print();
		}

		function getData() {
			examData.getExam($routeParams.examID).then(function (exam) {
				vm.exam = exam;
			});
		}
	}
})();