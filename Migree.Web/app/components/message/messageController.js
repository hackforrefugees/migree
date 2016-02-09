migree.controller('messageController', ['$scope', '$stateParams', 'messageService', function ($scope, $stateParams, messageService) {

  $scope.toUserId = $stateParams.user;
  $scope.message = null;

  messageService.getThread({ userId: $scope.toUserId })
    .then(function (thread) {
      $scope.thread = thread;
    }).then(function () {

      if ($scope.thread.length && $scope.thread.length === 1) {
        $scope.sendButtonText = $scope.language.message.sendButton;
      }
      else if ($scope.thread.length && $scope.thread.length > 1) {
        $scope.sendButtonText = $scope.language.message.sendButtonReply;
      }
      else {
        $scope.sendButtonText = $scope.language.message.startThread;
      }

      $scope.thread = $scope.thread;
    });

  $scope.sendMessage = function () {
    if (!$scope.editable) {
      messageService.saveMessage({ userId: $scope.toUserId, message: $scope.message }).then(function (data) {

        $scope.thread.unshift({
          isUser: true,
          content: $scope.message,
          created: $scope.language.message.now
        });

        $scope.message = null;
      });
    }
  };
}]);
