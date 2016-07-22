(function () {
	quizApp.controller("AdminController", AdminController);

	AdminController.$inject = ['$scope','adminData'];

	function AdminController($scope,adminData) {		
		$scope.Admins = adminData.query();
		$scope.AddAdmin = AddAdmin;
		$scope.RemoveAdmin = RemoveAdmin;
		$scope.SubmitAdmins = SubmitAdmins;

		function AddAdmin() {
			$scope.Admins.push({ Name: "" });
		}

		function RemoveAdmin(admin) {
			$scope.Admins.splice($scope.Admins.indexOf(admin), 1);
		}

		function SubmitAdmins() {
			adminData.save($scope.Admins)
		}
	}
})();