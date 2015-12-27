var migree = angular.module('migreeApp', [
    'ngRoute',
    'ngResource',
    'ui.router',
    'LocalStorageModule',
    'jcs-autoValidate',
    'ngImgCrop',
    '$q-spread'
]);

migree.constant('ngAuthSettings', {
  clientId: 'ngAuthApp'
});

migree.config(function ($httpProvider) {
  $httpProvider.interceptors.push('authInterceptorService');
});

migree.run(['authenticationService', '$rootScope', 'bootstrap3ElementModifier', function (authenticationService, $rootScope, bootstrap3ElementModifier) {
  $rootScope.apiServiceBaseUri = 'http://localhost:50402';
  bootstrap3ElementModifier.enableValidationStateIcons(true);
  $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {
    var requireLogin = toState.data.requireLogin;

    if (requireLogin && !authenticationService.isAuthenticated()) {
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
