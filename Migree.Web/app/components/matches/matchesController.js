migree.controller('matchesController', ['$scope', '$resource', '$state','apiService', function ($scope, $resource, $state, apiService) {
  'use strict';

  $scope.matches = apiService.matches.query();

  new ElastiStack(document.getElementById('stack'), {
    distDragBack: 50,
    distDragMax: 150,
    onUpdateStack: function (current) { return false; }
  });

  $scope.flip = function ($event) {
      var card = angular.element($event.target).closest('.card');
      card.toggleClass('flipped');
    };

}]);
