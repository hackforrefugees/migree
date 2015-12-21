migree.factory('Matches', ['$scope', '$resource', function ($scope, $resource) {  
  return $resource($scope.apiServiceBaseUri + '/matches'); 
}]);