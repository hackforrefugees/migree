migree.controller('messageController', ['$scope', '$stateParams', 'messageService', function ($scope, $stateParams, messageService) {

  var self = this;
  self.toUserId = $stateParams.user;
  $scope.message = null;


  messageService.getThread({userId: self.toUserId})
    .then(function(thread) {
      self.thread = thread;
    }).then(function() {
      
      // TODO: Remove when appropriate response is available from api.
      $scope.language.message = {
        "sendButtonReply": "Reply",
        "sendButton": "Send message",
        "startThread": "Start conversation",
        "now": "a moment ago"
      };

      $scope.sendButtonText = $scope.language.message.startThread;
      /* Handle this later on :) */
      if (self.thread.length) {
        if (self.thread[0].isUser) {
          $scope.sendButtonText = $scope.language.message.sendButton;
        }
        else {
          $scope.sendButtonText = $scope.language.message.sendButtonReply;
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
          created: $scope.language.message.now
        });

        $scope.message = null;
      });
    }
  };
}]);
