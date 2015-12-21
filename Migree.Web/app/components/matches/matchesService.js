migree.factory('Matches', ['$resource', function ($resource) {  
  return $resource('http://localhost:50402/matches');
}]);