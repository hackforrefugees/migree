'use strict';
migree.controller('registerController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

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

    $scope.getFile = function () {
      $scope.progress = 0;
      var fileReader = new FileReader();
      var file = fileReader.readAsDataUrl($scope.file);
      $scope.imageSrc = file;
    };


    $scope.goToNext = function(){
      authService.saveRegistration($scope.registration).then(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
            $('.step').prev().hide();
            $('.step').next().show();
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
