(function () {
	quizApp.controller("TestDetailController", TestDetailController);

	TestDetailController.$inject = ['$scope', 'testData'];

	function TestDetailController($scope,testData) {
		$scope.Submit = Submit;
		$scope.Delete = Delete;

		function Submit() {
			testData.save($scope.test);
		};

		function Delete() {
			testData.delete({ id: $scope.test.ID });
		};
	};
})();