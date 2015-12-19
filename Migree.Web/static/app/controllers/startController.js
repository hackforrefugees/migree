migree.controller('StartCtrl', ['$scope', 'AuthenticationService', function ($scope, authService) {
	authService.logOut();
}]);
