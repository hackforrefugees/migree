migree.controller('registerController', ['$scope', '$location', '$timeout', 'authService', 'fileReader', '$http', 'fileUploadService', '$state',
  function ($scope, $location, $timeout, authService, fileReader, $http, fileUploadService, $state) {
    'use strict';

    $http({
          url: 'https://migree.azurewebsites.net/competence',
          method: 'GET'
        }).then(function(response) {
          $scope.competences = response.data;
        }, function() {
    });

    $http({
          url: 'https://migree.azurewebsites.net/business',
          method: 'GET'
        }).then(function(response) {
          $scope.businesses = response.data;
        }, function() {
    });
    $http({
          url: 'https://migree.azurewebsites.net/location',
          method: 'GET'
        }).then(function(response) {
          $scope.locations = response.data;
        }, function() {
    });

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.aboutText = "";

    $scope.registration = {
        firstName: "",
        lastName: "",
        password: "",
        email: "",
        city: "",
        userType: 1
    };

    $scope.cities = [
    { value: '2', label: 'Gothenburg' },
    { value: '1', label: 'Stockholm' },
    { value: '3', label: 'Malmo' }
    ];

    $scope.competence = [
      {id: null, name: '1. Select a skill'},
      {id: null, name: '2. Select a skill'},
      {id: null, name: '3. Select a skill'}
    ];

    var userId = null;
    var profileFile = null;

    $scope.croppedImg = null;
    $scope.srcImg = null;
    $scope.avatarCropped = false;

    $scope.crop = function() {
      $scope.avatarCropped = true;
    };

    $scope.getFile = function () {
      $scope.progress = 0;

      fileReader.readAsDataUrl($scope.file, $scope).then(function(result) {
        profileFile = $scope.file;
        $scope.srcImg = result;
        $scope.didSelect = true;
      });
    };

    $scope.goToNext = function(){
      authService.saveRegistration($scope.registration).then(function (response) {
        $scope.savedSuccessfully = true;
        $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
        userId = response.data.userId;
        $('.step').prev().hide();
        $('.step').next().show();
        fileUploadService.upload(profileFile).then(function(response) {

        }, function(err) {

        });

        },
         function (response) {
             var errors = [];
             if(response.data) {
              for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             }
             $scope.message = "Failed to register user due to. " + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    };

    $scope.updateSkills = function() {
      var ids = $scope.competence.map(function(item) {
        return item.id;
      });

      $http({
        url: 'https://migree.azurewebsites.net/user/',
        method: 'PUT',
        data: {
          userLocation: $scope.registration.city.value,
          description: $scope.aboutText,
          competenceIds: ids
        }
      }).then(function(response) {
        console.log('Got OK when updating user: ', response);
        $state.go('thankyou');

      }, function(err) {
        console.log('Got error when updating user: ', err);
      });
    };

}]);
