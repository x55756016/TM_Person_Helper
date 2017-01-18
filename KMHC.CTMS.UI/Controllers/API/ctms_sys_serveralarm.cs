using Project.BLL;
using Project.Common.Helper;
using Project.Model;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ctms_sys_serveralarm")]
    public class ctms_sys_serveralarmController : ApiController
    {
        private readonly ctms_sys_serveralarmBLL bll = new ctms_sys_serveralarmBLL();

        public IHttpActionResult Get(int pageIndex = 1, int pageSize = 10, string kwd = "", int? type = null)
        {
            Response<List<V_ctms_sys_serveralarm>> rsp = new Response<List<V_ctms_sys_serveralarm>>();
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "InputTime",
                    Order = OrderEnum.desc
                };

                List<V_ctms_sys_serveralarm> list = bll.GetList(pageInfo, kwd, type);

                rsp.Data = list;
                rsp.PagesCount = pageInfo.PagesCount;
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.IsSuccess = true;
                rsp.Data = null;
                rsp.ErrorMsg = "服务器异常";
                return Ok(rsp);
            }
        }
    }
}
