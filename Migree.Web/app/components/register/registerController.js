migree.controller('registerController', ['$scope', '$timeout', 'authenticationService', 'fileReader', '$http', '$state', 'registerService', '$q',
  function ($scope, $timeout, authenticationService, fileReader, $http, $state, registerService, $q) {

    var self = this;

    $scope.allBusinesses = [];

    $scope.registration = {
      firstName: '',
      lastName: '',
      password: '',
      email: '',
      city: { name: $scope.language.register.defaultTextLocation, id: 0 },
      work: { name: $scope.language.register.defaultTextWork, id: 0 },
      userType: 2,
      competences: [],
      business: { id: 1, name: 'Developer'}
    };

    var promises = [
      registerService.competencePromise,
      registerService.locationPromise
    ];

    $q.all(promises).spread(function (competences, locations) {
      $scope.competences = competences;
      $scope.locations = locations;

      $scope.allBusinesses = competences;
      setCompetencesAndBusiness();
    });

    $scope.savedSuccessfully = false;
    $scope.message = '';
    $scope.aboutText = '';    

    var userId = null;
    var profileFile = null;

    $scope.croppedImg = null;
    $scope.srcImg = null;
    $scope.avatarCropped = false;

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

    function setCompetencesAndBusiness() {
      $scope.businesses = [];
      $scope.competences = [];

      $.each($scope.allBusinesses, function (key, businessGroup) {
        $scope.businesses.push(businessGroup.business);

        $.each(businessGroup.competences, function (innerKey, competence) {
          $scope.competences.push(competence);
        });
      });
    }

    var submitButtons = $('.step button');
    $scope.goToNext = function () {

      submitButtons.addClass('disabled');
      submitButtons.prop('disabled', true);

      registerService.save($scope.registration).then(function () {
        $scope.savedSuccessfully = true;

        $('.step').prev().hide();
        $('.step').next().show();

        submitButtons.removeClass('disabled');
        submitButtons.prop('disabled', false);

        authenticationService.login($scope.registration.email, $scope.registration.password).then(function (response) {
          /*$scope.registerService.imageUpload(profileFile);*/
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
        $state.go('login');
      }, 2000);
    };

    $scope.updateSkills = function () {
      if ($scope.registration.city.id && $scope.registration.competences.length > 0 && $scope.registration.work.id && $scope.aboutText.length > 0) {
        var userInformation = {
          userLocation: $scope.registration.city,
          description: $scope.aboutText,
          competences: $scope.registration.competences
        };

        registerService.update(userInformation);
        $state.go('thankyou');
      }
      else {
        window.alert('Please complete your registration by selecting a value in each of the dropdown boxes and writing a short description.');
      }
    };
  }]);
