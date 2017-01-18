var loginApp = angular.module('loginApp', ['ngRoute',
        'ngResource']);
loginApp.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('pwmatch', v);
                });
            });
        }
    }
}]);
loginApp.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/Login', { templateUrl: '/Views/User/login.html', controller: 'LoginCtrl' });
        $routeProvider.when('/Reset/:token', { templateUrl: '/Views/User/reset.html', controller: 'ResetCtrl' });
        $routeProvider.otherwise({ redirectTo: '/Login' });
    }
]).controller('LoginCtrl', function ($scope, $http) {
    $scope.loading = false;
    $scope.formData = {};
    $scope.resetData = {};
    $scope.resetData.ResetType = 0;

    $scope.processForm = function () {
        $scope.loading = true;
        $http({
            method: 'POST',
            url: '/User/UserLogin',
            data: $.param($scope.formData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            if (data.Status != 1) {
                alert(data.Msg);
                $scope.loading = false;
                return;
            }
            $scope.loading = false;
            location = "/#/";//登录成功后重定向到首页
        }).error(function (data) {
            alert(data.Msg);
        });
    };

    $scope.processRegister = function () {
        $scope.loading = true;
        $http({
            method: 'POST',
            url: '/User/UserRegister',
            data: $.param($scope.registerData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            alert(data.Msg);
            $scope.loading = false;
            if (data.Status == 1) {
                $(".modal").trigger("click");
            }
        }).error(function (data) {
            alert(data.Msg);
        });
    };

    $scope.processReset = function () {

        if (angular.isUndefined($scope.resetData))
            return;

        if ($scope.resetData.ResetType == 0) {
            if (angular.isUndefined($scope.resetData.Email)) {
                $scope.resetEmailError = 'has-error';
                return;
            }
            if ($scope.resetData.Email == '') {
                $scope.resetEmailError = 'has-error';
                return;
            }
            $scope.resetEmailError = '';
        } else if ($scope.resetData.ResetType == 1) {
            if (angular.isUndefined($scope.resetData.MobilePhone)) {
                $scope.resetMobilePhoneError = 'has-error';
                return;
            }
            if ($scope.resetData.MobilePhone == '') {
                $scope.resetMobilePhoneError = 'has-error';
                return;
            }
            $scope.resetMobilePhoneError = '';
        }

        $scope.loading = true;
        $http({
            method: 'POST',
            url: '/User/UserResetPwd',
            data: $.param($scope.resetData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            alert(data.Msg);
            $scope.loading = false;
            if (data.Status == 1) {
                $(".modal").trigger("click");
            }
        }).error(function (data) {
            alert(data.Msg);
        });
    };
}).controller('ResetCtrl', function ($scope, $routeParams, $http, $location) {
    $scope.loading = false;
    $scope.token = $routeParams.token;

    $scope.processForm = function () {
        $scope.loading = true;
        $scope.resetData.ResetToken = $scope.token;
        $http({
            method: 'POST',
            url: '/User/ResetUserPassword',
            data: $.param($scope.resetData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            alert(data.Msg);
            $scope.loading = false;
            if (data.Status == 1) {
                $(".modal").trigger("click");
                $location.path('/Login');
            }
        }).error(function (data) {
            alert(data.Msg);
        });
    };
});