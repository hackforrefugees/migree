migree.controller('messagesController', ['$scope', 'messageService', function ($scope, messageService) {
  messageService.getMessage().then(function(data) {
    //$scope.messages = data
    console.log(data);
  });


}]);
