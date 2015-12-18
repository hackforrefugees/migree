migree.factory('Business', ['$resource', function ($resource) {
  'use strict';
  return $resource('/business');
}]);