﻿migree.factory('apiService', ['$resource', '$http', function ($resource, $http) {
  'use strict';

  var self = this;

  self.apiBaseUrl = 'https://migree.azurewebsites.net';

  var competence = $resource(self.apiBaseUrl + '/competence');
  var location = $resource(self.apiBaseUrl + '/location');
  var matches = $resource(self.apiBaseUrl + '/matches');
  var message = $resource(self.apiBaseUrl + '/message');
  var messageThread = $resource(self.apiBaseUrl + '/message/:userId', { userId: '@userId' });

  var resetPassword = $resource(self.apiBaseUrl + '/resetpassword', null, {
    update: {
      method: 'PUT'
    }
  });

  var user = $resource(self.apiBaseUrl + '/user', null, {
    update: {
      method: 'PUT'
    },
    query: {
      method: 'GET',
      isArray: false
    }
  });

  var language = $resource(self.apiBaseUrl + '/language/:languageCode', { languageCode: '@languageCode' });

  var imageUpload = function (data) {
    return $http.post(self.apiBaseUrl + '/user/upload', data, { transformRequest: angular.identity, headers: { 'Content-Type': undefined } });
  };

  var token = function (data) {
    return $http.post(self.apiBaseUrl + '/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
  };

  return {
    competence: competence,
    language: language,
    location: location,
    matches: matches,
    message: message,
    resetPassword: resetPassword,
    user: user,
    imageUpload: imageUpload,
    token: token,
    messageThread: messageThread
  };
}]);