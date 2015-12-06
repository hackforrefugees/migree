'use strict';
migree.factory('userService', ['$http', '$q', function ($http, $q) {
    var serviceBase = 'https://migree.azurewebsites.net/';
    var userServiceFactory = {};
    var _getMatches = function (userId) {
        return $http.get(serviceBase + 'user/' + userId + '/matches').then(function (response) {
            return response;
        });
    }

    userServiceFactory.getMatches = _getMatches;

    return userServiceFactory;

}]);