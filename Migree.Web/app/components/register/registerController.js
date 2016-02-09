migree.controller('registerController', ['$scope', '$timeout', 'authenticationService', 'fileReader', '$http', '$state', 'registerService', '$q',
  function ($scope, $timeout, authenticationService, fileReader, $http, $state, registerService, $q) {

    var self = this;

    $scope.registration = {
      firstName: '',
      lastName: '',
      password: '',
      email: '',
      city: { name: $scope.language.register.defaultTextLocation, id: 0 },
      work: { name: $scope.language.register.defaultTextWork, id: 0 },
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
    $scope.message = '';
    $scope.aboutText = '';

    $scope.competence = [
      { id: null, name: $scope.language.register.defaultTextCompetence1 },
      { id: null, name: $scope.language.register.defaultTextCompetence2 },
      { id: null, name: $scope.language.register.defaultTextCompetence3 }
    ];

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

        authenticationService.login($scope.registration.email, $scope.registration.password).then(function (response) {
          $scope.registerService.imageUpload(profileFile);
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
