migree.controller('matchesController', ['$scope', 'matchesService', '$timeout', '$state',
  function ($scope, matchesService, $timeout, $state) {

    matchesService.matches.then(function (data) {
      $scope.matches = data;
      $timeout(function () {
        new ElastiStack(document.getElementById('stack'), {
          distDragBack: 80,
          distDragMax: 50,
          onUpdateStack: function (current) { return false; }
        });
      }, 500);
      $scope.gotoMessage = function() {
        var userId = angular.element('.first').data('userId');
        $state.go("message", { "user": userId});
      };
      $scope.flip = function ($event) {
        var card = angular.element('.first').find('.card');
        card.toggleClass('flipped');
      };
    });
  }]);
