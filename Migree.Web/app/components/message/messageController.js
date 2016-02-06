migree.controller('messageController', ['$scope', '$stateParams', 'apiService', function ($scope, $stateParams, apiService) {

  var message = {
    UserId: $stateParams.to,
    Message: null
  };

  $scope.message = message;

  $scope.sendMessage = function() {
    // TODO: Implement error handlingn. 
    apiService.message.save($scope.message, function(data) {});
  };

}]);
