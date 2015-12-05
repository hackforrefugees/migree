migree.controller('DashboardController', ['$scope', function($scope) {

  new ElastiStack( document.getElementById('stack'), {
    // distDragBack: if the user stops dragging the image in a area that does not exceed [distDragBack]px
    // for either x or y then the image goes back to the stack
    distDragBack : 50,
    // distDragMax: if the user drags the image in a area that exceeds [distDragMax]px
    // for either x or y then the image moves away from the stack
    distDragMax : 150,
    // callback
    onUpdateStack : function( current ) { return false; }
  });

}]);
