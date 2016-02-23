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

migree.config(function (uiSelectConfig) {
  uiSelectConfig.theme = 'bootstrap';
});

migree.run(['authenticationService', '$rootScope', 'bootstrap3ElementModifier', '$state', 'languageService', '$location',
  function (authenticationService, $rootScope, bootstrap3ElementModifier, $state, languageService, $location) {
    bootstrap3ElementModifier.enableValidationStateIcons(true);

    languageService.then(function (data) {
      $rootScope.language = data;
    });

    $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {
      var requireLogin = toState.data.requireLogin;

      if (!$rootScope.redirectToUrlAfterLoginUrl) {
        if ($location.path() === '/') {
          $rootScope.redirectToUrlAfterLoginUrl = '/matches';
        } else {
          $rootScope.redirectToUrlAfterLoginUrl = $location.path();
        }        
      }
      
      if (requireLogin && !authenticationService.isAuthenticated()) {
        event.preventDefault();
        $state.go('login');
      }
    });
  }]);
