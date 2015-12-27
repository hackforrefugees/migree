migree.controller('homeController', ['authenticationService', function (authenticationService) {
  authenticationService.logOut();
}]);
