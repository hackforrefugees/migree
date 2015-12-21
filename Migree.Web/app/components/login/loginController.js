migree.controller('loginController', ['$scope', '$location', 'AuthenticationService', function ($scope, $location, authService) {
    'use strict';
    
    $scope.userName = "";
    $scope.password = "";        
    $scope.message = "";

    $scope.login = function () {
      authService.login($scope.userName, $scope.password).then(function (response) {
        $location.path('/matches');
      }, function (err) {
        $scope.message = err.error_description;
      });
    };
}]);
