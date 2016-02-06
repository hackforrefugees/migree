migree.factory('languageService', ['apiService', '$q', function (apiService, $q) {
  'use strict';

  return apiService.language.get({ languageCode: 'en' }).$promise;
}]);