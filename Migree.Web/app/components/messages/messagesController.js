migree.controller('messagesController', ['$scope', 'messageService', function ($scope, messageService) {
  messageService.getMessages().then(function(data) {
    $scope.messages = data;    
  });


}]);
