migree.controller('messageController', ['$scope', '$stateParams', 'apiService', function ($scope, $stateParams, apiService) {

  var message = {
    UserId: $stateParams.to,
    Message: null
  };

  $scope.message = message;

  $scope.sendMessage = function() {
    apiService.message.save($scope.message, function(data) {
      console.log(data);
    });
  };

}]);
