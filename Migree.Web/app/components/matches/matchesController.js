migree.controller('matchesController', ['$scope', 'matchesService',
  function ($scope, matchesService) {

    $scope.matches = [];

    $scope.matches = matchesService.matches.then(function (data) {
      $scope.matches = data;

      new ElastiStack(document.getElementById('stack'), {
        distDragBack: 50,
        distDragMax: 150,
        onUpdateStack: function (current) { return false; }
      });

      $scope.flip = function ($event) {
        var card = angular.element($event.target).closest('.card');
        card.toggleClass('flipped');
      };
    })
  }]);
