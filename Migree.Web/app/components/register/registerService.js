migree.factory('registerService', ['apiService', function (apiService) {
  'use strict';  
  return {
    user: apiService.user,
    business: apiService.business.query().$promise,
    competence: apiService.competence.query().$promise,
    location: apiService.location.query().$promise
  };
}]);