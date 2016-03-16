migree.controller('settingsController', ['$scope', 'settingsService', '$q',
  function ($scope, settingsService, $q) {

    settingsService.user.query().$promise.then(function (data) {
      $scope.settings = data;
      var promises = [
            settingsService.competencePromise,
            settingsService.businessPromise,
            settingsService.locationPromise
      ];

      $q.all(promises).spread(function (competences, businesses, locations) {
        $scope.competences = competences;
        $scope.businesses = businesses;
        $scope.businesses.selected = $scope.businesses[0];
        $scope.locations = locations;
        $scope.locations.selected = $scope.locations.filter(function (location) {
          return location.id === $scope.settings.userLocation;
        })[0];
        $scope.settings.competences.selected = getFilteredArray($scope.competences, $scope.settings.competences);
      });

    });


    $scope.update = function () {
      settingsService.user.update($scope.settings);
    };

    function getFilteredArray(inputArray, filter) {
      var filteredArray = [];
      for (var i = 0; i < inputArray.length; i++) {
        for (var p = 0; p < filter.length; p++) {
          if (filter[p] === inputArray[i].id) {
            filteredArray.push(inputArray[i]);
          }
        }
      }
      return filteredArray;
    }
  }]);
