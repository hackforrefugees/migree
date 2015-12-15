migree.service('apiService', ['$scope', '$resource', 'localStorageService', function ($scope, $resource, localStorageService) {
  'use strict';
  
  var userResource = $resource('/user/:userId', { userId: '@userId' });

  $scope.get = function (userId) {
    return userResource.get({ userId: userId });
  }

  $scope

}]);

/*
{ 'get':    {method:'GET'},
  'save':   {method:'POST'},
  'query':  {method:'GET', isArray:true},
  'remove': {method:'DELETE'},
  'delete': {method:'DELETE'} };
*/




/*

var app = angular.module('app', ['ngResource', 'ngRoute']);

// Some APIs expect a PUT request in the format URL/object/ID
// Here we are creating an 'update' method
app.factory('Notes', ['$resource', function($resource) {
return $resource('/notes/:id', null,
    {
        'update': { method:'PUT' }
    });
}]);

// In our controller we get the ID from the URL using ngRoute and $routeParams
// We pass in $routeParams and our Notes factory along with $scope
app.controller('NotesCtrl', ['$scope', '$routeParams', 'Notes',
                                   function($scope, $routeParams, Notes) {
// First get a note object from the factory
var note = Notes.get({ id:$routeParams.id });
$id = note.id;

// Now call update passing in the ID first then the object you are updating
Notes.update({ id:$id }, note);

// This will PUT /notes/ID with the note object in the request payload
}]);


*/