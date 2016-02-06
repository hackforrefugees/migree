migree.controller('messagesController', ['$scope', 'apiService', function ($scope, apiService) {
  $scope.messages = apiService.message.query();
}]);
