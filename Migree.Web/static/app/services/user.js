migree.factory('User', ['$resource', function ($resource) {
  'use strict';
  return $resource('/user', null, {
    update: {
      method: 'PUT'
    }
  });
}]);