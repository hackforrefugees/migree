migree.controller('messageController', ['$scope', '$stateParams', 'apiService', function ($scope, $stateParams, apiService) {

  var self = this;
  self.toUserId = $stateParams.user;

  $scope.message = null;

  var thread = apiService.messageThread.query({userId: self.toUserId});
  $scope.thread = thread;

  $scope.sendMessage = function() {
    // TODO: Implement error handling :)
    apiService.message.save({userId: self.toUserId, message: $scope.message}, function(data) {});
  };

}]);
