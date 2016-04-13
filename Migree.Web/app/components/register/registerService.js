migree.factory('registerService', ['apiService', function (apiService) {  

  var imageUpload = function (file) {
    var formData = new FormData();
    formData.append('Content', file);
    apiService.imageUpload(formData);
  };

  var save = function (userData) {
    return apiService.user.save(userData).$promise;
  };

  var update = function (userData) {
    return apiService.user.update(userData).$promise;
  };

  return {
    save: save,
    update: update,
    competencePromise: apiService.competence.query().$promise,
    locationPromise: apiService.location.query().$promise,
    imageUpload: imageUpload,    
  };
}]);