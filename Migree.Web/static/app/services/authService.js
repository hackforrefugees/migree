migree.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {
  'use strict';
  var serviceBase = 'https://migree.azurewebsites.net/';
  var authServiceFactory = {};

  var _authentication = {
      isAuth: false,
      userName: "",
      userId: ""
  };

  var _saveRegistration = function (registration) {

      _logOut();

      return $http.post(serviceBase + 'user', registration).then(function (response) {
          return response;
      });

  };

  var _login = function (loginData) {

      var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

      var deferred = $q.defer();

      $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
          localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, userId: response.userId });

          _authentication.isAuth = true;
          _authentication.userName = loginData.userName;
          _authentication.userId = response.userId;

          deferred.resolve(response);

      }).error(function (err, status) {
          _logOut();
          deferred.reject(err);
      });

      return deferred.promise;

  };

  var _logOut = function () {

      localStorageService.remove('authorizationData');

      _authentication.isAuth = false;
      _authentication.userName = "";

  };

  var _fillAuthData = function () {


      var authData = localStorageService.get('authorizationData');
      if (authData)
      {
        _authentication.isAuth = true;
        _authentication.userName = authData.userName;
        _authentication.userId = authData.userId;
      }
  };

  authServiceFactory.saveRegistration = _saveRegistration;
  authServiceFactory.login = _login;
  authServiceFactory.logOut = _logOut;
  authServiceFactory.fillAuthData = _fillAuthData;
  authServiceFactory.authentication = _authentication;

  return authServiceFactory;
}]);
