migree.controller('messageController', ['$scope', '$stateParams', 'messageService', function ($scope, $stateParams, messageService) {

  var self = this;
  self.toUserId = $stateParams.user;
  $scope.message = null;


  messageService.getThread({userId: self.toUserId})
    .then(function(thread) {
      self.thread = thread;
    })
    .then(function() {

      $scope.sendButtonText = $scope.language.message.startThread;
      $scope.thread = self.thread;
    });

    self.setSendButtonText = function() {
      if (self.thread.length) {
        if (self.thread[0].isUser) {
          $scope.sendButtonText = $scope.language.message.sendButton;
        }
        else {
          $scope.sendButtonText = $scope.language.message.sendButtonReply;
        }
      }
    };

    $scope.sendMessage = function() {
      if(!$scope.editable) {
        messageService.saveMessage({userId: self.toUserId, message: $scope.message}).then(function(data) {

          self.thread.unshift({
            isUser: true,
            content: $scope.message,
            created: $scope.language.message.now
          });

          self.setSendButtonText();
          $scope.message = null;
        });
      }
    };
  }]);
