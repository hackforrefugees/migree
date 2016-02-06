migree.controller('matchesController', ['$scope', '$resource', '$state', function ($scope, $resource, $state) {
  'use strict';

  var result = $resource($scope.apiServiceBaseUri + '/matches').query();

  new ElastiStack(document.getElementById('stack'), {
    distDragBack: 50,
    distDragMax: 150,
    onUpdateStack: function (current) { return false; }
  });

  $scope.message = function() {
    $state.go('message', 
      {to: '005cbf19-2b73-4e1c-a5d2-89e13fcbb95d'}
    );
  };
}]);
