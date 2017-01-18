using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Project.UI.Attribute
{
    public class ExceptionAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 获取程序运行错误，返回500状态码以及信息
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { Success = false, Exception = filterContext.Exception.Message.ToString() }
                };
                //Todo 插入数据库
                filterContext.ExceptionHandled = true;
                filterContext.RequestContext.HttpContext.Response.StatusCode = 500;
                filterContext.RequestContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                base.OnException(filterContext);
            }
        }
    }
}