var migree = angular.module('migreeApp', [
    'ngRoute',
    'ui.router',
    'LocalStorageModule'
]);

migree.constant('config', {
  api: '/'
})

migree.config(function ($routeProvider, $locationProvider, $stateProvider, $urlRouterProvider) {

    //routing DOESN'T work without html5Mode
    $locationProvider.html5Mode({
      enabled: true,
      requireBase: false
    });


  $stateProvider
    .state('login', {
			url: '/login',
			templateUrl: '/views/login.html',
			controller: 'loginController'
	  })
    .state('home', {
      url: '/',
      templateUrl: '/views/start.html',
      controller: 'StartController'
    })
    .state('register', {
      url: '/register/:who',
      templateUrl: '/views/register.html',
      controller: 'RegisterController'
    })
    .state('dashboard', {
      url: '/dashboard',
      templateUrl: '/views/dashboard.html',
      controller: 'DashboardController'
    })
    .state('forgot', {
      url: '/forgot',
      templateUrl: '/views/forgot.html',
      controller: 'ForgotController'
    })
    .state('notfound', {
      url: '/notfound',
      templateUrl: '/views/404.html',
      controller: function($scope) {
        // do something here?
      }
    })

    $urlRouterProvider.otherwise('/404');
});

var serviceBase = 'http://migree.azurewebsites.net/';
migree.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

migree.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

migree.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

migree.directive('fileUploadChange', [function() {
    'use strict';
    return {
        restrict: "A",
        scope: {
            handler: '&'
        },
        link: function(scope, element) {
            element.change(function(event) {
                scope.$apply(function() {
                 // console.log(event);
                    var params = {event: event, el: element};
                    scope.handler({params: params});
                });
            });
        }
    };
}]);


/*===functions===*/

function validateEmail(email) {
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return re.test(email);
}
