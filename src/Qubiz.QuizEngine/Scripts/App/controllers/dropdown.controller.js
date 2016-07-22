(function () {
    quizApp.controller("DropdownController", DropdownController);

    DropdownController.$inject = ['$scope', '$timeout'];

    function DropdownController($scope, $timeout) {
        
        $scope.Clicked = Clicked;
        $scope.selectedItem = "None";

        function Clicked(item) {
            if (item.content != "")
                $scope.selectedItem = item.content;
            else
                $scope.selectedItem = "None";
            $scope.scopeItem = item.type;
            $timeout(function () {
                $scope.onItemChanged();
            }, 100);          
        };
    };

})();