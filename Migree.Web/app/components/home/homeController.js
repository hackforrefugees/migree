migree.controller('homeController', ['$scope', 'authenticationService', 'languageService',
  function ($scope, authenticationService, languageService) {
    
    languageService.then(function (data) {
      $scope.language = data.home;
    });
    authenticationService.logOut();
  }]);
