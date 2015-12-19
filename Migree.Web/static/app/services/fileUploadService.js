migree.service('fileUploadService', ['$http', '$scope', function ($http, $scope) {

  function upload(file) {

    var formData = new FormData();
    formData.append('Content', file);
    var url = $scope.apiServiceBaseUri + 'user/upload';
    return $http.post(
      url,
      formData,
      {
        transformRequest: angular.identity,
        headers: { 'Content-Type': undefined }
      }
    );
  }

  return {
    upload: upload
  };
}]);
