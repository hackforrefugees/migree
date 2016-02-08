migree.controller('messageController', ['$scope', '$stateParams', 'apiService', function ($scope, $stateParams, apiService) {

  var self = this;
  self.toUserId = $stateParams.user;

  $scope.message = null;

  self.thread = apiService.messageThread.query({userId: self.toUserId});
  $scope.sendButtonText = 'Start conversation';

  /* Handle this later on :) */
  if(self.thread.length) {
    if(self.thread[0].isUser) {
      $scope.sendButtonText = 'Send message';
    }
    else {
      $scope.sendButtonText = 'Reply';
    }
  }

  $scope.thread = self.thread;

  $scope.sendMessage = function() {
    if(!$scope.editable) {
      apiService.message.save({userId: self.toUserId, message: $scope.message}, function(data) {
        self.thread.unshift({
          isUser: true,
          content: $scope.message,
          created: 'a moment ago'
        });
      });
    }
  };

}]);
