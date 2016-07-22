(function () {
	quizApp.controller("QuestionSelectorController", QuestionSelectorController);

	QuestionSelectorController.$inject = ['$scope', 'questionData'];

	function QuestionSelectorController($scope, questionData) {		
		$scope.SelectionChanged = SelectionChanged;
		$scope.mergeQuestionsWithSelectedOnes = mergeQuestionsWithSelectedOnes;

		activate();

		function activate() {
			return questionData.filteredQuestion({ sectionId: "", type: "", complexity: "", filter: "" }).$promise.then(function (questions) {
				$scope.Questions = questions;
				$scope.mergeQuestionsWithSelectedOnes();
			});
		}

		function SelectionChanged(question) {
			if ($scope.selectedQuestions == null) $scope.selectedQuestions = [];

			if (question.IsSelected)
				$scope.selectedQuestions.push({ QuestionID: question.ID });
			else
				$scope.selectedQuestions = $scope.selectedQuestions.filter(function (i) {
					return i.QuestionID != question.ID;
				});
		}

		$scope.$watch('selectedQuestions', function (newValue, oldValue, scope) {
			$scope.mergeQuestionsWithSelectedOnes();
		});

		function mergeQuestionsWithSelectedOnes() {
			var enumerable = Enumerable.From($scope.selectedQuestions);
			for (idx in $scope.Questions) {
				var question = $scope.Questions[idx];
				question.IsSelected = enumerable.Any(function (s) { return s.QuestionID == question.ID });
			}
		}
	}
})();