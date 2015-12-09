migree.controller('StartController', ['$scope', 'authService', function($scope, authService) {
	authService.logOut();
}]);
