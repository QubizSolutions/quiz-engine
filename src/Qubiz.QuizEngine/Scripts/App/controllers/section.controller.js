(function () {
	quizApp.controller("SectionController", SectionController);	

	SectionController.$inject = ['$scope','sectionData'];

	function SectionController($scope,sectionData) {
		$scope.Sections = sectionData.query();
		$scope.AddSection = AddSection;
		$scope.RemoveSection = RemoveSection;
		$scope.SubmitSections = SubmitSections;

		function AddSection() {
			$scope.Sections.push({ Name: "" });
		}

		function RemoveSection(section) {
			$scope.Sections.splice($scope.Sections.indexOf(section), 1);
		}

		function SubmitSections() {
			sectionData.save($scope.Sections)
		}
	}
})();