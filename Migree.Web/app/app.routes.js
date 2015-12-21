migree.config(function ($routeProvider, $locationProvider, $stateProvider, $urlRouterProvider) {

  //routing DOESN'T work without html5Mode
  $locationProvider.html5Mode({
    enabled: true,
    requireBase: false
  });

  $stateProvider
    .state('home', {
      url: '/',
      templateUrl: '/app/components/home/homeView.html',
      controller: 'homeController',
      data: {
        requireLogin: false
      }
    })
    .state('register', {
      url: '/register',
      templateUrl: '/register/registerView.html',
      controller: 'registerController',
      data: {
        requireLogin: false
      }
    })
    .state('thankyou', {
      url: '/thankyou',
      templateUrl: '/thankYou/thankYouView.html',
      controller: 'thankYouController',
      data: {
        requireLogin: false
      }
    })
    .state('login', {
      url: '/login',
      templateUrl: '/login/loginView.html',
      controller: 'loginController',
      data: {
        requireLogin: false
      }
    })
    .state('matches', {
      url: '/matches',
      templateUrl: '/matches/matchesView.html',
      controller: 'matchesController',
      data: {
        requireLogin: true
      }
    })
    .state('notfound', {
      url: '/notfound',
      templateUrl: 'notFound/notFoundView.html',
      data: {
        requireLogin: false
      }
    });

  $urlRouterProvider.otherwise('/notfound');
});