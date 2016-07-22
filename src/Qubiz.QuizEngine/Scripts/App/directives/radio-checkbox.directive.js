(function () {
	'use strict'

	quizApp.directive("radioCheckbox", radioCheckbox);

	radioCheckbox.$inject = ['$compile'];

	function radioCheckbox($compile) {
		var directive = {
			restrict: 'E',
			replace: true,
			link: link,
			scope: {
				option: "=", 
				allOptions: "=", 
				propertyToBind: "@", 
				type: "=",
				optionId: "="
			},
			controller: radioCheckboxController
		};

		return directive;
			
		function link(scope, element, attrs, controller) {
			var markup = "<label><input name='" + attrs.name + "' ng-attr-type='{{type | questionType}}' ng-click='ValueChanged(option)' ng-checked='option." + attrs.propertyToBind + "'class='" + attrs.class + "' id='id_" + scope.optionId + "'/></label>";
				angular.element(element).replaceWith($compile(markup)(scope));
		}
		
		function radioCheckboxController($scope) {
			var wm = this;
			wm.ValueChanged = ValueChanged;
			$scope.$watch("type", function (newValue, oldvalue) {
				if (newValue != oldvalue && wm.type == 0)
					wm.option[wm.propertyToBind] = false;
			});

			function ValueChanged(newValue) {
				if (wm.type == 0)
					for (idx in wm.allOptions) {
						wm.allOptions[idx][wm.propertyToBind] = wm.allOptions[idx] == newValue;
					}
				else wm.option[wm.propertyToBind] = !wm.option[wm.propertyToBind];
			};
		}
	}
})();