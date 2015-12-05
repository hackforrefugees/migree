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