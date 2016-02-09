migree.controller('matchesController', ['$scope', 'matchesService', '$timeout', '$state',
  function ($scope, matchesService, $timeout, $state) {

    $scope.gotoMessage = function() {
      var userId = angular.element('.first').data('userId');
      $state.go("message", { "user": userId});
    };
    $scope.flip = function ($event) {
      var card = angular.element('.first').find('.card');
      card.toggleClass('flipped');
    };

    matchesService.matches.then(function (data) {
      $scope.matches = data;
      if(data.length>0) {
        $timeout(function () {
          angular.element('.cardbuttons').removeClass('hidden');
          angular.element('.elasticstack').removeClass('hidden');
          new ElastiStack(document.getElementById('stack'), {
            distDragBack: 80,
            distDragMax: 50,
            onUpdateStack: function (current) { return false; }
          });
        }, 500);
      }
      else {
        angular.element('.emptystate').removeClass('hidden');
      }
    });
  }]);
