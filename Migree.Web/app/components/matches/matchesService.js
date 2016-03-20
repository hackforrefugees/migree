migree.factory('matchesService', ['apiService', function (apiService) {
  return {
    matches: apiService.matches.query().$promise
  };
}]);