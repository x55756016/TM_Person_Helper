using AutoMapper;
using Project.BLL;
using Project.BLL.Authorization;
using Project.Model;
using Project.Model.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project.UI.Controllers
{
    public class HomeController :Controller
    {
        public ActionResult Index()
        {
            ViewBag.para = "";
            HttpCookie cookie = Request.Cookies["Token"];
            if (cookie != null)
            {
                string tokenValue = cookie.Value;
                if (!string.IsNullOrEmpty(tokenValue))
                {
                    V_ctms_sys_userinfo user = new ctms_sys_userinfoBLL().GetLoginInfo(tokenValue);
                    if (user != null)
                    {
                        ViewBag.LoginName = user.LOGINNAME;
                        ViewBag.MenuInfo = GetMenuHtml();
                        return View();
                    }
                }
            }
            return Redirect("User/Login");
        }

        protected string GetMenuHtml()
        {
            V_ctms_sys_userinfo currentUser = new ctms_sys_userinfoBLL().GetCurrentUser();
            List<MenuInfo> list = new List<MenuInfo>();
            if (currentUser.LOGINNAME.Equals("admin")) //Todo 正式上线时需屏蔽
            {
                list = new MenuInfoBLL().GetList();
            }
            else
            {
                list = new MenuInfoBLL().GetList(currentUser.USERID);
            }
            StringBuilder sb = new StringBuilder("<ul class=\"nav navbar-nav side-nav\">");
            if (list != null && list.Count > 0)
            {
                foreach (MenuInfo menu in list)
                {
                    AppendHtml(ref sb, menu);
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }

        protected void AppendHtml(ref StringBuilder sb, MenuInfo menu)
        {
            /*LangType langtype = LangType.cn;
            if (Session["Lang"] != null)
            {
                langtype = (LangType)Session["Lang"];
            }
            string menuName = LocalizationHelpers.InnerLang(this.ControllerContext.HttpContext, menu.Code);
            if (langtype == LangType.cn && menu.Code.Equals(menuName))
            {
                menuName = menu.Name;
            }*/
            sb.Append("<li>");
            if (menu.ChildrenList == null || menu.ChildrenList.Count == 0)
            {
                sb.Append(string.Format("<a href=\"{0}\"><i class=\"{1}\"></i>{2}</a>", menu.Url, menu.Icon, menu.Name));
            }
            else
            {

                sb.Append(string.Format("<a href=\"{0}\" data-target=\"#{1}\" data-toggle=\"{2}\" aria-expanded=\"{3}\" aria-controls=\"{4}\"><i class=\"{5}\"></i><strong>{6}</strong><i class=\"fa fa-fw fa-caret-down\"></i></a>"
                    , menu.Url, menu.Code, "collapse", menu.IsExpand, menu.Code, menu.Icon, menu.Name));
                sb.Append(string.Format("<ul id=\"{0}\" class=\"collapse {1}\">", menu.Code, menu.IsExpand ? "in" : ""));
                foreach (MenuInfo subMenu in menu.ChildrenList)
                {
                    AppendHtml(ref sb, subMenu);
                }
                sb.Append("</ul>");

            }
            sb.Append("</li>");
        }

        public ActionResult Login()
        {
            return View();
        }

    }
}
