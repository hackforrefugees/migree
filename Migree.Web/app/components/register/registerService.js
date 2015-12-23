migree.factory('registerService', ['apiService', function (apiService) {
  'use strict';

  return {
    user: apiService.user,
    business: function() {
      return apiService.business().query().$promise;
    },
    competence: function() {
      return apiService.competence().query().$promise;
    },
    location: function() {
      return apiService.location().query().$promise;
    }
  };
}]);
