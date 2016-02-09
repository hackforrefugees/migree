migree.controller('messagesController', ['$scope', 'messageService', '$state',
  function ($scope, messageService, $state) {
    messageService.getMessages().then(function (data) {
      $scope.messages = data;
    });

    $scope.gotoMessage = function (userId) {
      $state.go("message", { "user": userId });
    };
  }]);