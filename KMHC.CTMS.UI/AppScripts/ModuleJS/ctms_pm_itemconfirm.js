var app = angular.module('ctms_pm_itemconfirm', []);

app.controller('ctms_pm_itemconfirmCtrl', ['$scope', '$http', '$filter','$location', '$routeParams', 'ctms_pm_itemconfirmServices',
        function ($scope, $http,$filter, $location, $routeParams, ctms_pm_itemconfirmServices) {

            //搜索、分页列表
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                $scope.loading = true;
                ctms_pm_itemconfirmServices.get({ CurrentPage: $scope.CurrentPage }, function (data) {
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
                $location.url('/Addctms_pm_itemconfirm');
            };
            
            //编辑
            $scope.Edit = function (obj) {
                $location.url('/Editctms_pm_itemconfirm?id='+ obj.COLUMN_17);
            };
			
			//删除
            $scope.Remove = function (obj) {
                if (!confirm('确认删除？')) return;

                $http.delete('/Api/ctms_pm_itemconfirm?id=' + obj.COLUMN_17).success(function (data) {
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


app.controller('Addctms_pm_itemconfirmCtrl', ['$scope', '$http', '$filter','$location', '$routeParams',
    function ($scope, $http,$filter, $location, $routeParams) {
        $scope.SelectInfo = [{ text: "是", value: true }, { text: "否", value: false }];

        if ($routeParams.id != undefined) {
            $http.get('/Api/ctms_pm_itemconfirm?ID=' + $routeParams.id).success(function (data) {
                $scope.Info = data;            
            });
        }

        $scope.Save = function () {
            $http.post('/Api/ctms_pm_itemconfirm', { Data: $scope.Info }).success(function (data) {
                if (data == 'ok')
                    alert('保存成功');
                else
                    alert('网络异常');
            });
        };

        $scope.GoBack = function () {
            $location.url('ctms_pm_itemconfirm');
        };
    }
]);