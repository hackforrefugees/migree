var app = angular.module('migreeApp', [
    'ngRoute'
]);

app.constant('config', {
  api: '/'
})


app.config(function ($routeProvider, $locationProvider) {

    //routing DOESN'T work without html5Mode
    $locationProvider.html5Mode({
      enabled: true,
      requireBase: false
    });

	$routeProvider.when('/',  {
	            templateUrl:'/views/start.html', 
	            controller: 'StartController'
	        }).when('/login',  {
              templateUrl:'/views/login.html', 
              controller: 'LoginController'
          }).when('/register/:who',  {
              templateUrl:'/views/register.html', 
              controller: 'RegisterController'
          }).when('/dashboard',  {
              templateUrl:'/views/dashboard.html', 
              controller: 'DashboardController'
          }).when('/forgot',  {
              templateUrl:'/views/forgot.html', 
              controller: 'ForgotController'
          }).otherwise({
	            templateUrl: '/views/404.html',
	        }); 
});


app.directive('fileUploadChange', [function() {
        'use strict';

        return {
            restrict: "A",

            scope: {
                handler: '&'
            },
            link: function(scope, element){

                element.change(function(event){

                    scope.$apply(function(){
                     // console.log(event);
                        var params = {event: event, el: element};
                        scope.handler({params: params});
                    });
                });
            }

        };
    }]);



app.controller('MasterController', function($scope, $http){




});


app.controller('LoginController', function($scope, $http, $location){


  $scope.login = function(){
    $http({
      method: 'GET',
      url: 'ajax/login.json'
    }).then(function successCallback(response) {
      if(!response.error)
        $location.path('/dashboard');
      else
        $scope.message = "Invalid login.";
    }, function errorCallback(response) {
      // called asynchronously if an error occurs
      // or server returns response with an error status.
    });
    
  }



});

app.controller('ForgotController', function($scope, $http, $location){




});


app.controller('RegisterController', function($scope, $http){


 $scope.register = function(){

    var firstname = $scope.firstname; 
    var email = $scope.email; 
    var password = $scope.password; 
    var repassword = $scope.repassword; 

    if(validateEmail(email)){

      $http({
        method: 'POST',
        url: 'ajax/login.json'
      }).then(function successCallback(response) {
        if(!response.error)
          $location.path('/dashboard');
        else
          $scope.message = "Invalid login.";
      }, function errorCallback(response) {
        // called asynchronously if an error occurs
        // or server returns response with an error status.
      });
      
    } else {
      $scope.message = 'Email is not valid.';
    }
  };

  $scope.avatarUpload = function(event){

    var reader = new FileReader();

    reader.onload = function (e) {
      $('.avatar-upload').empty();
      $('.avatar-upload').append('<img width="100%" src="'+e.target.result+'" />')
     
    }

    reader.readAsDataURL(event.el[0].files[0]);

  };

  $scope.goToNext = function(slide){

  }

});


app.controller('StartController', function($scope, $http){



});

app.controller('DashboardController', function($scope, $http){

    new ElastiStack( document.getElementById('stack'), {
      // distDragBack: if the user stops dragging the image in a area that does not exceed [distDragBack]px 
      // for either x or y then the image goes back to the stack 
      distDragBack : 50,
      // distDragMax: if the user drags the image in a area that exceeds [distDragMax]px 
      // for either x or y then the image moves away from the stack 
      distDragMax : 150,
      // callback
      onUpdateStack : function( current ) { return false; }
    } );

});



/*===functions===*/

function validateEmail(email) {
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return re.test(email);
}