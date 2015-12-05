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
          }).otherwise({
	            templateUrl: '/views/404.html',
	        }); 
});



app.controller('MasterController', function($scope, $http){




});


app.controller('LoginController', function($scope, $http){


  $scope.login = function(){
    console.log('login')
    $location.path('/dashboard');
  }



});

app.controller('RegisterController', function($scope, $http){




});


app.controller('StartController', function($scope, $http){



});