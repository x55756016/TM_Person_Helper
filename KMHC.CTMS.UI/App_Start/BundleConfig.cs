
using System.Web;
using System.Web.Optimization;

namespace Project.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.min.css",
                "~/Content/sb-admin.css"
                , "~/Content/bootstrap-datetimepicker.min.css"
                , "~/Content/font-awesome.min.css"
                , "~/Content/bootstrap-datepicker.min.css"));

            bundles.Add(new ScriptBundle("~/Script/bootstrap").Include("~/Scripts/jquery-2.1.4.min.js"
                , "~/Scripts/bootstrap.js"
                , "~/Scripts/bootstrap-datepicker*"
                , "~/Scripts/bootstrap-datetimepicker*"
                , "~/Scripts/bootstrap-multiselect.js"
                , "~/AppScripts/common/common.js"
                , "~/Scripts/paging.js"
                , "~/Scripts/webuploader.js"
                   , "~/Scripts/bootstrap-typeahead.js"
                , "~/AppScripts/common/pager.js"));

            bundles.Add(new ScriptBundle("~/Script/angular").Include(
                        "~/Scripts/angular.js"
                        , "~/Scripts/angular-route.js"
                        , "~/Scripts/angular-resource.js"
                        , "~/Scripts/angular-animate.js"
                        , "~/AppScripts/common/navi-controllers.js"
                        , "~/AppScripts/common/services.js"
                        , "~/AppScripts/common/utility.js"
                        , "~/AppScripts/common/area.js"
                        , "~/AppScripts/common/AppRoute.js"
                        , "~/AppScripts/Home/dug.js"
                        , "~/AppScripts/ModuleJS/ctms_hr_company.js"
                        , "~/AppScripts/ModuleJS/ctms_hr_department.js"
                        , "~/AppScripts/ModuleJS/ctms_hr_post.js"
                        , "~/AppScripts/ModuleJS/ctms_hr_userpost.js"
                        , "~/AppScripts/ModuleJS/ctms_pm_dotask.js"
                        , "~/AppScripts/ModuleJS/ctms_pm_itemconfirm.js"
                        , "~/AppScripts/ModuleJS/ctms_pm_itemreport.js"
                        , "~/AppScripts/ModuleJS/ctms_pm_project.js"
                        , "~/AppScripts/ModuleJS/ctms_pm_task.js"
                        , "~/AppScripts/ModuleJS/ctms_sys_sysmonitor.js"
                        , "~/AppScripts/ModuleJS/ctms_sys_userinfo.js"
                        , "~/AppScripts/OrganizationManage/Organization.js"
                        , "~/AppScripts/Monitor/monitor.js"
                        , "~/Scripts/Chart.min.js"
                        , "~/AppScripts/DictManage/DictionaryManage.js"
                        , "~/AppScripts/Authorization/Role.js"
                        , "~/AppScripts/Authorization/User.js"
                        , "~/AppScripts/Authorization/Function.js"
                        , "~/AppScripts/common/area.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                        ));
        }
    }
}