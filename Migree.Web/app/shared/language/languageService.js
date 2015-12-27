migree.factory('Language', ['$resource', function ($resource) {
  'use strict';
  return $resource('/language/:languageCode', { languageCode: '@languageCode' });
}]);