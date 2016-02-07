migree.controller('messageController', ['$scope', '$stateParams', 'apiService', function ($scope, $stateParams, apiService) {

  var self = this;
  self.toUserId = $stateParams.user;

  $scope.message = null;

  var thread = apiService.messageThread.query({userId: self.toUserId});
  $scope.sendButtonText = 'Start conversation';

  if(thread.length) {
    if(thread[0].isUser) {
      $scope.sendButtonText = 'Send message';
    }
    else {
      $scope.sendButtonText = 'Reply';
    }
  }

  $scope.thread = thread;

  $scope.sendMessage = function() {
    if(!$scope.editable) {
      apiService.message.save({userId: self.toUserId, message: $scope.message}, function(data) {

      });
    }
  };

}]);
