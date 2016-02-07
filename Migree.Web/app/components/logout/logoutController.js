migree.controller('logoutController', ['authenticationService', '$state',
  function (authenticationService, $state) {
    authenticationService.logOut();
    $state.go('home');
  }]);
