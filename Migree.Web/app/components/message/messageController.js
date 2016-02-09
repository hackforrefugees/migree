migree.controller('messageController', ['$scope', '$stateParams', 'messageService', function ($scope, $stateParams, messageService) {

  var self = this;
  self.toUserId = $stateParams.user;

  $scope.message = null;

  messageService.getThread({userId: self.toUserId})
    .then(function(thread) { self.thread = thread; })
    .then(function() {

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
    });

  $scope.sendMessage = function() {
    if(!$scope.editable) {
      messageService.saveMessage({userId: self.toUserId, message: $scope.message}).then(function(data) {
        self.thread.unshift({
          isUser: true,
          content: $scope.message,
          created: 'a moment ago'
        });

        $scope.message = null;

      });
    }
  };

}]);
