migree.service('fileUploadService', ['$http', function($http) {

  var _serviceBase = 'https://migree.azurewebsites.net/user/';

  function upload(file, userId) {
    var fd = new FormData();
    fd.append('Content', file);

    var url = _serviceBase+userId+'/upload';

    return $http.post(url, fd,
      {
        transformRequest: angular.identity,
        headers: {'Content-Type':undefined}
      });
  }

  return {
    upload: upload
  };
}]);
