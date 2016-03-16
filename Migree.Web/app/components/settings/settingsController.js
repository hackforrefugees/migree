migree.controller('settingsController', ['$scope', 'settingsService', '$q', 'fileReader',
  function ($scope, settingsService, $q, fileReader) {

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

    $scope.croppedImg = null;
    $scope.srcImg = null;
    $scope.avatarCropped = false;

    var profileFile = null;

    $scope.crop = function () {
      $scope.avatarCropped = true;
    };

    $scope.getFile = function () {
      $scope.progress = 0;

      fileReader.readAsDataUrl($scope.file, $scope).then(function (result) {
        profileFile = $scope.file;
        $scope.srcImg = result;
        $scope.didSelect = true;
      });
    };

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
