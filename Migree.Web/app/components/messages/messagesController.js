migree.controller('messagesController', ['$scope', 'apiService', function ($scope, apiService) {
  var messages = apiService.message.query();
}]);
