migree.factory('Matches', ['$resource', function ($resource) {
  'use strict';
  return $resource('/matches');
}]);