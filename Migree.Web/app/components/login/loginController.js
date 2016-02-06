migree.controller('loginController', ['$scope', '$location', 'authenticationService', 'languageService',
function ($scope, $location, authenticationService, languageService) {
  'use strict';

  languageService.then(function (data) {
    $scope.language = data.login;    
  });

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
