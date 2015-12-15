migree.factory('Language', ['$resource', function ($resource) {
  'use strict';
  return $resource('/language/:id', { id: '@_id' });
}]);