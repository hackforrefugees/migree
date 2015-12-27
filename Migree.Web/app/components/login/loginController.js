migree.controller('loginController', ['$scope', '$location', 'authenticationService', function ($scope, $location, authenticationService) {
    'use strict';
    
    $scope.userName = "";
    $scope.password = "";        
    $scope.message = "";

    $scope.login = function () {
      authenticationService.login($scope.userName, $scope.password, $scope.apiServiceBaseUri).then(function (response) {
        $location.path('/matches');
      }, function (err) {
        $scope.message = err.error_description;
      });
    };
}]);
