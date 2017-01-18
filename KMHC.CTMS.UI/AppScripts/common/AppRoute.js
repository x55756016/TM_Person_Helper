
angular.module('userApp', [
        'ngRoute',
        'ngResource',
        'HR.services',
        'Utility',
        'navigation.controllers'
        , 'ctms_hr_company'
        , 'ctms_hr_department'
        , 'ctms_hr_post'
        , 'ctms_hr_userpost'
        , 'ctms_pm_dotask'
        , 'ctms_pm_itemconfirm'
        , 'ctms_pm_itemreport'
        , 'ctms_pm_project'
        , 'ctms_pm_task'
        , 'ctms_sys_sysmonitor'
        , 'ctms_sys_userinfo'
        , 'OrganizationManage'
        , 'Monitor'
        , 'DictionaryManage'
        , 'Role'
        , 'UserManageCtrl'
        , 'SysFunction'
        , 'common'
]).
    config([
        '$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $routeProvider.when('/', {});
            $routeProvider.when('/ctms_hr_company', { templateUrl: '/Views/List/ctms_hr_company.html', controller: 'ctms_hr_companyCtrl' });
            $routeProvider.when('/Addctms_hr_company', { templateUrl: '/Views/Info/ctms_hr_company.html', controller: 'Addctms_hr_companyCtrl' });
            $routeProvider.when('/Editctms_hr_company', { templateUrl: '/Views/Info/ctms_hr_company.html', controller: 'Addctms_hr_companyCtrl' });
            $routeProvider.when('/ctms_hr_department', { templateUrl: '/Views/List/ctms_hr_department.html', controller: 'ctms_hr_departmentCtrl' });
            $routeProvider.when('/Addctms_hr_department', { templateUrl: '/Views/Info/ctms_hr_department.html', controller: 'Addctms_hr_departmentCtrl' });
            $routeProvider.when('/Editctms_hr_department', { templateUrl: '/Views/Info/ctms_hr_department.html', controller: 'Addctms_hr_departmentCtrl' });
            $routeProvider.when('/ctms_hr_post', { templateUrl: '/Views/List/ctms_hr_post.html', controller: 'ctms_hr_postCtrl' });
            $routeProvider.when('/Addctms_hr_post', { templateUrl: '/Views/Info/ctms_hr_post.html', controller: 'Addctms_hr_postCtrl' });
            $routeProvider.when('/Editctms_hr_post', { templateUrl: '/Views/Info/ctms_hr_post.html', controller: 'Addctms_hr_postCtrl' });
            $routeProvider.when('/ctms_hr_userpost', { templateUrl: '/Views/List/ctms_hr_userpost.html', controller: 'ctms_hr_userpostCtrl' });
            $routeProvider.when('/Addctms_hr_userpost', { templateUrl: '/Views/Info/ctms_hr_userpost.html', controller: 'Addctms_hr_userpostCtrl' });
            $routeProvider.when('/Editctms_hr_userpost', { templateUrl: '/Views/Info/ctms_hr_userpost.html', controller: 'Addctms_hr_userpostCtrl' });
            $routeProvider.when('/ctms_pm_dotask', { templateUrl: '/Views/List/ctms_pm_dotask.html', controller: 'ctms_pm_dotaskCtrl' });
            $routeProvider.when('/Addctms_pm_dotask', { templateUrl: '/Views/Info/ctms_pm_dotask.html', controller: 'Addctms_pm_dotaskCtrl' });
            $routeProvider.when('/Editctms_pm_dotask', { templateUrl: '/Views/Info/ctms_pm_dotask.html', controller: 'Addctms_pm_dotaskCtrl' });
            $routeProvider.when('/ctms_pm_itemconfirm', { templateUrl: '/Views/List/ctms_pm_itemconfirm.html', controller: 'ctms_pm_itemconfirmCtrl' });
            $routeProvider.when('/Addctms_pm_itemconfirm', { templateUrl: '/Views/Info/ctms_pm_itemconfirm.html', controller: 'Addctms_pm_itemconfirmCtrl' });
            $routeProvider.when('/Editctms_pm_itemconfirm', { templateUrl: '/Views/Info/ctms_pm_itemconfirm.html', controller: 'Addctms_pm_itemconfirmCtrl' });
            $routeProvider.when('/ctms_pm_itemreport', { templateUrl: '/Views/List/ctms_pm_itemreport.html', controller: 'ctms_pm_itemreportCtrl' });
            $routeProvider.when('/Addctms_pm_itemreport', { templateUrl: '/Views/Info/ctms_pm_itemreport.html', controller: 'Addctms_pm_itemreportCtrl' });
            $routeProvider.when('/Editctms_pm_itemreport', { templateUrl: '/Views/Info/ctms_pm_itemreport.html', controller: 'Addctms_pm_itemreportCtrl' });
            $routeProvider.when('/ctms_pm_project', { templateUrl: '/Views/List/ctms_pm_project.html', controller: 'ctms_pm_projectCtrl' });
            $routeProvider.when('/Addctms_pm_project', { templateUrl: '/Views/Info/ctms_pm_project.html', controller: 'Addctms_pm_projectCtrl' });
            $routeProvider.when('/Editctms_pm_project', { templateUrl: '/Views/Info/ctms_pm_project.html', controller: 'Addctms_pm_projectCtrl' });
            $routeProvider.when('/ctms_pm_task', { templateUrl: '/Views/List/ctms_pm_task.html', controller: 'ctms_pm_taskCtrl' });
            $routeProvider.when('/Addctms_pm_task', { templateUrl: '/Views/Info/ctms_pm_task.html', controller: 'Addctms_pm_taskCtrl' });
            $routeProvider.when('/Editctms_pm_task', { templateUrl: '/Views/Info/ctms_pm_task.html', controller: 'Addctms_pm_taskCtrl' });
            $routeProvider.when('/ctms_sys_sysmonitor', { templateUrl: '/Views/List/ctms_sys_sysmonitor.html', controller: 'ctms_sys_sysmonitorCtrl' });
            $routeProvider.when('/Addctms_sys_sysmonitor', { templateUrl: '/Views/Info/ctms_sys_sysmonitor.html', controller: 'Addctms_sys_sysmonitorCtrl' });
            $routeProvider.when('/Editctms_sys_sysmonitor', { templateUrl: '/Views/Info/ctms_sys_sysmonitor.html', controller: 'Addctms_sys_sysmonitorCtrl' });
            $routeProvider.when('/ctms_sys_userinfo', { templateUrl: '/Views/List/ctms_sys_userinfo.html', controller: 'ctms_sys_userinfoCtrl' });
            $routeProvider.when('/Addctms_sys_userinfo', { templateUrl: '/Views/Info/ctms_sys_userinfo.html', controller: 'Addctms_sys_userinfoCtrl' });
            $routeProvider.when('/Editctms_sys_userinfo', { templateUrl: '/Views/Info/ctms_sys_userinfo.html', controller: 'Addctms_sys_userinfoCtrl' });

            //组织架构
            $routeProvider.when('/OrganizationManage', { templateUrl: '/Views/OrganizationManage/Index.html', controller: 'OrganizationManageCtrl' });
            $routeProvider.when('/OrganizationControl', { templateUrl: '/Views/Common/OrganizationManage.html', controller: 'OrganizationControlCtrl' });

            //字典管理
            $routeProvider.when('/DictionaryManage', { templateUrl: '/Views/DictionaryManage/Index.html', controller: 'DictManageCtrl' });
            $routeProvider.when('/AddDictionary', { templateUrl: '/Views/DictionaryManage/Info.html', controller: 'AddDictCtrl' });
            $routeProvider.when('/EditDictionary', { templateUrl: '/Views/DictionaryManage/Info.html', controller: 'EditDictCtrl' });

            //系统监控模块
            //用户统计
            $routeProvider.when('/UserStatistics', { templateUrl: '/Views/Monitor/userstatistics.html', controller: 'UserStatisticsCtrl' });
            //编码设置
            $routeProvider.when('/ModelSetting', { templateUrl: '/Views/Monitor/modelsetting.html', controller: 'ModelSettingCtrl' });
            //用户活跃度统计
            $routeProvider.when('/ActiveStatistics', { templateUrl: '/Views/Monitor/activestatistics.html', controller: 'ActiveStatisticsCtrl' });
            //操作失败率监控统计
            $routeProvider.when('/ErrorCount', { templateUrl: '/Views/Monitor/errorcount.html', controller: 'ErrorCountCtrl' });
            //服务器监控配置
            $routeProvider.when('/ServerMonitorSetting', { templateUrl: '/Views/Monitor/servermonitorsetting.html', controller: 'ServerMonitorSettingCtrl' });
            //服务器监控
            $routeProvider.when('/ServerMonitor', { templateUrl: '/Views/Monitor/servermonitor.html', controller: 'ServerMonitorCtrl' });
            //服务器告警列表
            $routeProvider.when('/ServerAlarmList', { templateUrl: '/Views/Monitor/serveralarmlist.html', controller: 'ServerAlarmListCtrl' });


            //权限管理
            $routeProvider.when('/Role', { templateUrl: '/Views/Authorization/Role.html', controller: 'RoleCtrl' });
            $routeProvider.when('/AddRole', { templateUrl: '/Views/Authorization/RoleInfo.html', controller: 'AddRoleCtrl' });
            $routeProvider.when('/RoleView', { templateUrl: '/Views/Authorization/RoleView.html', controller: 'RoleViewCtrl' });
            $routeProvider.when('/EditRole', { templateUrl: '/Views/Authorization/RoleInfo.html', controller: 'EditRoleCtrl' });
            $routeProvider.when('/UserManage', { templateUrl: '/Views/Authorization/User.html', controller: 'UserManageCtrl' });
            $routeProvider.when('/Permission', { templateUrl: '/Views/Authorization/Permission.html', controller: 'PermissionCtrl' });

            //系统菜单管理
            $routeProvider.when('/SysFunction', { templateUrl: '/Views/Authorization/Function.html', controller: 'SysFunctionCtrl' });
            $routeProvider.when('/AddSysFunction', { templateUrl: '/Views/Authorization/FunctionInfo.html', controller: 'AddSysFunctionCtrl' });
            $routeProvider.when('/EditSysFunction', { templateUrl: '/Views/Authorization/FunctionInfo.html', controller: 'EditSysFunctionCtrl' });

            $routeProvider.otherwise({ redirectTo: '/' });
        }
    ]).controller('MyController', function ($scope, $http) {
        $scope.logout = function () {
            $http({
                method: 'POST',
                url: '/User/UserLogout'
            }).success(function (data) {
                alert(data.Msg);
                window.location = "/User/Login#/Login";
            }).error(function (data) {
                alert(data);
            });
        };
    });

