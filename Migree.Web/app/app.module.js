var migree = angular.module('migreeApp', [
    'ngRoute',
    'ui.router',
    'LocalStorageModule',
    'jcs-autoValidate',
    'ngImgCrop',
    'frapontillo.bootstrap-switch'
]);

migree.constant('ngAuthSettings', {
  clientId: 'ngAuthApp'
});

migree.config(function ($httpProvider) {
  $httpProvider.interceptors.push('authInterceptorService');
});

migree.run(['AuthenticationService', '$rootScope', 'bootstrap3ElementModifier', function (authService, $rootScope, bootstrap3ElementModifier) {
  $rootScope.apiServiceBaseUri = 'http://migree.azurewebsites.net/';
  $rootScope.apiServiceVersion = 1;
  authService.fillAuthData();
  bootstrap3ElementModifier.enableValidationStateIcons(true);
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