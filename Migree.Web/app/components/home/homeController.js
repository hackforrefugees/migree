migree.controller('homeController', ['$scope', 'AuthenticationService', function ($scope, authService) {
	authService.logOut();
}]);
