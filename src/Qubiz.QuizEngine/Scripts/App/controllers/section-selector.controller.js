(function () {
	quizApp.controller("SectionSelectorController", SectionSelectorController);

	SectionSelectorController.$inject = ['$scope', 'sectionData'];

	function SectionSelectorController($scope, sectionData) {		
		$scope.SelectionChanged = SelectionChanged;
		$scope.mergeSectionsWithSelectedOnes = mergeSectionsWithSelectedOnes;

		activate();

		function activate() {
			return sectionData.query().$promise.then(function (sections) {
				$scope.Sections = sections;
				$scope.mergeSectionsWithSelectedOnes();
			});
		}

		function SelectionChanged(section) {
			if ($scope.selectedSections == null) $scope.selectedSections = [];

			if (section.IsSelected)
				$scope.selectedSections.push({ SectionID: section.ID });
			else
				$scope.selectedSections = $scope.selectedSections.filter(function (i) {
					return i.SectionID != section.ID;
				});
		}

		$scope.$watch('selectedSections', function (newValue, oldValue, scope) {
			$scope.mergeSectionsWithSelectedOnes();
		});

		function mergeSectionsWithSelectedOnes() {
			var enumerable = Enumerable.From($scope.selectedSections);
			for (idx in $scope.Sections) {
				var section = $scope.Sections[idx];
				section.IsSelected = enumerable.Any(function (s) { return s.SectionID == section.ID });
			}
		}
	}
})();