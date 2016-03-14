migree.controller('settingsController', ['$scope', 'settingsService', '$q',
  function ($scope, settingsService, $q) {
    
    settingsService.user.query().$promise.then(function (data) {      
      $scope.settings = data;      
    });

    var promises = [
      settingsService.competencePromise,
      settingsService.businessPromise,
      settingsService.locationPromise
    ];

    $q.all(promises).spread(function (competences, businesses, locations) {
      $scope.competences = competences;
      $scope.businesses = businesses;
      $scope.locations = locations;
    });


    $scope.update = function () {
      
    };
}]);
