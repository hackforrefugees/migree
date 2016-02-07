migree.controller('matchesController', ['$scope', 'matchesService', '$timeout',
  function ($scope, matchesService, $timeout) {

    $scope.matches = [];


    $scope.matches = matchesService.matches.then(function (data) {
    $scope.matches = data;
    $timeout(function() {
       new ElastiStack(document.getElementById('stack'), {
              distDragBack: 80,
              distDragMax: 50,
              onUpdateStack: function (current) { return false; }
            });
     }, 500);
     

    $scope.flip = function ($event) {
      var card = angular.element($event.target).closest('.card');
      card.toggleClass('flipped');
    };

    });
}]);