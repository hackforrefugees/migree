migree.controller('settingsController', ['$scope', 'settingsService',
  function ($scope, settingsService) {
    
    settingsService.user.query().$promise.then(function (data) {
      console.log(data);
      $scope.settings = data;      
    });

    $scope.update = function () {
      
    };
}]);
