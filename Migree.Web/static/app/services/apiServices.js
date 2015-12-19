migree.factory('Business', ['$resource', function ($resource) {
  'use strict';
  return $resource('/business');
}]);

migree.factory('Competence', ['$resource', function ($resource) {
  'use strict';
  return $resource('/competence');
}]);

migree.factory('Language', ['$resource', function ($resource) {
  'use strict';
  return $resource('/language/:languageCode', { languageCode: '@languageCode' });
}]);

migree.factory('Location', ['$resource', function ($resource) {
  'use strict';
  return $resource('/location');
}]);

migree.factory('Matches', ['$resource', function ($resource) {
  'use strict';
  return $resource('/matches');
}]);

migree.factory('ResetPassword', ['$resource', function ($resource) {
  'use strict';
  return $resource('/resetpassword', null, {
    update: {
      method: 'PUT'
    }
  });
}]);

migree.factory('User', ['$resource', function ($resource) {
  'use strict';
  return $resource('/user', null, {
    update: {
      method: 'PUT'
    }
  });
}]);