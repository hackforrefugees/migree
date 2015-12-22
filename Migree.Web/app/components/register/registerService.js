migree.factory('registerService', ['apiService', function (apiService) {
  'use strict';  
  return {
    user: apiService.user,
    business: apiService.business,
    competence: apiService.competence,
    location: apiService.location
  };
}]);