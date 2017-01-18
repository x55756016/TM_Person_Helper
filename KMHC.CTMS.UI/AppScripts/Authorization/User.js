var app = angular.module("UserManageCtrl", []);
///元数据
app.controller('UserManageCtrl', ['$scope', '$http', '$location', '$routeParams', 'DictionaryServices','UserManageServices',
    function ($scope, $http, $location, $routeParams, DictionaryServices, UserManageServices) {
        $scope.Init = function () {
            $scope.CurrentPage = 1;
            $scope.s_Name = "";
            $scope.CRUDUserRole = { AllSelected: false };
            $scope.CRUDUser = {};



            //开始用户选择后的回调
            $scope.CurrentUserSearcher = {};//当前选择用户的model，这个名称可随意
            $scope.FinishUserSearch = function (searcher_user) {
                $scope.CRUDUser[$scope.CurrentUserSearcher] = searcher_user;
            };
            $scope.ChoiceDoctor = function () {
                $scope.CurrentUserSearcher = 'MyDoctor';
            };
            $scope.ChoiceService = function () {
                $scope.CurrentUserSearcher = 'MyService';
            }
            //结束用户选择后的回调



            DictionaryServices.get({ keyWord: "UserStatus" }
                , function (data) {
                    $scope.DicUserStatusList = data.Data
                });
            DictionaryServices.get({ keyWord: "UserType" }
              , function (data) {
                  $scope.DicUserTypeList = data.Data
              });

            $http.get("/api/Role?keyWord=&key=").success(
                function (data) {
                    $scope.RoleList = data.Data;

                }).error(function (errorResponse) {
                    alert("发生错误,查询失败!")
                    console.log("$http.get.Role:" + errorResponse.data.Message);
                });
            $scope.Search();

        };

        $scope.goSearch = function () {
            $scope.CurrentPage = 1;
            $scope.Search();
        };
       
        $scope.Search = function () {
            $scope.loading = true;
            UserManageServices.get({ pageIndex: $scope.CurrentPage, keyWord: $scope.s_Name == undefined ? "" : $scope.s_Name }
                , function (data) {
                    $scope.UserList = data.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                        $scope.CurrentPage = parseInt(curPage);
                        $scope.Search();
                    });
                }, function (errorResponse) {
                    alert("发生错误,查询失败!")
                    console.log("UserManageServices.get:" + errorResponse.data.Message);
                });
            $scope.loading = false;
        };
        $scope.Init();
        $scope.Reset = function () {
            $scope.s_Name = "";
        };
        $scope.EditUser = function (u) {
            $scope.CRUDUser = $scope.Clone(u);
            //$("#userType").combobox();
            $("#modalUserEdit").modal("show");

        };
        $scope.SaveUser = function () {
            $scope.loading = true;

            console.log(JSON.stringify($scope.CRUDUser));


            $http.post("/api/UserManage", { Data: $scope.CRUDUser }).success(
                function (data) {
                    $scope.Search();
                    $("#modalUserEdit").modal("hide");
                }).error(function (errorResponse) {
                    alert("发生错误,查询失败!")
                    console.log("$http.get.UserManage:" + errorResponse.data.Message);
                });
            $scope.loading = false;
        };
        $scope.EditUserRole = function (u) {
            $scope.CRUDUserRole.User = $scope.Clone(u);
            $scope.CRUDUserRole.Roles = $scope.Clone($scope.RoleList);
            $scope.CRUDUserRole.AllSelected = false;
            $http.get("/api/UserRole?uid=" + u.UserId).success(
                function (data) {
                   if (data.Data != null && data.Data.length > 0)
                   {
                       angular.forEach(data.Data, function (ur) {
                           angular.forEach($scope.CRUDUserRole.Roles, function (r) {
                               if (r.RoleID == ur.RoleID)
                               {
                                   r.IsSelected = true;
                               }
                           });
                       });
                   }
               }).error(function (errorResponse) {
                   alert("发生错误,查询失败!")
                   console.log("$http.get.UserManage:" + errorResponse.data.Message);
               });
            $("#modalUserRoleEdit").modal("show");
        };
       
        $scope.SaveUserRole = function () {
            $scope.loading = true;
            var UserRoleList=$.grep($scope.CRUDUserRole.Roles, function (r) {
                return r.IsSelected != null && r.IsSelected == true;
            });
            $http.post("/api/UserRole", { Data: UserRoleList, ID: $scope.CRUDUserRole.User.UserId }).success(
                function (data) {
                    $("#modalUserRoleEdit").modal("hide");
                }).error(function (errorResponse) {
                    alert("发生错误,查询失败!")
                    console.log("$http.get.UserRole:" + errorResponse.data.Message);
                });
            $scope.loading = false;
        };
        $scope.ToggleAllRole = function () {
                angular.forEach($scope.CRUDUserRole.Roles, function (r) {
                    r.IsSelected = $scope.CRUDUserRole.AllSelected;
                });
        };

        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
    }
]);

app.controller("userCtrl", ["$scope", '$rootScope', "$http", "UserTypeServices", 'DictionaryServices', function ($scope, $rootScope, $http, UserTypeServices, DictionaryServices) {

    $scope.Searcher = {};
    $scope.Searcher_CurrentPage = 1;
    $scope.ResultUser = {};

    DictionaryServices.get({ keyWord: "UserType" }
              , function (data) {
                  $scope.Searcher_DicUserTypeList = data.Data
              });

    $scope.processSearch = function () {
        $scope.Searcher_CurrentPage = 1;
        $scope.search();
    };

    $scope.search = function () {
        $scope.Searcher.Name = $scope.Searcher.Name == undefined ? "" : $scope.Searcher.Name;
        $scope.Searcher.IdCard = $scope.Searcher.IdCard == undefined ? "" : $scope.Searcher.IdCard;
        $scope.Searcher.UserType = $scope.Searcher.UserType == undefined ? 0 : $scope.Searcher.UserType;
        $http({
            method: 'GET',
            url: '/User/SearchUser' + '?pageIndex=' + $scope.Searcher_CurrentPage + '&Name=' + $scope.Searcher.Name + '&IdCard=' + $scope.Searcher.IdCard + '&UserType=' + $scope.Searcher.UserType
        }).success(function (data) {
            if (data.Status != 1) {
                alert(data.Msg);
                return;
            }
            $scope.Searcher_UserList = data.Data;
            var pager = new Pager('searcher_pager', $scope.Searcher_CurrentPage, data.PagesCount, function (curPage) {
                $scope.Searcher_CurrentPage = parseInt(curPage);
                $scope.search();
            });
        }).error(function (data) {
            alert(data.Msg);
        });
    };

    $scope.choiceItem = function ($event, model) {
        $scope.ResultUser = model;
        $($event.target).parent().siblings('tr').css("background", "#fff");
        $($event.target).parent().css("background", "#d9edf7");
    };

    $scope.Ok = function () {
        $scope.$parent.FinishUserSearch($scope.ResultUser);
        $('.close').last().trigger('click');
    };
}]);