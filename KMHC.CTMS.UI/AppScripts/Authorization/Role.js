var app = angular.module('Role', []);

app.controller('RoleCtrl', ['$scope', '$http', '$location', '$routeParams', 'RoleServices',
        function ($scope, $http, $location, $routeParams, RoleServices) {

            //搜索、分页列表
            $scope.RoleName = '';
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                $scope.loading = true;
                $http.get('Api/Role/Get?RoleName='+$scope.RoleName+'&CurrentPage='+$scope.CurrentPage ).success(function (data) {
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

            $scope.SearchCondition = function () {
                $scope.CurrentPage = 1;
                $scope.Search();
            };

            //新增
            $scope.Add = function () {
                $location.url('/AddRole');
            };

            //删除
            $scope.Remove = function (obj) {
                KMConfirm({
                    msg: '是否删除当前记录?',
                    btnMsg: '删除'
                }, function (e) {
                    $http.delete('/Api/Role/Delete?roleId=' + obj.RoleID).success(function (data) {
                        if (data != "ok") {
                            alert("删除失败！");
                        } else {
                            $scope.Search();
                        }
                    }).error(function (response) {
                        alert(response);
                    });
                });
            };

            //重置
            $scope.Reset = function () {
                $scope.RoleName = null;
            };

            $scope.Search();
        }
]);

app.controller('AddRoleCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $scope.Info = {};
            $scope.Info.SystemCategory = '0';
            $http.get('/Api/Dictionary?Keyword=SystemCategory').success(function (data) {
                $scope.CateData = data.Data;
            }).error(function (response) {
                $scope.CateData = [];
            });

            $http.get('/Api/Role/Get').success(function (data) {
                for (var i = 0; i < data.ExtFuns.length; i++) {
                    var _fun = data.ExtFuns[i];
                    for (var k = 0; k < _fun.Permissions.length; k++) {
                        data.ExtFuns[i]['Permissions'][k]['DataRange'] = [];
                        data.ExtFuns[i]['Permissions'][k]['DataRange2'] = [];
                        var xmlDoc = $.parseXML(_fun.Permissions[k].Remark);
                        var $xml = $(xmlDoc);
                        $xml.find("item").each(function (o) {
                            var _item = { relationship: $(this).children("relationship").text(), nameID: $(this).children("nameID").text(), name: $(this).children("name").text(), operation: $(this).children("operation").text(), value: $(this).children("value").text() };
                            data.ExtFuns[i]['Permissions'][k]['DataRange'].push(_item);
                        });

                        var orgs = $.parseJSON(_fun.Permissions[k].CreateUserName);
                        data.ExtFuns[i]['Permissions'][k]['DataRange2'] = orgs == null ? [] : orgs;

                        data.ExtFuns[i]['Permissions'][k]['DataRange'].length > 0 ? data.ExtFuns[i]['Permissions'][k].Tool = true : data.ExtFuns[i]['Permissions'][k].Tool = false;
                        data.ExtFuns[i]['Permissions'][k]['DataRange2'].length > 0 ? data.ExtFuns[i]['Permissions'][k].Tool2 = true : data.ExtFuns[i]['Permissions'][k].Tool2 = false;
                    }
                }
                $scope.Info.RoleID = data.RoleID;
                $scope.Info.RoleName = data.RoleName;
                $scope.Info.SystemCategory = data.SystemCategory + '';
                $scope.Info.Remark = data.Remark;
                $scope.ExtFuns = data.ExtFuns;
            }).error(function (response) {
                $scope.ExtFuns = [];
            });

            //返回
            $scope.GoBack = function () {
                $location.url('/Role');
            };

            //保存
            $scope.Save = function () {
                var RoleFunc = [];
                var ExtFuns = $scope.ExtFuns;
                for (var i = 0; i < ExtFuns.length; i++) {
                    var _permiss = ExtFuns[i].Permissions;
                    for (var j = 0; j < _permiss.length; j++) {
                        var _fun = {};
                        _fun.FunctionID = ExtFuns[i].FunctionID;
                        _fun.RoleID = '';
                        _fun.DataRange = '';
                        var _txt = '';
                        for (var k = 0; k < _permiss[j].DataRange.length; k++) {
                            var _range = _permiss[j].DataRange[k];
                            _txt += _range.name + '#' + _range.nameID + '#' + _range.operation + '#' + _range.relationship + '#' + _range.value + ',';
                        }

                        _fun.RoleFunOrgs = _permiss[j].DataRange2;

                        if (_permiss[j].IsDeleted) {
                            _fun.DataRange = _txt;
                            _fun.PermissionValue = _permiss[j].PermissionValue;
                            RoleFunc.push(_fun);
                        }
                    }
                }
                $scope.Info.RoleFuns = RoleFunc;

                $http.post('/Api/Role/Post', { Data: $scope.Info }).success(function (data) {
                    if (data == 'ok') {
                        alert("当前记录保存成功！");
                    }
                    else {
                        alert("网络异常！");
                    }
                }).error(function (error) {
                    alert("网络异常！");
                });
            };

            //MetaDataPicker
            $scope.SelectedMetaDataID = 0;
            $scope.SelectedMetaDataName = "";
            $scope.SetMetaData = function (text, value) {
                $scope.SelectedMetaDataID = value;
                $scope.SelectedMetaDataName = text;
            };

            $scope.SaveMetaDataPicker = function () {
                var _item = { relationship: 'and', nameID: $scope.SelectedMetaDataID, name: $scope.SelectedMetaDataName, operation: '>', value: '' };
                $scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange'].push(_item);
                $("#modalMetaDataPicker").modal("hide");
            };

            $scope.CheckItem = function (pid, id) {
                $scope.TPid = pid;
                $scope.Tid = id;
                $('#modalMetaDataPicker').modal('toggle');
            };

            //移除
            $scope.Remove = function (p, k, i) {
                $scope.ExtFuns[p]['Permissions'][k]['DataRange'].splice(k, 1);
            };

            $scope.CheckItem2 = function (pid, id) {
                $scope.TPid = pid;
                $scope.Tid = id;
                $('#modalOrganization').modal('toggle');
            };

            $scope.updateSelection = function (node) {
                console.info(node);
                $scope.OrgID = node.value;
                $scope.OrgName = node.text;
                $scope.OrgType = node.tags;
                $scope.Exit = false;
            };
            $scope.updateCheckd = function (node) {

            };

            $scope.SaveOrganization = function () {
                console.info("baocun");
                var _arr = $scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange2'];
                $.each(_arr, function (i, item) {
                    if (item.OrgID == $scope.OrgID) {
                        $scope.Exit = true;
                        return false;
                    }
                });
                if ($scope.Exit) {
                    return;
                }
                var _item = { OrgType: $scope.OrgType, OrgID: $scope.OrgID, OrgName: $scope.OrgName };
                $scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange2'].push(_item);
                $("#modalOrganization").modal("hide");
                console.info($scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange2']);

            };
            
            //移除
            $scope.Remove2 = function (p, k, i) {
                $scope.ExtFuns[p]['Permissions'][k]['DataRange2'].splice(k, 1);
            };

        }
]);

app.controller('RoleViewCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $scope.Info = {};
            $http.get('/Api/Role?roleId=' + $routeParams.roleId).success(function (data) {
                for (var i = 0; i < data.ExtFuns.length; i++) {
                    var _fun = data.ExtFuns[i];
                    for (var k = 0; k < _fun.Permissions.length; k++) {
                        data.ExtFuns[i]['Permissions'][k]['DataRange'] = [];
                        var xmlDoc = $.parseXML(_fun.Permissions[k].Remark);
                        var $xml = $(xmlDoc);
                        $xml.find("item").each(function (o) {
                            var _item = { relationship: $(this).children("relationship").text(), nameID: $(this).children("nameID").text(), name: $(this).children("name").text(), operation: $(this).children("operation").text(), value: $(this).children("value").text() };
                            data.ExtFuns[i]['Permissions'][k]['DataRange'].push(_item);
                        });

                        if (data.ExtFuns[i]['Permissions'][k]['DataRange'].length > 0)
                            data.ExtFuns[i]['Permissions'][k].Tool = true;
                    }
                }
                $scope.Info.RoleID = data.RoleID;
                $scope.Info.RoleName = data.RoleName;
                $scope.Info.SystemCategory = data.SystemCategory + '';
                $scope.Info.Remark = data.Remark;
                $scope.ExtFuns = data.ExtFuns;
            }).error(function (response) {
                $scope.ExtFuns = [];
            });

            $http.get('/Api/Dictionary?Keyword=SystemCategory').success(function (data) {
                $scope.CateData = data.Data;
            }).error(function (response) {
                $scope.CateData = [];
            });

            //返回
            $scope.GoBack = function () {
                //$location.url('/Role');
                history.go(-1)
            };
        }
]);

app.controller('EditRoleCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $scope.Info = {};
            $http.get('/Api/Role/Get?roleId=' + $routeParams.roleId).success(function (data) {
                for (var i = 0; i < data.ExtFuns.length; i++) {
                    var _fun = data.ExtFuns[i];
                    for (var k = 0; k < _fun.Permissions.length; k++) {
                        data.ExtFuns[i]['Permissions'][k]['DataRange'] = [];
                        data.ExtFuns[i]['Permissions'][k]['DataRange2'] = [];
                        var xmlDoc = $.parseXML(_fun.Permissions[k].Remark);
                        var $xml = $(xmlDoc);
                        $xml.find("item").each(function (o) {
                            var _item = { relationship: $(this).children("relationship").text(), nameID: $(this).children("nameID").text(), name:$(this).children("name").text(), operation: $(this).children("operation").text(), value: $(this).children("value").text() };
                            data.ExtFuns[i]['Permissions'][k]['DataRange'].push(_item);
                        });

                        var orgs = $.parseJSON(_fun.Permissions[k].CreateUserName);
                        data.ExtFuns[i]['Permissions'][k]['DataRange2'] = orgs==null?[]:orgs;

                        data.ExtFuns[i]['Permissions'][k]['DataRange'].length > 0 ? data.ExtFuns[i]['Permissions'][k].Tool = true : data.ExtFuns[i]['Permissions'][k].Tool = false;
                        data.ExtFuns[i]['Permissions'][k]['DataRange2'].length > 0 ? data.ExtFuns[i]['Permissions'][k].Tool2 = true : data.ExtFuns[i]['Permissions'][k].Tool2 = false;
                    }
                }
                $scope.Info.RoleID = data.RoleID;
                $scope.Info.RoleName = data.RoleName;
                $scope.Info.SystemCategory = data.SystemCategory + '';
                $scope.Info.Remark = data.Remark;
                $scope.ExtFuns = data.ExtFuns;
            }).error(function (response) {
                $scope.ExtFuns = [];
            });

            $http.get('/Api/Dictionary?Keyword=SystemCategory').success(function (data) {
                $scope.CateData = data.Data;
            }).error(function (response) {
                $scope.CateData = [];
            });

            //返回
            $scope.GoBack = function () {
                $location.url('/Role');
            };

            //保存
            $scope.Save = function () {
                var RoleFunc = [];
                var ExtFuns = $scope.ExtFuns;
                for (var i = 0; i < ExtFuns.length; i++) {
                    var _permiss = ExtFuns[i].Permissions;
                    for (var j = 0; j < _permiss.length; j++) {
                        var _fun = {};
                        _fun.FunctionID = ExtFuns[i].FunctionID;
                        _fun.RoleID = '';
                        _fun.DataRange = '';
                        var _txt = '';
                        for (var k = 0; k < _permiss[j].DataRange.length; k++) {
                            var _range = _permiss[j].DataRange[k];
                            _txt += _range.name + '#' + _range.nameID + '#' + _range.operation + '#' + _range.relationship + '#' + _range.value + ',';
                        }
                        
                        _fun.RoleFunOrgs = _permiss[j].DataRange2;

                        if (_permiss[j].IsDeleted) {
                            _fun.DataRange = _txt;
                            _fun.PermissionValue = _permiss[j].PermissionValue;
                            RoleFunc.push(_fun);
                        }  
                    }
                }
                $scope.Info.RoleFuns = RoleFunc;

                $http.post('/Api/Role/Post', { Data: $scope.Info }).success(function (data) {
                    if (data == 'ok') {
                        alert("当前记录保存成功！");
                    }
                    else {
                        alert("网络异常！");
                    }
                }).error(function (error) {
                    alert("网络异常！");
                });
            };

            //移除
            $scope.Remove = function (p,k,i) {
                $scope.ExtFuns[p]['Permissions'][k]['DataRange'].splice(k,1);
            };

            //MetaDataPicker
            $scope.SelectedMetaDataID = 0;
            $scope.SelectedMetaDataName = "";
            $scope.SetMetaData = function (text, value) {
                $scope.SelectedMetaDataID = value;
                $scope.SelectedMetaDataName = text;
            };

            $scope.SaveMetaDataPicker = function () {
                var _item = { relationship: 'and', nameID: $scope.SelectedMetaDataID, name: $scope.SelectedMetaDataName, operation: '>', value: '' };
                $scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange'].push(_item);
                $("#modalMetaDataPicker").modal("hide");
            };

            $scope.CheckItem = function (pid, id) {
                $scope.TPid = pid;
                $scope.Tid = id;
                $('#modalMetaDataPicker').modal('toggle');
            };

            $scope.CheckItem2 = function (pid, id) {
                $scope.TPid = pid;
                $scope.Tid = id;
                $('#modalOrganization').modal('toggle');
            };

            $scope.updateSelection = function (node) {
                console.info(node);
                $scope.OrgID = node.value;
                $scope.OrgName = node.text;
                $scope.OrgType = node.tags;
                $scope.Exit = false;
            };

            $scope.updateCheckd = function (node) {
               
            };

            $scope.SaveOrganization = function () {
                console.info("baocun");
                var _arr = $scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange2'];
                $.each(_arr, function (i, item) {
                    if (item.OrgID == $scope.OrgID) {
                        $scope.Exit = true;
                        return false;
                    }
                });
                if ($scope.Exit) {
                    return;
                }
                var _item = { OrgType: $scope.OrgType, OrgID: $scope.OrgID, OrgName: $scope.OrgName };
                $scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange2'].push(_item);
                console.info($scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange2']);
                $("#modalOrganization").modal("hide");
            };

            //移除
            $scope.Remove2 = function (p, k, i) {
                $scope.ExtFuns[p]['Permissions'][k]['DataRange2'].splice(k, 1);
            };
        }
]);