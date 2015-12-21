migree.factory('ResetPassword', ['$resource', function ($resource) {
  'use strict';
  return $resource('/resetpassword', null, {
    update: {
      method: 'PUT'
    }
  });
}]);