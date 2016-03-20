migree.controller('homeController', ['authenticationService', '$state',
  function (authenticationService, $state) {
    if (authenticationService.isAuthenticated()) {
      $state.go('matches');
    }
  }]);
