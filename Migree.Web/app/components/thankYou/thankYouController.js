migree.controller('thankYouController', ['$scope', 'languageService',
  function ($scope, languageService) {
    languageService.then(function (data) {
      $scope.language = data.thankYou;
    });
  }]);
