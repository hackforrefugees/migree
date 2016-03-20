migree.factory('settingsService', ['apiService', function (apiService) {

  var imageUpload = function (file) {
    if(file) {
      var formData = new FormData();
      formData.append('Content', file);
      apiService.imageUpload(formData);  
    }
  };

  return {
    user: apiService.user,
    businessPromise: apiService.business.query().$promise,
    competencePromise: apiService.competence.query().$promise,
    locationPromise: apiService.location.query().$promise,
    imageUpload: imageUpload
  };
}]);