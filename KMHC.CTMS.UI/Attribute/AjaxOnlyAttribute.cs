/*
 * 描述: 指定类或方法只能接受ajax类型请求
 *  
 * 修订历史: 
 *    日期            修改人              Email                  内容
 *  20151113  		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System.Web.Mvc;

namespace Project.UI.Attribute
{
    public class AjaxOnlyAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpNotFoundResult();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}