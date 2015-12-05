migree.controller('LoginController', ['$scope', '$http', '$location', function($scope, $http, $location) {
  $scope.login = function(){
    $http({
      method: 'GET',
      url: 'ajax/login.json'
    }).then(function successCallback(response) {
      if(!response.error)
        $location.path('/dashboard');
      else
        $scope.message = "Invalid login.";
    }, function errorCallback(response) {
      // called asynchronously if an error occurs
      // or server returns response with an error status.
    });
  };

}]);
