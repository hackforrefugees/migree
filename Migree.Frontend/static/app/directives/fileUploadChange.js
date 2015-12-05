migree.directive('fileUploadChange', [function() {
  'use strict';
  return {
    restrict: "A",
    scope: {
        handler: '&'
    },
    link: function(scope, element) {
      element.change(function(event) {
        scope.$apply(function() {
          var params = {event: event, el: element};
          scope.handler({params: params});
        });
      });
    }
  };
}]);
