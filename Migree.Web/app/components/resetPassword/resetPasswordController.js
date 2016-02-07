migree.controller('resetPasswordController', ['$scope', '$state', 'resetPasswordService',
  function ($scope, $state, resetPasswordService) {
    
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