(function () {
    quizApp.controller("PrintController", PrintController);

    PrintController.$inject = ['printer', '$scope', '$routeParams', 'examData', 'testData'];

    function PrintController(printer, $scope, $routeParams, examData, testData) {

        var vm = this;
        vm.getData = getData;
        vm.printAll = printAll;
        vm.grabHTML = grabHTML;
        getData();
        
        function grabHTML() {
            var ceva = $('#detail').text();
            console.log(ceva);
            //printer.printHTML(ceva);
            printer.print("Templates/PrintPreview", {SelectedExam: $scope.SelectedExam});
        }

        function getData() {
            examData.getExam($routeParams.examID).then(function (data) {
                $scope.SelectedExam = data;
                grabHTML();
            });
        }

        function printAll() {
            var doc = new jsPDF();
            var elementHandler = {
                '#ignorePDF': function (element, renderer) {
                    return true;
                }
            };
            var source = window.document.getElementsByTagName("body")[0];
            doc.fromHTML(
                source,
                15,
                15,
                {
                    'width': 180, 'elementHandlers': elementHandler
                });

            doc.output("dataurlnewwindow");
        }
    }
})();