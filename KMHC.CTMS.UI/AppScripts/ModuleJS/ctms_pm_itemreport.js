var app = angular.module('ctms_pm_itemreport', []);

app.controller('ctms_pm_itemreportCtrl', ['$scope', '$http', '$filter','$location', '$routeParams', 'ctms_pm_itemreportServices',
        function ($scope, $http,$filter, $location, $routeParams, ctms_pm_itemreportServices) {

            //搜索、分页列表
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                $scope.loading = true;
                ctms_pm_itemreportServices.get({ CurrentPage: $scope.CurrentPage }, function (data) {
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
                $location.url('/Addctms_pm_itemreport');
            };
            
            //编辑
            $scope.Edit = function (obj) {
                $location.url('/Editctms_pm_itemreport?id='+ obj.TASKID);
            };
			
			//删除
            $scope.Remove = function (obj) {
                if (!confirm('确认删除？')) return;

                $http.delete('/Api/ctms_pm_itemreport?id=' + obj.TASKID).success(function (data) {
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


app.controller('Addctms_pm_itemreportCtrl', ['$scope', '$http', '$filter','$location', '$routeParams',
    function ($scope, $http,$filter, $location, $routeParams) {
        $scope.SelectInfo = [{ text: "是", value: true }, { text: "否", value: false }];

        if ($routeParams.id != undefined) {
            $http.get('/Api/ctms_pm_itemreport?ID=' + $routeParams.id).success(function (data) {
                $scope.Info = data;            
            });
        }

        $scope.Save = function () {
            $http.post('/Api/ctms_pm_itemreport', { Data: $scope.Info }).success(function (data) {
                if (data == 'ok')
                    alert('保存成功');
                else
                    alert('网络异常');
            });
        };

        $scope.GoBack = function () {
            $location.url('ctms_pm_itemreport');
        };
    }
]);