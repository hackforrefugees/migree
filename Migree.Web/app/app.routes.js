migree.config(function ($routeProvider, $locationProvider, $stateProvider, $urlRouterProvider) {

  //routing DOESN'T work without html5Mode
  $locationProvider.html5Mode({
    enabled: true,
    requireBase: false
  });

  $stateProvider
    .state('home', {
      url: '/',
      templateUrl: 'components/home/homeView.html',
      controller: 'homeController',
      data: {
        requireLogin: false
      }
    })
    .state('register', {
      url: '/register',
      templateUrl: 'components/register/registerView.html',
      controller: 'registerController',
      data: {
        requireLogin: false
      }
    })
    .state('thankyou', {
      url: '/thankyou',
      templateUrl: 'components/thankYou/thankYouView.html',
      controller: 'thankYouController',
      data: {
        requireLogin: false
      }
    })
    .state('login', {
      url: '/login',
      templateUrl: 'components/login/loginView.html',
      controller: 'loginController',
      data: {
        requireLogin: false
      }
    })
    .state('resetpassword', {
      url: '/resetpassword',
      templateUrl: 'components/resetPassword/resetPasswordView.html',
      controller: 'resetPasswordController',
      data: {
        requireLogin: false
      }
    })
    .state('finishpasswordreset', {
      url: '/user/:userid/reset/:verificationkey',
      templateUrl: 'components/finishPasswordReset/finishPasswordResetView.html',
      controller: 'finishPasswordResetController',
      data: {
        requireLogin: false
      }
    })
    .state('matches', {
      url: '/matches',
      templateUrl: 'components/matches/matchesView.html',
      controller: 'matchesController',
      data: {
        requireLogin: true
      }
    })
    .state('messages', {
      url: '/messages',
      templateUrl: 'components/messages/messagesView.html',
      controller: 'messagesController',
      data: {
        requireLogin: true
      }
    })
    .state('message', {
      url: '/message/:user',
      templateUrl: 'components/message/messageView.html',
      controller: 'messageController',
      data: {
        requireLogin: true
      }
    })
    .state('notfound', {
      url: '/notfound',
      templateUrl: 'components/notFound/notFoundView.html',
      controller: 'notFoundController',
      data: {
        requireLogin: false
      }
    });

  $urlRouterProvider.otherwise('/notfound');
});
