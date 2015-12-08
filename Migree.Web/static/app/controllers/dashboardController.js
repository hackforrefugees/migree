migree.controller('DashboardController', ['$scope', 'authService', 'userService', function ($scope, authService, userService) {
    'use strict';
    var result = userService.getMatches();
    console.log(result);

    new ElastiStack(document.getElementById('stack'), {
      distDragBack : 50,
      distDragMax : 150,
      onUpdateStack : function( current ) { return false; }
    });
}]);
