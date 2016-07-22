var quizApp = angular.module('quizApp', ['ngRoute', 'ngResource', 'ngCookies', 'ngSanitize'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/Sections',
            {
                templateUrl: 'Templates/Sections',
                controller: 'SectionController'
            })
            .when('/Admins',
           {
               templateUrl: 'Templates/Admins',
               controller: 'AdminController'
           })
            .when('/Questions',
            {
                templateUrl: 'Templates/QuestionList',
                controller: 'QuestionListController'
            })
            .when('/Tests',
           {
               templateUrl: 'Templates/TestList',
               controller: 'TestListController'
           })
            .when('/Exams',
          {
              templateUrl: 'Templates/ExamList',
              controller: 'ExamListController'
          })
            .when('/StartExam/:testID',
           {
               templateUrl: 'Templates/TakeExam',
               controller: 'ExamController'
           })
            .otherwise(
            {
                redirectTo: '/Tests'
            });
    });
