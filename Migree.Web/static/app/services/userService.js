migree.factory('userService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {
  'use strict';
  var serviceBase = 'https://migree.azurewebsites.net/';
  var userServiceFactory = {};
  var _getMatches = function () {
    var authData = localStorageService.get('authorizationData');
    if(authData) {
      return $http.get(serviceBase + 'user/' + authData.userId + '/matches').then(function (response) {
            return response;
        });
    }
  };
  
  userServiceFactory.getMatches = _getMatches;
  return userServiceFactory;

}]);
