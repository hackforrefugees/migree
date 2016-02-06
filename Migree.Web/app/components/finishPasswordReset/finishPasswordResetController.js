migree.controller('finishPasswordResetController', ['$scope', '$stateParams', 'resetPasswordService', 'languageService',
  function ($scope, $stateParams, resetPasswordService, languageService) {

    languageService.then(function (data) {
      $scope.language = data.finishPasswordReset;
    });

    $scope.newPassword = '';

    $scope.update = function () {
      var model = {
        userId: $stateParams.userid,
        resetKey: $stateParams.verificationkey,
        password: $scope.newPassword
      };

      resetPasswordService.update(model);
    };
  }]);
