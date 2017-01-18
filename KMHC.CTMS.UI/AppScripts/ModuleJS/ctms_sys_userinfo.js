var app = angular.module('ctms_sys_userinfo', []);

app.controller('ctms_sys_userinfoCtrl', ['$scope', '$http', '$filter','$location', '$routeParams', 'ctms_sys_userinfoServices',
        function ($scope, $http,$filter, $location, $routeParams, ctms_sys_userinfoServices) {

            //搜索、分页列表
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                $scope.loading = true;
                ctms_sys_userinfoServices.get({ CurrentPage: $scope.CurrentPage }, function (data) {
                    $scope.List = data.Data;
                    $scope.loading = false;
                    var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                        $scope.CurrentPage = parseInt(curPage);
                        $scope.Search();
                    });
                },
                    function (data) {
                        $scope.List = [];
                        $scope.loading = false;
                    }
                );
            };
			
			//  新增
            $scope.Add = function () {
                $location.url('/Addctms_sys_userinfo');
            };
            
            //编辑
            $scope.Edit = function (obj) {
                $location.url('/Editctms_sys_userinfo?id='+ obj.USERID);
            };
			
			//删除
            $scope.Remove = function (obj) {
                if (!confirm('确认删除？')) return;

                $http.delete('/Api/ctms_sys_userinfo?id=' + obj.USERID).success(function (data) {
                    if (data != "ok") {
                        alert('删除失败');
                    } else {
                        $scope.Search();
                    }
                });
            }

            $scope.Search();

        }
]);


app.controller('Addctms_sys_userinfoCtrl', ['$scope', '$http', '$filter','$location', '$routeParams',
    function ($scope, $http,$filter, $location, $routeParams) {
        $scope.SelectInfo = [{ text: "是", value: true }, { text: "否", value: false }];

        if ($routeParams.id != undefined) {
            $http.get('/Api/ctms_sys_userinfo?ID=' + $routeParams.id).success(function (data) {
                $scope.Info = data;            
            });
        }

        $scope.Save = function () {
            $http.post('/Api/ctms_sys_userinfo', { Data: $scope.Info }).success(function (data) {
                if (data == 'ok')
                    alert('保存成功');
                else
                    alert('网络异常');
            });
        };

        $scope.GoBack = function () {
            $location.url('ctms_sys_userinfo');
        };
    }
]);