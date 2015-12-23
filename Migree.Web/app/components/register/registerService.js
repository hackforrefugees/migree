migree.factory('registerService', ['apiService', function (apiService) {
  'use strict'; 

  return {
    user: apiService.user,
    businessPromise: apiService.business.query().$promise,
    competencePromise: apiService.competence.query().$promise,
    locationPromise: apiService.location.query().$promise
  };
}]);