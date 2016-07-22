(function () {
	quizApp.controller("ExamController", ExamController);
	
	ExamController.$inject = ['$scope', '$routeParams', '$cookies', '$timeout', '$q', 'testData', 'examData', 'questionData', 'QuestionType', '$window'];

	function ExamController($scope, $routeParams, $cookies, $timeout, $q, testData, examData, questionData, QuestionType, $window) {
		$scope.Initialize = Initialize;
		$scope.StartExam = StartExam;		
		$scope.SubmitExam = SubmitExam;
		$scope.StepToQuestion = StepToQuestion;
		$scope.StoreAnswers = StoreAnswers;
		$scope.RestoreAnswers = RestoreAnswers;
		$scope.SetCorrectOption = SetCorrectOption;
		$scope.InitTimer = InitTimer
		$scope.Status = "start";

		var disposeOnLocationChanged = $scope.$on('$locationChangeStart', function (event, next, current) {
		    var leavePagePermission = confirm("Are you sure you want to leave this page? You will be able to return to this exam anytime!");

		    if (!leavePagePermission) {
		        event.preventDefault();
		    }
		    else if ($scope.Exam)
		        $scope.StoreAnswers($scope.Exam.QuestionIndex);
		});

		window.onbeforeunload = function (event) {
		    if ($scope.Exam)
		        $scope.StoreAnswers($scope.Exam.QuestionIndex);
		    return event.returnValue;
		}

		$timeout(function () {
		    $scope.Initialize();
		});

        function Initialize() {
			$scope.Status = "start";
			$scope.Test = testData.get({ id: $routeParams.testID });
			if ($cookies.PendingExam) { var pendingExam = JSON.parse($cookies.PendingExam); }
			if (pendingExam) {
			    var resumeExam = confirm("There is an exam that has not been finished for candidate " + pendingExam.CandidateName + ". Do you wish to resume it?");

			    if (resumeExam) {
			        $scope.Exam = pendingExam;
			        $scope.StepToQuestion(0);
			        $scope.Test.MinutesAllowed = $cookies.MinutesAllowed;
			        $scope.Status = "progress";
			        $scope.InitTimer();
			    }
			    else {
			        $cookies.PendingExam = null;
			    }
			}
		}

		function InitTimer() {
		    $scope.timer = timer;
		    function timer() {
		        $scope.clock = $scope.Test.MinutesAllowed * 60;
		        $timeout(function ticker() {
		            $scope.clock--;
		            $scope.progressBar = (($scope.Test.MinutesAllowed * 60) - $scope.clock) / 36;
		            if ($scope.clock == 0)
		                $scope.SubmitExam();
		            $cookies.MinutesAllowed = JSON.stringify($scope.clock / 60);
		            $timeout(ticker, 1000);
		        }, 1000);
		    }
		    this.timer();
		}

		function StartExam() {			
			$scope.Status = "progress";
			examData.startExam({ testID: $scope.Test.ID, candidateName: $scope.CandidateName }).then(function (examData) {
				examData.QuestionIndex = 0;
				$scope.Exam = examData;
				$scope.StepToQuestion(0);
				$scope.InitTimer();
			});
		}

		function SubmitExam() {
		    disposeOnLocationChanged();
			$scope.Status = "finish";
			$scope.StoreAnswers($scope.Exam.QuestionIndex)
			$cookies.PendingExam = null;
			examData.endExam($scope.Exam).then(function (result) { if ($scope.Test.ShowScoreWhenCompleted) $scope.ExamResult = result; });
		}

		function StepToQuestion(step) {
			$scope.Exam.QuestionIndex += step;
			if ($scope.Question != null)
				$scope.StoreAnswers($scope.Exam.QuestionIndex - step);

			questionData.examQuestion({ id: $scope.Exam.Answers[$scope.Exam.QuestionIndex].QuestionID }).$promise.then(function (question) {
				$scope.Question = question;
				$scope.RestoreAnswers($scope.Exam.QuestionIndex);
			});
		}

		function StoreAnswers(index) {
			$scope.Exam.Answers[index].Answers = [];

			for (idx in $scope.Question.Options) {
				if ($scope.Question.Options[idx].IsSelected)
					$scope.Exam.Answers[index].Answers.push($scope.Question.Options[idx].ID);
			}
			$cookies.PendingExam = JSON.stringify($scope.Exam);
		}

		function RestoreAnswers(index) {

			if ($scope.Exam.Answers[index].Answers)
				for (idx in $scope.Question.Options) {
					$scope.Question.Options[idx].IsSelected = $scope.Exam.Answers[index].Answers.indexOf($scope.Question.Options[idx].ID) >= 0;
				}
		}

		function SetCorrectOption(option, Question) {
			if (Question.Type == QuestionType.SingleSelect)
				for (var i = 0; i < Question.Options.length; i++)
					Question.Options[i].IsSelected = false;
			option.IsSelected = !option.IsSelected;
		}
	}
})();