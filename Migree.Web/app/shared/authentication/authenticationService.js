migree.factory('authenticationService', ['apiService', '$q', 'localStorageService', function (apiService, $q, localStorageService) {
  'use strict';
  var apiServiceFactory = {};

  var _login = function (email, password, apiServiceBaseUri) {
    email = email.replace('+', encodeURIComponent('+'));
    var data = "grant_type=password&username=" + email + "&password=" + password;
    var deferred = $q.defer();

    apiService.token(data).success(function (response) {
      localStorageService.set('authorizationData', { token: response.access_token });
      deferred.resolve(response);
    }).error(function (err, status) {
      _logOut();
      deferred.reject(err);
    });

    return deferred.promise;
  };

  var _logOut = function () {
    localStorageService.remove('authorizationData');
  };

  var _isAuthenticated = function () {
    var authData = localStorageService.get('authorizationData');
    return authData;
  };

  apiServiceFactory.login = _login;
  apiServiceFactory.logOut = _logOut;
  apiServiceFactory.isAuthenticated = _isAuthenticated;

  return apiServiceFactory;
}]);