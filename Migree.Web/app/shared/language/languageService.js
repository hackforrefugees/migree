migree.factory('languageService', ['apiService', '$q', 'localStorageService',
function (apiService, $q, localStorageService) {
  var deferred = $q.defer();

  var languageData = localStorageService.get('languageData');

  if (languageData) {
    deferred.resolve(languageData);
  }

  apiService.language.get({ languageCode: 'en' }, function (data) {
    localStorageService.set('languageData', data);
    deferred.resolve(data);
  });

  return deferred.promise;
}]);