var app = angular.module("Permission", []);
///元数据
app.controller('PermissionCtrl', ['$scope', '$http', '$location', '$routeParams', 'DictionaryServices',
    function ($scope, $http, $location, $routeParams, DictionaryServices) {
        $scope.Init = function () {
            $http.get("/api/Permission").success(
              function (data) {
                  $scope.PermissionList = data.Data;

              });
        };
        $scope.Init();
    }
]);