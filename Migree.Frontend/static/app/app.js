var app = angular.module('migreeApp', [
    'ngRoute',
    'ui.router'
]);

app.constant('config', {
  api: '/'
})

app.config(function ($routeProvider, $locationProvider, $stateProvider, $urlRouterProvider) {


    //routing DOESN'T work without html5Mode
    $locationProvider.html5Mode({
      enabled: true,
      requireBase: false
    });


  $stateProvider
    .state('login', {
			url: '/login',
			templateUrl: '/views/login.html',
			controller: 'LoginController'
	  })
    .state('home', {
      url: '/',
      templateUrl: '/views/start.html',
      controller: 'StartController'
    })
    .state('register', {
      url: '/register/:who',
      templateUrl: '/views/register.html',
      controller: 'RegisterController'
    })
    .state('dashboard', {
      url: '/dashboard',
      templateUrl: '/views/dashboard.html',
      controller: 'DashboardController'
    })
    .state('forgot', {
      url: '/forgot',
      templateUrl: '/views/forgot.html',
      controller: 'ForgotController'
    })
    .state('404', {
      url: '/notfound',
      templateUrl: '/views/404.html',
      controller: function($scope) {
        // do something here?
      }
    })

    $urlRouterProvider.otherwise('/404');
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

  $scope.cities = [
    { value: '2', label: 'Gothenburg' },
    { value: '1', label: 'Stockholm' },
    { value: '3', label: 'Malmo' }
];


  $scope.setLabel = function(c){
    console.log(c)
  };

  $scope.goToNext = function(){

    $('.step').prev().hide();
    $('.step').next().show();
  }

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
      $('.avatar-upload i').remove();
      $('.avatar-upload img').remove();
      $('.avatar-upload').append('<img width="100%" src="'+e.target.result+'" />')
     
    }

    reader.readAsDataURL(event.el[0].files[0]);

  };


});


app.controller('StartController', function($scope, $http){



});

app.controller('DashboardController', function($scope, $http){

    new ElastiStack( document.getElementById('stack'), {
      distDragBack : 50,
      distDragMax : 150,
      onUpdateStack : function( current ) { return false; }
    } );

});


/*===functions===*/

function validateEmail(email) {
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return re.test(email);
}

