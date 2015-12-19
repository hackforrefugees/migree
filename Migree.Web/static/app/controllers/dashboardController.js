migree.controller('DashboardController', ['$scope', 'AuthenticationService', 'Matches', function ($scope, authService, matches) {
    'use strict';
    var result = matches.query();    

    new ElastiStack(document.getElementById('stack'), {
      distDragBack : 50,
      distDragMax : 150,
      onUpdateStack : function( current ) { return false; }
    });
}]);
