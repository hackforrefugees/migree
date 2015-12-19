migree.controller('StartController', ['$scope', 'AuthenticationService', function ($scope, authService) {
	authService.logOut();
}]);
