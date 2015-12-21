migree.factory('User', ['$resource', function ($resource) {
  'use strict';
  return $resource('/user', null, {
    update: {
      method: 'PUT'
    }
  });
}]);

migree.factory('Business', ['$resource', function ($resource) {
  'use strict';
  return $resource('/business');
}]);

migree.factory('Competence', ['$resource', function ($resource) {
  'use strict';
  return $resource('/competence');
}]);

migree.factory('Location', ['$resource', function ($resource) {
  'use strict';
  return $resource('/location');
}]);