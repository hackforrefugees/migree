migree.controller('resetPasswordController', ['$scope', '$state', 'resetPasswordService', 'languageService',
  function ($scope, $state, resetPasswordService, languageService) {

    languageService.then(function (data) {
      $scope.language = data.resetPassword;
    });

    $scope.email = '';

    $scope.init = function () {
      var model = {
        email: $scope.email
      };

      resetPasswordService.save(model, function () {
        $state.go('login');
      });
    };
  }]);