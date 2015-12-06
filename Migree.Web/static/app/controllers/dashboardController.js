migree.controller('DashboardController', ['$scope', function($scope) {

  new ElastiStack( document.getElementById('stack'), {
    distDragBack : 50,
    distDragMax : 150,
    onUpdateStack : function( current ) { return false; }
  });


}]);
