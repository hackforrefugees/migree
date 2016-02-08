migree.factory('authInterceptorService', ['$q', '$state', 'localStorageService', function ($q, $state, localStorageService) {
  return {
    request: function (config) {

      config.headers = config.headers || {};

      var authData = localStorageService.get('authorizationData');
      if (authData) {
        config.headers.Authorization = 'Bearer ' + authData.token;
      }

      return config;
    },
    responseError: function (rejection) {
      if (rejection.status === 401) {
        $state.go('login');
      }
      return $q.reject(rejection);
    }
  };
}]);
