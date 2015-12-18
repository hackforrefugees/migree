migree.factory('Location', ['$resource', function ($resource) {
  'use strict';
  return $resource('/location');
}]);