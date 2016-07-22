(function () {
	quizApp.controller("TestListController", TestListController);

	TestListController.$inject = ['$scope', 'testData', 'guid'];

	function TestListController($scope,testData, guid) {		
		$scope.Tests = testData.query();
		$scope.Filter = "";
		$scope.SelectedTest = null;

		$scope.SearchTests = SearchTests;
		$scope.CreateTest = CreateTest;
		$scope.SelectedTestChanged = SelectedTestChanged;

		function SearchTests() {
			$scope.Tests = testData.query({ filter: $scope.Filter });
		};

		function CreateTest() {			
			$scope.SelectedTest = { ID: guid.newGuid(), NumberOfQuestions: 20, QuestionsSelectedRandomly: true, MinutesAllowed: 60, Sections: [] };
		};

		function SelectedTestChanged(test) {
			$scope.SelectedTest = testData.get({ id: test.ID });
		};
	};
})();