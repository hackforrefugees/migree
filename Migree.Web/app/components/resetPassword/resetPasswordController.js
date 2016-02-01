migree.controller('resetPasswordController', ['$scope', 'resetPasswordService', function ($scope, resetPasswordService) {
  var self = this;

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
