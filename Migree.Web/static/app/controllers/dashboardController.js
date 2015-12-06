migree.controller('DashboardController', ['$scope', 'authService', function ($scope, authService) {
    
  new ElastiStack( document.getElementById('stack'), {
    distDragBack : 50,
    distDragMax : 150,
    onUpdateStack : function( current ) { return false; }
  });
}]);
