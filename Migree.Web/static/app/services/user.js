migree.factory('User', ['$resource', function ($resource) {
  'use strict';
  return $resource('/user/:id', { id: '@_id' }, {
    update: {
      method: 'PUT'
    }
  });
}]);

/*
{ 'get':    {method:'GET'},
  'save':   {method:'POST'},
  'query':  {method:'GET', isArray:true},
  'remove': {method:'DELETE'},
  'delete': {method:'DELETE'} };
*/