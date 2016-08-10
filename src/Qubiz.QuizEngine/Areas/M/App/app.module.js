(function () {
    'use strict'

    angular
        .module('quizEngineMaterial', ['ngRoute', 'ngAnimate', 'ngAria', 'ngMaterial'])
		.run(function($rootScope) {
			$rootScope.activeTab = 0;
		})
        .config(function ($routeProvider) {
        	$routeProvider
                .when("/tests", {
                	templateUrl: "Template/Test",
                	resolve: {
                		"activeTab": function ($rootScope) {
                			$rootScope.activeTab = 0;
                		}
                	}
                })
                .when("/exams", {
                	templateUrl: "Template/Exams",
                	resolve: { 
                		"activeTab" : function($rootScope) {
                			$rootScope.activeTab = 1;
                		}
                	}
                })
                .when("/questions", {
					templateUrl: "Template/Questions",
					controller: "QuestionListController",
					controllerAs: "questionCtrl",
					resolve: {
						"activeTab": function ($rootScope) {
							$rootScope.activeTab = 2;
						}
					}
                })
                .when("/sections", {
                    templateUrl: "Template/Sections",
                    controller: "SectionsController",
                    controllerAs: "SectionsCtrl",
                    resolve: {
                    	"activeTab": function ($rootScope) {
                    		$rootScope.activeTab = 3;
                    	}
                    }
                })
                .when("/administrators", {
                	templateUrl: "Template/Administrators",
                	controller: "AdminsController",
                	controllerAs: "AdminCtrl",
                	resolve: {
                		"activeTab": function ($rootScope) {
                			$rootScope.activeTab = 4;
                		}
                	}
                })
                .when('/addadmin', {
                	templateUrl: "Template/AddAdmin",
                	controller: "AddAdminController",
                	controllerAs: "AddAdminCtrl"
                })
                .when('/editadmin/:id',
                {
                	templateUrl: "Template/EditAdmin",
                	controller: "EditAdminController",
                	controllerAs: "EditCtrl"
                })
		        .otherwise({ redirectTo: "/tests" });
        });
})();