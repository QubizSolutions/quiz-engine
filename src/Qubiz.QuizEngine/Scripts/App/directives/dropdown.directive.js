(function () {
    'use strict'

    quizApp.directive("dropdown", dropdown);

    function dropdown() {
        var directive = {
            restrict: 'E',
            templateUrl: "Templates/Dropdown",
            scope: { scopeItem: "=selecteditem", items: "=", onItemChanged:"=event", cssClass: "@class" },
            controller: "DropdownController"
        };
        return directive;
    }
})();
