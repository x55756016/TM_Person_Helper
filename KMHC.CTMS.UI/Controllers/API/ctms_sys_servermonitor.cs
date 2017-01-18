using Project.BLL;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ctms_sys_servermonitor")]
    public class ctms_sys_servermonitorController : ApiController
    {
        private readonly ctms_sys_servermonitorBLL bll = new ctms_sys_servermonitorBLL();
        private readonly ctms_sys_serverinfoBLL bllInfo = new ctms_sys_serverinfoBLL();

        public IHttpActionResult Get(int pageIndex = 1, int pageSize = 10, string kwd = "")
        {
            Response<IEnumerable<V_ctms_sys_servermonitor>> rsp = new Response<IEnumerable<V_ctms_sys_servermonitor>>();
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "ModifyTime",
                    Order = OrderEnum.desc
                };

                List<V_ctms_sys_servermonitor> list = bll.GetList(pageInfo, kwd).ToList();
                foreach (var item in list)
                {
                    var infoList = bllInfo.GetAll(p => p.IPAddress == item.IPAddress).OrderByDescending(p => p.InputTime).ToList();
                    if (infoList.Count <= 0)
                    {
                        item.IsValid = 0;
                    }
                    else
                    {
                        TimeSpan ts = DateTime.Now - infoList.First().InputTime.Value;
                        if (ts.TotalSeconds >= 1000 * 60 * 3)
                        {
                            item.IsValid = 0;
                        }
                    }
                }

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

        public IHttpActionResult Post([FromBody]Request<V_ctms_sys_servermonitor> request)
        {
            Response<bool> rsp = new Response<bool>();

            V_ctms_sys_servermonitor model = request.Data as V_ctms_sys_servermonitor;
            if (string.IsNullOrEmpty(model.ServerMonitorId))
            {
                var temp = bll.Get(p => p.IPAddress == model.IPAddress && p.IsDeleted == 0);
                if (temp != null)
                {
                    rsp.Data = false;
                    rsp.ErrorMsg = "数据重复，IP地址为：" + model.IPAddress;
                    return Ok(rsp);
                }
                rsp.Data = string.IsNullOrEmpty(bll.Add(model)) ? false : true;
            }
            else
            {
                rsp.Data = bll.Edit(model);
            }
            rsp.IsSuccess = true;
            return Ok(rsp);
        }

        public IHttpActionResult Delete(string ID)
        {
            Response<bool> rsp = new Response<bool>();
            rsp.Data = bll.Delete(ID);
            rsp.IsSuccess = true;
            return Ok(rsp);
        }

        [Route("GetDistinctServerList"),HttpGet]
        public IHttpActionResult GetDistinctServerList()
        {
            Response<List<V_ctms_sys_servermonitor>> rsp = new Response<List<V_ctms_sys_servermonitor>>();
            try
            {
                string sql = "select DISTINCT a.IPAddress,a.ServerName from ctms_sys_servermonitor a";
                using (DbContext db = new tmpmEntities2())
                {
                    rsp.Data = db.Database.SqlQuery<V_ctms_sys_servermonitor>(sql).ToList();
                    rsp.IsSuccess = true;
                    return Ok(rsp);
                }
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
