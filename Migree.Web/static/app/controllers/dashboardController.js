migree.controller('DashboardController', ['$scope', 'authService', 'userService', function ($scope, authService, userService) {
<<<<<<< HEAD
    var result = userService.getMatches();
    console.log(result);
=======
    var result = userService.getMatches(authService.authentication.userId);

>>>>>>> grunt-connect
    new ElastiStack(document.getElementById('stack'), {
      distDragBack : 50,
      distDragMax : 150,
      onUpdateStack : function( current ) { return false; }
    });
}]);
