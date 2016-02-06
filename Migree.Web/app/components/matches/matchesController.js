migree.controller('matchesController', ['$scope', '$resource', '$state', function ($scope, $resource, $state) {
  'use strict';

  var result = $resource($scope.apiServiceBaseUri + '/matches').query();

  new ElastiStack(document.getElementById('stack'), {
    distDragBack: 50,
    distDragMax: 150,
    onUpdateStack: function (current) { return false; }
  });

  $scope.flip = function ($event) {
      var card = angular.element($event.target).closest('.card');
      console.log(card);
      card.toggleClass('flipped');
    };

}]);
