migree.controller('finishPasswordResetController', ['$scope', '$state', '$stateParams', 'apiService',
  function ($scope, $state, $stateParams, apiService) {

    $scope.newPassword = '';

    $scope.update = function () {
      var model = {
        userId: $stateParams.userid,
        resetKey: $stateParams.verificationkey,
        password: $scope.newPassword
      };

      apiService.resetPassword.update(model, function () {
        $state.go('login');
      });
    };
  }]);