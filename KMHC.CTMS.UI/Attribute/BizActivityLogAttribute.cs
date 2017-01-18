/*
 * 描述:操作日志拦截器
 *  
 * 修订历史: 
 * 日期                   修改人              Email                   内容
 * 2015/11/13 9:30:43     邓柏生                                      创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project.UI.Attribute
{
    public class BizActivityLogAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 参数名称列表,可以用, | 分隔
        /// </summary>
        private readonly string _parameterNameList;

        //类型名称
        private string _activityLogTypeName = "";

        /// <summary>
        /// 活动日志
        /// </summary>
        /// <param name="activityLogTypeName">类别名称</param>
        /// <param name="parm">参数名称列表,可以用, | 分隔</param>
        public BizActivityLogAttribute(string activityLogTypeName, string parm)
        {
            _activityLogTypeName = activityLogTypeName;
            _parameterNameList = parm;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var parms = filterContext.ActionParameters;
            if (filterContext != null)
            {
                Dictionary<string, string> parmsObj = new Dictionary<string, string>();

                foreach (var item in _parameterNameList.Split(',', '|'))
                {
                    foreach (var actionParameter in filterContext.ActionParameters)
                    {
                        if (actionParameter.Key == item&&actionParameter.Value!=null)
                        {
                            parmsObj.Add(item, actionParameter.Value.ToString()); 
                        }
                    }
                }
                //日志内容
                StringBuilder logContent = new StringBuilder();

                foreach (KeyValuePair<string, string> kvp in parmsObj)
                {
                    logContent.AppendFormat("{0}:{1} ", kvp.Key, kvp.Value);
                }

                //******************************************************************************
                //这里是插入数据表操作
                //步骤：
                //1、根据日志类型表的SystemKeyword得到日志类型Id
                //2、往日志表里插入数据，logContent.ToString()是内容，内容可以自己拼接字符串，比如：string.Format("删除记录，删除操作者{0}","xxxx");

                //******************************************************************************
            }
        }
    }
}