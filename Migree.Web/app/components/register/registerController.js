migree.controller('registerController', ['$scope', '$location', '$timeout', 'authenticationService', 'fileReader', '$http', '$state', 'registerService', '$q',
  function ($scope, $location, $timeout, authenticationService, fileReader, $http, $state, registerService, $q) {
    'use strict';

    var self = this;

    $scope.registration = {
      firstName: "",
      lastName: "",
      password: "",
      email: "",
      city: { name: "I live in / near...", id: 0 },
      work: { name: "I am a...", id: 0 },
      userType: 2,
      competences: []
    };

    var promises = [
      registerService.competencePromise,
      registerService.businessPromise,
      registerService.locationPromise
    ];

    $q.all(promises).spread(function (competences, businesses, locations) {
      $scope.competences = competences;
      $scope.businesses = businesses;
      $scope.locations = locations;
    });

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.aboutText = "";


    $scope.competence = [
      { id: null, name: '1. Select a skill' },
      { id: null, name: '2. Select a skill' },
      { id: null, name: '3. Select a skill' }
    ];

    var userId = null;
    var profileFile = null;

    $scope.croppedImg = null;
    $scope.srcImg = null;
    $scope.avatarCropped = false;

    self.upload = function (file) {
      var formData = new FormData();
      formData.append('Content', file);
      var url = $scope.apiServiceBaseUri + '/user/upload';
      return $http.post(
        url,
        formData,
        {
          transformRequest: angular.identity,
          headers: { 'Content-Type': undefined }
        }
      );
    };

    $scope.crop = function () {
      $scope.avatarCropped = true;
    };

    $scope.getFile = function () {
      $scope.progress = 0;

      fileReader.readAsDataUrl($scope.file, $scope).then(function (result) {
        profileFile = $scope.file;
        $scope.srcImg = result;
        $scope.didSelect = true;
      });
    };

    var submitButtons = $('.step button');
    $scope.goToNext = function () {

      submitButtons.addClass('disabled');
      submitButtons.prop('disabled', true);

      registerService.user.save($scope.registration).$promise.then(function (response) {
        $scope.savedSuccessfully = true;

        $('.step').prev().hide();
        $('.step').next().show();

        submitButtons.removeClass('disabled');
        submitButtons.prop('disabled', false);

        authenticationService.login($scope.registration.email, $scope.registration.password, $scope.apiServiceBaseUri).then(function (response) {
          self.upload(profileFile).then(function (response) {
          }, function (err) {

          });
        }, function (err) {
          $scope.message = err.error_description;
        });
      },
         function (response) {
           if (response.status === 409) {
             window.alert('This email is already registered. Please try again with another email address.');
           }
           else {
             window.alert('Could not save user due to: ' + response.statusText);
           }
         });
    };

    var startTimer = function () {
      var timer = $timeout(function () {
        $timeout.cancel(timer);
        $location.path('/login');
      }, 2000);
    };

    $scope.updateSkills = function () {
      var competenceIds = $scope.registration.competences.map(function (item) {
        return item.id;
      });
      if ($scope.registration.city.id && competenceIds[0] && $scope.registration.work.id && $scope.aboutText.length > 0) {
        var userInformation = {
          userLocation: $scope.registration.city.id,
          description: $scope.aboutText,
          competenceIds: competenceIds
        };

        registerService.user.update(userInformation);
        $state.go('thankyou');
      }
      else {
        window.alert('Please complete your registration by selecting a value in each of the dropdown boxes and writing a short description.');
      }
    };
  }]);
