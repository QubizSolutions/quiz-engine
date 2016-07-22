(function () {
	quizApp.controller("QuestionDetailController", QuestionDetailController);

	QuestionDetailController.$inject = ['$scope', 'questionData', 'sectionData', 'guid', 'QuestionType'];

	function QuestionDetailController($scope, questionData, sectionData, guid, QuestionType) {

		$scope.Sections = sectionData.query();
		$scope.AddOption = AddOption;
		$scope.RemoveOption = RemoveOption;
		$scope.Submit = Submit;
		$scope.Delete = Delete;
		$scope.SetCorrectOption = SetCorrectOption;

		function AddOption() {
			$scope.question.Options.push({ ID: guid.newGuid(), Order: $scope.question.Options.length });
		}

		function RemoveOption(option) {
			var index = $scope.question.Options.indexOf(option)
			$scope.question.Options.splice(index, 1);
		}

		function Submit() {
			questionData.save($scope.question).$promise.then(function (number) {
				$scope.question.Number = number;
			});
		}

		function Delete() {
			questionData.delete({ id: $scope.question.ID });
			$scope.question = null;
		}

		function SetCorrectOption (option) {
		    if ($scope.question.Type == QuestionType.SingleSelect) {
		        for (var i = 0; i < $scope.question.Options.length; i++) {
		            $scope.question.Options[i].IsCorrectAnswer = false;
		        }
		    }

		    option.IsCorrectAnswer = !option.IsCorrectAnswer;
		};
	}
})();