var migree = angular.module('migreeApp', [
    'ngRoute',
    'ui.router',
    'LocalStorageModule'
]);


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
    .state('logout', {
			url: '/logout',
			templateUrl: '/views/404.html',
			controller: function() {
        // this is where you logout :)
        console.log('Logout controller');
      }
	  })
    .state('home', {
      url: '/',
      templateUrl: '/views/start.html',
      controller: 'StartController'
    })
    .state('register', {
      url: '/register/:who',
      templateUrl: '/views/register.html',
      controller: 'registerController'
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
    .state('inbox', {
      url: '/inbox',
      templateUrl: '/views/inbox.html',
      controller: function($scope) {
        console.log('No separate controller');
      }
    })
    .state('matches', {
      url: '/matches',
      templateUrl: '/views/404.html',
      controller: function($scope) {
        console.log('No separate controller');
      }
    })
    .state('profile', {
      url: '/profile',
      templateUrl: '/views/profile.html',
      controller: 'profileController'
    })
    .state('about', {
      url: '/about',
      templateUrl: '/views/404.html',
      controller: function($scope) {
        console.log('No separate controller');
      }
    });

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

/*===functions===*/

function validateEmail(email) {
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return re.test(email);
}
