migree.controller('matchesController', ['$scope', '$resource', 'localStorageService', function ($scope, $resource, localStorageService) {
  'use strict';

  var testOfAuth = localStorageService.get('authorizationData');  

  var result = $resource($scope.apiServiceBaseUri + '/matches', null, {
    query: {
      method: "GET",
      isArray: true,
      headers: { 'Authorization': 'Bearer ' + testOfAuth.token }
    },
  }).query();

    new ElastiStack(document.getElementById('stack'), {
      distDragBack : 50,
      distDragMax : 150,
      onUpdateStack : function( current ) { return false; }
    });
}]);
