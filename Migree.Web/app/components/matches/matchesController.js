migree.controller('matchesController', ['$scope', '$resource', function ($scope, $resource) {
  'use strict';  

  var result = $resource($scope.apiServiceBaseUri + '/matches').query();

    new ElastiStack(document.getElementById('stack'), {
      distDragBack : 50,
      distDragMax : 150,
      onUpdateStack : function( current ) { return false; }
    });
}]);
