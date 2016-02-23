migree.controller('loginController', ['$scope', '$location', 'authenticationService',
function ($scope, $location, authenticationService) {
  $scope.userName = '';
  $scope.password = '';
  $scope.message = '';

  $scope.login = function () {
    authenticationService.login($scope.userName, $scope.password).then(function (response) {
      $location.path($scope.redirectToUrlAfterLoginUrl);
    }, function (err) {
      $scope.message = err.error_description;
    });
  };
}]);
