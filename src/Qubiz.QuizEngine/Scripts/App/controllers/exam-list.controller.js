(function () {
    quizApp.controller("ExamListController", ExamListController);

    ExamListController.$inject = ['$scope', 'examData', 'testData'];

    function ExamListController($scope, examData, testData) {
        $scope.nameFilter = "";
        $scope.SelectedExam = null;
        $scope.SelectedExamChanged = SelectedExamChanged;
        $scope.onItemChanged = onItemChanged;
        $scope.selectedItemDate = "";
        $scope.selectedItemScore = "";
        $scope.selectedItemTitle = "";
        $scope.TotalExamsCount = 0;
        $scope.dateSortDropdown = [
            { content: "Descendant", type: 0 },
            { content: "Ascendant", type: 1 },
            { content: "None", type: ""}
        ];
        $scope.scoreSortDropdown =[
            { content: "Descendant", type: 0 },
            { content: "Ascendant", type: 1 },
            { content: "None", type: ""}
        ];
        $scope.examNameDropdown =[];
        $scope.pageNumber = 1;
        $scope.maxPage = 1;
        $scope.examsCount = 0;
        $scope.getExams = getExams;
        $scope.goToPage = goToPage;

        activate();
        initTests();

        function onItemChanged() {
            $scope.pageNumber = 1;
            getExams();
        };

        function getExams() {
            if ($scope.pageNumber > $scope.maxPage || $scope.pageNumber < 1) $scope.pageNumber = $scope.maxPage;

            examData.get($scope.nameFilter, $scope.selectedItemDate, $scope.selectedItemScore, $scope.selectedItemTitle, $scope.pageNumber - 1).then(function (data) {
                $scope.Exams = data.Items;
                $scope.examsCount = data.TotalCount;
                $scope.maxPage = Math.ceil($scope.examsCount / 20);
            });
        };

        function goToPage(value) {
            $scope.pageNumber += value;
            getExams();
        };

        function initTests() {
            testData.query({}, function (data) {
                for (var i = 0; i < data.Items.length; i++) {
                    if($scope.examNameDropdown.map(function (e) { return e.content }).indexOf(data.Items[i].Title) == -1)
                        $scope.examNameDropdown.push({
                            content: data.Items[i].Title, type: data.Items[i].ID
                        });
                    }
                $scope.examNameDropdown.push({
                    content: "None", type: ""
                });
            });
        };

        function activate() {
            return examData.get().then(function (data) {
                $scope.Exams = data.Items;
                $scope.examsCount = data.TotalCount;
                $scope.TotalExamsCount = $scope.examsCount;
                $scope.maxPage = Math.ceil($scope.examsCount / 20);
            });
        }

        function SelectedExamChanged(exam) {
            examData.getExam(exam.ID).then(function (data) {
                $scope.SelectedExam = data;
            });
        }
    }
})();