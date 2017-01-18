var app = angular.module('DictionaryManage', []);

app.controller('DictManageCtrl', ['$scope', '$http', '$location', '$routeParams', 'DictionaryManageServices',
        function ($scope, $http, $location, $routeParams, DictionaryManageServices) {
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                DictionaryManageServices.get({ CurrentPage: $scope.CurrentPage,Type:1, Name: $scope.DictionaryName }, function (data) {
                    $scope.List = data.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                        $scope.CurrentPage = parseInt(curPage);
                        $scope.Search();
                    });
                },
                    function (data) {
                        $scope.List = data;
                    }
                );
            };

            $scope.Search();

            $scope.SearchCondition = function () {
                $scope.CurrentPage = 1;
                $scope.Search();
            };

            //新增
            $scope.Add = function () {
                $location.url('AddDictionary');
            };

            //删除
            $scope.Remove = function (id) {
                if (confirm("是否删除当前记录?")) {
                    $http.delete('/Api/DictionaryManage?Id=' + id).success(function (data) {
                        if (data != "ok") {
                            alert("删除失败！");
                        } else {
                            $scope.Search();
                        }
                    }).error(function (response) {
                        alert(response);
                    });
                }
            };

            //重置
            $scope.Reset = function () {
                $scope.DictionaryName = null;
            };
        }
]);

app.controller('AddDictCtrl', ['$scope', '$http', '$location', '$routeParams', 'DictionaryManageServices',
        function ($scope, $http, $location, $routeParams, DictionaryManageServices) {
            $scope.Info = {};
            $scope.Info.SystemCategory = '0';
            $scope.System = [{ Name: '列表', Code: 'List' }];
            $http.get('/Api/Dictionary?Keyword=SystemCategory').success(function (data) {
                $scope.CateData = data.Data;
            }).error(function (xlr) {
                $scope.CateData = [];
            });

            $scope.CurrentPage = 1;
            $scope.GetFather = function () {
                DictionaryManageServices.get({  CurrentPage: $scope.CurrentPage },function (data) {
                    $scope.Father = data.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                        $scope.CurrentPage = parseInt(curPage);
                        $scope.GetFather();
                    });
                },
                    function (data) {
                        $scope.Father = data;
                    }
                );
            };

            $scope.GetFather();

            //父字典选择
            $scope.CheckFather = function (obj) {
                $scope.Info.FatherName = obj.DictionaryName;
                $scope.Info.FatherId = obj.DictionaryId;
                $('#modalFather').modal('toggle');
            };

            //字典类型选择
            $scope.CheckSystem = function (obj) {
                $scope.Info.DictionCategoryName = obj.Name;
                $('#modalSystem').modal('toggle');
            };

            //保存
            $scope.Save = function () {
                $http.post('/Api/DictionaryManage', { Data: $scope.Info }).success(function (data) {
                    if (data == "ok") {
                        alert("保存成功！");
                    } else {
                        alert("保存失败！");
                    }
                });
            };

            //返回
            $scope.GoBack = function () {
                $location.url('DictionaryManage');
            };
        }
]);

app.controller('EditDictCtrl', ['$scope', '$http', '$location', '$routeParams', 'DictionaryManageServices',
        function ($scope, $http, $location, $routeParams, DictionaryManageServices) {
            $scope.Info = {};

            $http.get('/Api/DictionaryManage?Id='+$routeParams.Id).success(function (data) {
                $scope.Info = data;
                if (data != "") {
                    var _father = $scope.Info.FatherId.split('#');
                    $scope.Info.FatherId = _father[0];
                    $scope.Info.FatherName = _father[1];
                }
            })

            $scope.Info.SystemCategory = '0';
            $scope.System = [{ Name: '列表', Code: 'List' }];
            $http.get('/Api/Dictionary?Keyword=SystemCategory').success(function (data) {
                $scope.CateData = data.Data;
            }).error(function (xlr) {
                $scope.CateData = [];
            });

            $scope.CurrentPage = 1;
            $scope.GetFather = function () {
                DictionaryManageServices.get({ CurrentPage: $scope.CurrentPage }, function (data) {
                    $scope.Father = data.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                        $scope.CurrentPage = parseInt(curPage);
                        $scope.GetFather();
                    });
                },
                    function (data) {
                        $scope.Father = data;
                    }
                );
            };

            $scope.GetFather();

            //父字典选择
            $scope.CheckFather = function (obj) {
                $scope.Info.FatherName = obj.DictionaryName;
                $scope.Info.FatherId = obj.DictionaryId;
                $('#modalFather').modal('toggle');
            };

            //字典类型选择
            $scope.CheckSystem = function (obj) {
                $scope.Info.DictionCategoryName = obj.Name;
                $('#modalSystem').modal('toggle');
            };

            //保存
            $scope.Save = function () {
                $http.post('/Api/DictionaryManage', { Data: $scope.Info }).success(function (data) {
                    if (data == "ok") {
                        alert("保存成功！");
                    } else {
                        alert("保存失败！");
                    }
                });
            };

            //返回
            $scope.GoBack = function () {
                $location.url('DictionaryManage');
            };

            //清除
            $scope.Clear = function () {
                $scope.Info.FatherName = null;
                $scope.Info.FatherId = null;
            };
        }
]);
