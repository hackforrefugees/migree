migree.controller('resetPasswordController', ['$scope', 'resetPasswordService', function ($scope, resetPasswordService) {
  var self = this;

  $scope.email = '';
  $scope.newPassword = '';

  $scope.init = function () {
    var model = {
      email: $scope.email
    };

    window.alert('hoj');
    window.alert(model.email);
    resetPasswordService.save(model);
  };

  $scope.update = function () {
    var model = {
      userId: '',
      resetKey: '',

    };
  };

  //userId, resetKey, password
}]);
