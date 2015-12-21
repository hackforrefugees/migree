migree.controller('homeController', ['AuthenticationService', function (authService) {
	authService.logOut();
}]);
