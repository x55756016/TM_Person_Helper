var app = angular.module("OrganizationManage", []);

app.controller('OrganizationManageCtrl', ['$scope','$window', '$http', '$location', '$routeParams', 'OrganizationManageServices',
    function ($scope, $window, $http, $location, $routeParams, OrganizationManageServices) {
        $http.get('/Api/HRUserPost/GetDict?code=PostCode').success(function (data) {
            $scope.PostCodes = data.Data;
        });
        $http.get('/Api/HRUserPost/GetDict?code=PostLevel').success(function (data) {
            $scope.PostLevels = data.Data;
        });

        $scope.updateSelection = function (node) {
            $scope.PostID = node.value;
            $scope.Search();
        };

        $scope.Node = {};
        $scope.updateCheckd = function (node) {
            $scope.Node = node;
            console.info($scope.Node);
        };

        $scope.DelOrg = function () {
            if ($scope.Node.value == null) {
                KMAlert({
                    msg:'请先选择任一组织架构！'
                });
                return;
            }
            if ($scope.Node.nodes!= null&&$scope.Node.nodes.length!=0) {
                KMAlert({
                    msg: '该组织架构有子数据，请先删除子数据！'
                });
                return;
            }

            $http.get('/Api/HRUserPost/DelOrg?id='+$scope.Node.value+'&Type='+$scope.Node.tags).success(function (data) {
                if (data.IsSuccess) {
                    $window.location.reload();
                } else {
                    KMAlert({
                        msg: data.ErrorMsg
                    });
                }
            });
            console.info($scope.Node);
        };
        $scope.EditOrg = function () {
            if ($scope.Node.value == null) {
                KMAlert({
                    msg: '请先选择任一组织架构！'
                });
                return;
            }

            switch ($scope.Node.tags) {
                case "0":
                    if ($scope.Company == null) $scope.Company = {};
                    $scope.Company.CName = $scope.Node.text;
                    $scope.Company.CompanyID = $scope.Node.value;
                    $scope.AddCompany();
                    break;
                case "1":
                    var _parentNode = $('#tree').treeview('getParent', $scope.Node.nodeId);
                    if ($scope.Department == null) $scope.Department = {};
                    $scope.Department.DepartmentID = $scope.Node.value;
                    $scope.Department.DepartmentName = $scope.Node.text;
                    $scope.Department.CName = _parentNode.text;
                    $scope.Department.CompanyID = _parentNode.value;
                    $scope.AddDepartment();
                    break;
                case "2":
                    var _parentNode = $('#tree').treeview('getParent', $scope.Node.nodeId);
                    var _topParentNode = $('#tree').treeview('getParent', _parentNode.nodeId);
                    if ($scope.Post == null) $scope.Post = {};
                    $scope.Post.CompanyID = _topParentNode.value;
                    $scope.Post.CompanyName = _topParentNode.text;
                    $scope.Post.DepartmentID = _parentNode.value;
                    $scope.Post.DepartmentName = _parentNode.text;
                    $scope.Post.PostID = $scope.Node.value;
                    $scope.Post.PostName = $scope.Node.text;
                    $scope.AddPost();
                    break;
                default:
                    break;
            }
            console.info($scope.Node);
        };

        $scope.SaveEditOrg = function () {
            $http.get('/Api/HRUserPost/SaveOrg?id=' + $scope.Node.value + '&Type=' + $scope.Node.tags + '&text=' + $scope.Node.text).success(function (data) {

            });
        };

        $scope.Edit = function (obj) {
            console.info(obj);
            if ($scope.UserPost == null) $scope.UserPost = {};
            $scope.UserPost.EmployeepostID = obj.EmployeepostID;
            $scope.UserPost.PostID = obj.PostID;
            $scope.UserPost.PostName = obj.PostName;
            $scope.UserPost.CompanyID = obj.CompanyID;
            $scope.UserPost.CName = obj.CName;
            $scope.UserPost.DepartmentID = obj.DepartmentID;
            $scope.UserPost.DepartmentName = obj.DepartmentName;
            $scope.UserPost.UserID = obj.UserID;
            $scope.UserPost.UserName = obj.UserName;
            $scope.AddUserPost();
        };

        //搜索、分页列表
        $scope.CurrentPage = 1;
        $scope.PostID = '';
        $scope.UserName = '';
        $scope.Search = function () {
            $http.get('/Api/HRUserPost/Index?CurrentPage=' + $scope.CurrentPage+'&postID='+$scope.PostID+'&userName='+$scope.UserName).success(function (data) {
                $scope.List = data.Data;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                })
            });
        };
        $scope.Search();

        $scope.Remove = function (id) {
            KMConfirm({
                msg: '是否删除当前记录?',
                btnMsg: '删除'
            }, function (e) {
                $http.get('/Api/HRUserPost/Delete?ID=' + id).success(function (data) {
                    KMAlert({
                        msg: "删除成功！"
                    });
                });
                $scope.Search();
            });
        };

        $scope.AddCompany = function () {
            $('#modalCompany').modal('toggle');
        };

        $scope.SaveCompany = function () {
            $http.post('/Api/HRUserPost/SaveCompany', { Data: $scope.Company }).success(function (data) {
                if (data.IsSuccess) {
                    $window.location.reload();
                } else {
                    KMAlert({
                        msg: '保存失败！'
                    });
                }
                $scope.Company = {};
                $('#modalCompany').modal('toggle');
            });
        };

        $scope.AddDepartment = function () {
            $('#modalDepartment').modal('toggle');
        };

        $scope.SelectCompany = function () {
            $('#modalSelectComapny').modal('toggle');
            $scope.CompanySearch();
        };

        //部门信息选择医院搜索、分页列表
        $scope.CompanyCurrentPage = 1;
        $scope.CompanySearch = function () {
            $http.get('/Api/HRUserPost/CompanyList?CurrentPage=' + $scope.CompanyCurrentPage).success(function (data) {
                $scope.Companys = data.Data;
                var pager = new Pager('companyPager', $scope.CompanyCurrentPage, data.PagesCount, function (curPage) {
                    $scope.CompanyCurrentPage = parseInt(curPage);
                    $scope.CompanySearch();
                })
            });
        };
        $scope.CheckCompany = function (obj) {
            $scope.Department = { CName: obj.CName, CompanyID: obj.CompanyID };
            $('#modalSelectComapny').modal('toggle');
        };
        $scope.SaveDepartment = function () {
            $http.post('/Api/HRUserPost/SaveDepartment', { Data: $scope.Department }).success(function (data) {
                if (data.IsSuccess) {
                    $window.location.reload();
                } else {
                    KMAlert({
                        msg: '保存失败！'
                    });
                }
                $scope.Department = {};
                $('#modalDepartment').modal('toggle');
            });
        };

        $scope.AddPost = function () {
            $('#modalPost').modal('toggle');
            
        };
        $scope.SelectDepartment = function () {
            $('#modalSelectDepartment').modal('toggle');
            $scope.DepartmentSearch();
        };

        //岗位信息搜索、分页列表
        $scope.DepartmentCurrentPage = 1;
        $scope.DepartmentSearch = function () {
            $http.get('/Api/HRUserPost/DepartmentList?CurrentPage=' + $scope.DepartmentCurrentPage).success(function (data) {
                $scope.Departments = data.Data;
                var pager = new Pager('departmentPager', $scope.DepartmentCurrentPage, data.PagesCount, function (curPage) {
                    $scope.DepartmentCurrentPage = parseInt(curPage);
                    $scope.DepartmentSearch();
                })
            });
        };
        $scope.CheckDepartment = function (obj) {
            $scope.Post = { CompanyID: obj.CompanyID, DepartmentID: obj.DepartmentID, DepartmentName: obj.CompanyName + '--' + obj.DepartmentName };
            $('#modalSelectDepartment').modal('toggle');
        };

        $scope.SavePost = function () {
            $http.post('/Api/HRUserPost/SavePost', { Data: $scope.Post }).success(function (data) {
                if (data.IsSuccess) {
                    KMAlert({
                        msg: '保存成功！'
                    });
                } else {
                    KMAlert({
                        msg: '保存失败！'
                    });
                }
                $scope.Post = {};
                $('#modalPost').modal('toggle');
            });
        };


        $scope.AddUserPost = function () {
            $('#modalUserPost').modal('toggle');
            console.info($scope.UserPost);
        };
        $scope.SelectPost = function () {
            $('#modalSelectPost').modal('toggle');
            $scope.PostSearch();
        };
        $scope.SelectUser = function () {
            $('#modalSelectUser').modal('toggle');
            $scope.UserSearch();
        };
        //搜索、分页列表
        $scope.PostCurrentPage = 1;
        $scope.PostSearch = function () {
            $http.get('/Api/HRUserPost/PostList?CurrentPage=' + $scope.PostCurrentPage).success(function (data) {
                $scope.Departments = data.Data;
                var pager = new Pager('postPager', $scope.PostCurrentPage, data.PagesCount, function (curPage) {
                    $scope.PostCurrentPage = parseInt(curPage);
                    $scope.PostSearch();
                })
            });
        };
        $scope.CheckPost = function (obj) {
            if ($scope.UserPost == null) $scope.UserPost = {};
            $scope.UserPost.PostID = obj.PostID;
            $scope.UserPost.PostName = obj.PostName;
            $scope.UserPost.CompanyID = obj.CompanyID;
            $scope.UserPost.CName = obj.PostCode;
            $scope.UserPost.DepartmentID = obj.DepartmentID;
            $scope.UserPost.DepartmentName = obj.DepartmentName;
            $('#modalSelectPost').modal('toggle');
        };
        //搜索、分页列表
        $scope.UserCurrentPage = 1;
        $scope.SearchUserName = '';
        $scope.UserSearch = function () {
            $http.get('/Api/HRUserPost/UserList?CurrentPage=' + $scope.UserCurrentPage + '&Name=' + $scope.SearchUserName).success(function (data) {
                $scope.Departments = data.Data;
                var pager = new Pager('userPager', $scope.UserCurrentPage, data.PagesCount, function (curPage) {
                    $scope.UserCurrentPage = parseInt(curPage);
                    $scope.UserSearch();
                })
            });
        };

        $scope.SearchUserCondition = function () {
            //搜索、分页列表
            $scope.UserCurrentPage = 1;
            $scope.UserSearch();
        };
        $scope.CheckUser = function (obj) {
            if ($scope.UserPost == null) $scope.UserPost = {};
            $scope.UserPost.UserID = obj.UserId;
            $scope.UserPost.UserName = obj.UserName;
            $('#modalSelectUser').modal('toggle');
        };
        $scope.SaveUserPost = function () {
            $http.post('/Api/HRUserPost/SaveUserPost', { Data: $scope.UserPost }).success(function (data) {
                if (data.IsSuccess) {
                    KMAlert({
                        msg: '保存成功！'
                    });
                } else {
                    KMAlert({
                        msg: '保存失败！'
                    });
                }
                $scope.Post = {};
                $('#modalUserPost').modal('toggle');
                $scope.Search();
            });
        };
    }
]);

//疾病选择器
app.controller("OrganizationControlCtrl", ["$scope", "$http",
    function ($scope, $http, services) {
        $scope.Checkd = {};
        $http.get('/api/HRUserPost/Get').success(function (data) {
            var treeData = eval(data.Data);
            $scope.initSelectableTree = function () {
                return $('#tree').treeview({
                    data: treeData,
                    levels: 3,
                    selectable: false,
                    showIcon: false,
                    showCheckbox: true,
                    onNodeSelected: function (event, node) {
                        $scope.updateSelection(node);
                    },
                    onNodeChecked: function (event, node) {
                        console.info(event);
                        console.info(node);
                        console.info($scope.Checkd.text == null);
                        if ($scope.Checkd.text != null) {
                            $('#tree').treeview('uncheckNode', [$scope.Checkd.nodeId, { silent: true }]);
                        }
                        $scope.Checkd = node;
                        $scope.updateCheckd($scope.Checkd);
                    },
                    onNodeUnchecked: function (event, node) {
                        $scope.Checkd = {};
                        $scope.updateCheckd($scope.Checkd);
                    }
                });
            };
            $scope.tree = $scope.initSelectableTree();
        });
    }
]);

