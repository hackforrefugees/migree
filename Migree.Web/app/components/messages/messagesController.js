migree.controller('messagesController', ['$scope', 'messageService', '$state',
  function ($scope, messageService, $state) {
    messageService.getMessages().then(function (data) {
      $scope.messages = data;
      if(data.length<=0) {
      	angular.element('.emptystate').removeClass('hidden');
      }
    });

    $scope.gotoMessage = function (userId) {
      $state.go("message", { "user": userId });
    };
  }]);