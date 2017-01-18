var app = angular.module('ctms_hr_department', []);

app.controller('ctms_hr_departmentCtrl', ['$scope', '$http', '$filter','$location', '$routeParams', 'ctms_hr_departmentServices',
        function ($scope, $http,$filter, $location, $routeParams, ctms_hr_departmentServices) {

            //搜索、分页列表
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                $scope.loading = true;
                ctms_hr_departmentServices.get({ CurrentPage: $scope.CurrentPage }, function (data) {
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
                $location.url('/Addctms_hr_department');
            };
            
            //编辑
            $scope.Edit = function (obj) {
                $location.url('/Editctms_hr_department?id='+ obj.DEPARTMENTID);
            };
			
			//删除
            $scope.Remove = function (obj) {
                if (!confirm('确认删除？')) return;

                $http.delete('/Api/ctms_hr_department?id=' + obj.DEPARTMENTID).success(function (data) {
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


app.controller('Addctms_hr_departmentCtrl', ['$scope', '$http', '$filter','$location', '$routeParams',
    function ($scope, $http,$filter, $location, $routeParams) {
        $scope.SelectInfo = [{ text: "是", value: true }, { text: "否", value: false }];

        if ($routeParams.id != undefined) {
            $http.get('/Api/ctms_hr_department?ID=' + $routeParams.id).success(function (data) {
                $scope.Info = data;            
            });
        }

        $scope.Save = function () {
            $http.post('/Api/ctms_hr_department', { Data: $scope.Info }).success(function (data) {
                if (data == 'ok')
                    alert('保存成功');
                else
                    alert('网络异常');
            });
        };

        $scope.GoBack = function () {
            $location.url('ctms_hr_department');
        };
    }
]);