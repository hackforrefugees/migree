migree.controller('resetPasswordController', ['$scope', 'resetPasswordService', 'languageService',
  function ($scope, resetPasswordService, languageService) {    

    languageService.then(function (data) {
      $scope.language = data.resetPassword;
    });

    $scope.email = '';
    $scope.newPassword = '';

    $scope.init = function () {
      var model = {
        email: $scope.email
      };

      resetPasswordService.save(model);
    };

    $scope.update = function () {
      var model = {
        userId: '',
        resetKey: '',
        password: ''
      };


    };
  }]);
