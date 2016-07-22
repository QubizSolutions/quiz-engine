(function () {
	quizApp.controller("QuestionListController", QuestionListController);

	QuestionListController.$inject = ['$scope', 'questionData', 'guid', 'sectionData','dropdownData'];

	function QuestionListController($scope, questionData, guid, sectionData, dropdownData) {
        $scope.SectionId = "";
		$scope.Filter = "";
        $scope.complexity = "";
	    $scope.type = "";		
		$scope.pageNumber = 0;						
		$scope.goToPage = goToPage;
		$scope.onFilterChanged = onFilterChanged;
		$scope.SelectedQuestion = null;
		$scope.CreateQuestion = CreateQuestion;
		$scope.SelectedQuestionChanged = SelectedQuestionChanged;
	    $scope.Types = dropdownData.getTypes();
	    $scope.Complexities = dropdownData.getComplexities();
	    getQuestions();
	    $scope.sections = [
            { content: "All Sections", type: ""}
	    ];
	    $scope.maxPages = 0;
        sectionData.query({}, 
            function(data) { 
	            for (var index = 0; index <data.length; index++) {         
	                var obj = { content: data[index].Name, type: data[index].ID };
                    $scope.sections.push(obj);
                }
            });

        function onFilterChanged() {
            $scope.pageNumber = 0;
            getQuestions();
        }

        function goToPage(value) {
            $scope.pageNumber = $scope.pageNumber + value;            
		    getQuestions();
		}

        function getQuestions() {
            if ($scope.pageNumber > $scope.maxPages) { $scope.pageNumber = $scope.maxPages; }
            if ($scope.pageNumber < 0) { $scope.pageNumber = 0; }
            questionData.filteredQuestion({ sectionId: $scope.SectionId, type: $scope.type, complexity: $scope.complexity, filter: $scope.Filter, pagenumber: $scope.pageNumber }).$promise.then(function (result) {
                $scope.Questions = result;
                $scope.maxPages = Math.ceil($scope.Questions.TotalCount / 20) - 1;
            });
		}		

		function CreateQuestion() {
			$scope.SelectedQuestion = {
				ID: guid.newGuid(),
				Number: 0,
				Type: 0,
				Complexity: 5,
			Options: [{ID: guid.newGuid(), order: 0 }, { ID: guid.newGuid(), order: 1 }, { ID: guid.newGuid(), order: 2}], IsNew : true};
		}

		function SelectedQuestionChanged(question) {
			$scope.SelectedQuestion = questionData.get({ id: question.ID });
		}
	}
})();