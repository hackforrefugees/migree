migree.controller('homeController', ['$scope', 'languageService',
  function ($scope, languageService) {

    languageService.then(function (data) {
      $scope.language = data.home;
    });
  }]);
