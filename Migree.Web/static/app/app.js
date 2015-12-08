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
    .state('home', {
      url: '/',
      templateUrl: '/views/start.html',
      controller: 'StartController',
      data: {
        requireLogin: false
      }
    })
    .state('register', {
      url: '/register/:who',
      templateUrl: '/views/register.html',
      controller: 'registerController',
      data: {
        requireLogin: false
      }
    })
    .state('thankyou', {
      url: '/thankyou',
      templateUrl: '/views/thankyou.html',
      controller: 'thankYouController',
      data: {
        requireLogin: false
      }
    })
    .state('login', {
			url: '/login',
			templateUrl: '/views/login.html',
			controller: 'loginController',
      data: {
        requireLogin: false
      }
	  })
    .state('logout', {
			url: '/logout',
			templateUrl: '/views/404.html',
			controller: function() {
        
      },
      data: {
        requireLogin: true
      }
	  })
    .state('dashboard', {
      url: '/dashboard',
      templateUrl: '/views/dashboard.html',
      controller: 'DashboardController',
      data: {
        requireLogin: true
      }
    })
    .state('forgot', {
      url: '/forgot',
      templateUrl: '/views/forgot.html',
      controller: 'ForgotController',
      data: {
        requireLogin: false
      }
    })
    .state('notfound', {
      url: '/notfound',
      templateUrl: '/views/404.html',
      controller: function($scope) {
        // do something here?
      },
      data: {
        requireLogin: false
      }
    })
    .state('inbox', {
      url: '/inbox',
      templateUrl: '/views/inbox.html',
       controller: 'inboxController',
       data: {
        requireLogin: true
      }
    })
    .state('matches', {
      url: '/matches',
      templateUrl: '/views/404.html',
      controller: function($scope) {
        console.log('No separate controller');
      },
      data: {
        requireLogin: true
      }
    })
    .state('profile', {
      url: '/profile',
      templateUrl: '/views/profile.html',
      controller: 'profileController',
      data: {
        requireLogin: true
      }
    })
    .state('messages', {
      url: '/messages/:id',
      templateUrl: '/views/messages.html',
      controller: 'messagesController',
      data: {
        requireLogin: true
      }
    })
    .state('about', {
      url: '/about',
      templateUrl: '/views/about.html',
      controller: function($scope) {
        console.log('No separate controller');
      },
      data: {
        requireLogin: false
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

migree.run(['authService', '$rootScope', function (authService, $rootScope) {
    authService.fillAuthData();
    $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {
    var requireLogin = toState.data.requireLogin;

    if (requireLogin && !authService.authentication.isAuth) {
      event.preventDefault();
      //Show login modal here
    }
  });
}]);

/*===functions===*/

function validateEmail(email) {
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return re.test(email);
}
