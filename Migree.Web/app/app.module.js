var migree = angular.module('migreeApp', [
    'ngRoute',
    'ngResource',
    'ui.router',
    'LocalStorageModule',
    'jcs-autoValidate',
    'ngImgCrop',
    '$q-spread',
    'ui.select', 
    'ngSanitize'
]);

migree.constant('ngAuthSettings', {
  clientId: 'ngAuthApp'
});

migree.config(function ($httpProvider) {
  $httpProvider.interceptors.push('authInterceptorService');
});

migree.config(function(uiSelectConfig) {
  uiSelectConfig.theme = 'bootstrap';
});

migree.run(['authenticationService', '$rootScope', 'bootstrap3ElementModifier', function (authenticationService, $rootScope, bootstrap3ElementModifier) {
  $rootScope.apiServiceBaseUri = 'https://migree-test.azurewebsites.net';
  bootstrap3ElementModifier.enableValidationStateIcons(true);
  $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {
    var requireLogin = toState.data.requireLogin;

    if (requireLogin && !authenticationService.isAuthenticated()) {
      event.preventDefault();
      //Show login modal here
    }
  });
}]);
