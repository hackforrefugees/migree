migree.factory('apiService', ['$resource', function ($resource) {
  'use strict';

  var self = this;
  self.apiBaseUrl = 'http://localhost:50402';

  var user = $resource(self.apiBaseUrl + '/user', null, {
    update: {
      method: 'PUT'
    }
  });

  var business = function () {
    return $resource(self.apiBaseUrl + '/business');
  };

  var competence = function () {
    return $resource(self.apiBaseUrl + '/competence');
  };

  var location = function () {
    return $resource(self.apiBaseUrl + '/location');
  };

  return {
    user: user,
    business: business,
    competence: competence,
    location: location
  };
}]);