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
	            templateUrl:'/views/login.html', 
	            controller: 'LoginController'
	        }).otherwise({
	            templateUrl: '/views/404.html',
	        }); 
});



app.controller('MasterController', function($scope, $http){




});



app.controller('LoginController', function($scope, $http){




});