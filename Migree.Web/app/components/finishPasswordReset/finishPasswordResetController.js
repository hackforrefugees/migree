migree.controller('finishPasswordResetController', ['$scope', '$state', '$stateParams', 'resetPasswordService', 
  function ($scope, $state, $stateParams, resetPasswordService) {
    
    $scope.newPassword = '';

    $scope.update = function () {
      var model = {
        userId: $stateParams.userid,
        resetKey: $stateParams.verificationkey,
        password: $scope.newPassword
      };

      resetPasswordService.update(model, function () {
        $state.go('login');
      });
    };
  }]);
