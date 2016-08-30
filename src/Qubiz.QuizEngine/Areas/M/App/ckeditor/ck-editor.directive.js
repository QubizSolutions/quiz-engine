(function () {
	'use strict'

	angular
        .module('quizEngineMaterial').directive('ckEditor', ckEditor);
	
	function ckEditor() {
		var directive = {
			require: '?ngModel',
			link: link,
		};

		return directive;
			
		function link(scope, elm, attr, ngModel) {
				CKEDITOR.disableAutoInline = true;
				var ck = CKEDITOR.inline(elm[0]);
				if (!ngModel) return;

				ck.on('pasteState', function () {
					scope.$apply(function () {
						ngModel.$setViewValue(ck.getData());
					});
				});

				ngModel.$render = function (value) {
					ck.setData(ngModel.$viewValue);
				};
			}
		}
})();