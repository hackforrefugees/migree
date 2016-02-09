migree.controller('loginController', ['$scope', '$state', 'authenticationService',
function ($scope, $state, authenticationService) {
  $scope.userName = '';
  $scope.password = '';
  $scope.message = '';

  $scope.login = function () {
    authenticationService.login($scope.userName, $scope.password).then(function (response) {
      $state.go('matches');
    }, function (err) {
      $scope.message = err.error_description;
    });
  };
}]);
