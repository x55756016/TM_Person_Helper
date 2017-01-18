
(function () {
    "use strict";
    var app = angular.module('HR.services', []);

    var services = ['users'
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
                    , 'DictionaryManage'
                    , 'Role'
                    , 'Dictionary'
                    , 'UserManage'
                    , 'UserType'
                    , 'SysFunction'
                    , 'MetaDataPicker'
    ];// the services without customized actions
    function addService(name, actions) {
        app.factory(name + 'Services', ['$resource', function ($resource) {
            return $resource('/api/' + name + '/:id', null, actions);
        }]);
    }
    angular.forEach(services, function (name) {
        addService(name, null);
    });
    //addService('login_register', { login: { method: 'POST', url: '/api/authenticate' } });
})();