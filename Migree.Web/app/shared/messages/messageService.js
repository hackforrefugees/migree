migree.factory('messageService', ['apiService', function (apiService) {

  var getThread = function(params) {
    return apiService.messageThread.query(params).$promise;
  };

  var saveMessage = function(message) {
    return apiService.message.save(message).$promise;
  };

  var getMessages = function() {
    return apiService.message.query().$promise;
  };

  return {
    getThread: getThread,
    getMessages: getMessages,
    saveMessage: saveMessage
  };
}]);
