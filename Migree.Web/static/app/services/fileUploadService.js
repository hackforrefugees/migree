migree.service('fileUploadService', ['$http', function($http) {
  
  function upload(file) {

    var fd = new FormData();
    fd.append('Content', file);
    var url = 'https://migree.azurewebsites.net/user/upload';
    return $http.post(
      url,
      fd,
      {
        transformRequest: angular.identity,
        headers: {'Content-Type':undefined}
      }
    );
  }

  return {
    upload: upload
  };
}]);
