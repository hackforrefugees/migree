migree.controller('registerController', ['$scope', '$location', '$timeout', 'authenticationService', 'fileReader', '$http', '$state', 'Competence', 'Business', 'Location', '$q',
  function ($scope, $location, $timeout, authenticationService, fileReader, $http, $state, competence, business, location, $q) {
    'use strict';

    var self = this;

    console.log('will query backend for stuff. ');
    var promises = [
      competence.get(),
      business.get(),
      location.get()
    ];

    $q.all(promises).spread(function(competences, businesses, locations) {
      $scope.competences = competences;
      $scope.business = businesses;
      $scope.location = locations;

      console.log('got stuff from backend. Remove loader or other thing');
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
      userType: 2
    };

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

    var upload = function(file) {

      var formData = new FormData();
      formData.append('Content', file);
      var url = $scope.apiServiceBaseUri + 'user/upload';
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

      //OBS REMOVED FROM AUTHSERVICE :)
      //authenticationService.saveRegistration($scope.registration).then(function (response) {
      //  $scope.savedSuccessfully = true;

      //  $('.step').prev().hide();
      //  $('.step').next().show();

      //  submitButtons.removeClass('disabled');
      //  submitButtons.prop('disabled', false);

      //  authenticationService.login($scope.registration.email, $scope.registration.password, $scope.apiServiceBaseUri).then(function (response) {
      //    self.upload(profileFile).then(function (response) {
      //    }, function (err) {

      //    });
      //  }, function (err) {
      //    $scope.message = err.error_description;
      //  });
      //},
      //   function (response) {
      //     if (response.status === 409) {
      //       window.alert('This email is already registered. Please try again with another email address.');
      //     }
      //     else {
      //       window.alert('Could not save user due to: ' + response.statusText);
      //     }
      //   });
    };

    var startTimer = function () {
      var timer = $timeout(function () {
        $timeout.cancel(timer);
        $location.path('/login');
      }, 2000);
    };

    $scope.updateSkills = function () {
      var ids = $scope.competence.map(function (item) {
        return item.id;
      });
      if ($scope.registration.city.id && ids[0] && ids[1] && ids[2] && $scope.registration.work.id && $scope.aboutText.length > 0) {
        var skillsData = {
          userLocation: $scope.registration.city.id,
          description: $scope.aboutText,
          competenceIds: ids
        };

        $http.put('https://migree.azurewebsites.net/user', skillsData)
            .success(function (data, status, headers) {

            })
            .error(function (data, status, header, config) {
            });
        $state.go('thankyou');
      }
      else {
        window.alert('Please complete your registration by selecting a value in each of the dropdown boxes and writing a short description.');
      }


    };

  }]);
