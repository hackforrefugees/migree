'use strict';
migree.controller('registerController', ['$scope', '$location', '$timeout', 'authService', 'fileReader', '$http', 'fileUploadService',
  function ($scope, $location, $timeout, authService, fileReader, $http, fileUploadService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        firstName: "",
        lastName: "",
        password: "",
        confirmPassword: "",
        email: "",
        city: "",
        userType: 1
    };

    $scope.cities = [
    { value: '2', label: 'Gothenburg' },
    { value: '1', label: 'Stockholm' },
    { value: '3', label: 'Malmo' }
    ];

    var profileFile = null;
    $scope.getFile = function () {
      $scope.progress = 0;
      fileReader.readAsDataUrl($scope.file, $scope).then(function(result) {
        profileFile = $scope.file;

        /* TODO: scale
        var canvas = document.createElement('canvas');
        var w = 100, h = 100;
        canvas.width = w;
        canvas.height = h;
        var ctx = canvas.getContext('2d').putImageData(result, 0, 0);
        ctx.drawImage(result, 0, 0, w, h);
        $scope.imageSrc = canvas.toDataUrl('image/jpeg');
        */
        $scope.profileImageSrc = result;
      });
    };

    $scope.goToNext = function(){
      authService.saveRegistration($scope.registration).then(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
            $('.step').prev().hide();
            $('.step').next().show();
            fileUploadService.upload(profileFile, response.data.userId).then(function(response) {

            }, function(err) {

            });

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    }
    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}]);
