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
  return $resource('/business', {}, {isArray: true});
}]);

migree.factory('Competence', ['$resource', function ($resource) {
  'use strict';
  return $resource('/competence', {}, {isArray: true});
}]);

migree.factory('Location', ['$resource', function ($resource) {
  'use strict';
  return $resource('/location', {}, {isArray: true});
}]);
