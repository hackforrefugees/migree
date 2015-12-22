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

  var get = function() {
    return $resource('/business', {}, {isArray: true}).query().$promise;
  };

  return {
    get: get
  };
}]);

migree.factory('Competence', ['$resource', function ($resource) {
  'use strict';

  var get = function() {
    return $resource('/competence', {}, {isArray: true}).query().$promise;
  };

  return {
    get: get
  };
}]);

migree.factory('Location', ['$resource', function ($resource) {
  'use strict';
  var get = function() {
    return $resource('/location', {}, {isArray: true}).query().$promise;
  };

  return {
    get: get
  };
}]);
