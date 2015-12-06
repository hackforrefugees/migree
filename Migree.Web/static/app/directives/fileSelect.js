migree.directive('fileSelect', [function() {
  return {
    link: function($scope, elem) {
      console.log('fileSelect');
      elem.bind('change', function(e){
        $scope.file = (e.srcElement || e.target).files[0];
        $scope.getFile();
      });
    }
  };
}]);
