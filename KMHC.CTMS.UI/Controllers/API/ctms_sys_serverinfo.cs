using Project.BLL;
using Project.Common;
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
    [System.Web.Http.RoutePrefix("api/ctms_sys_serverinfo")]
    public class ctms_sys_serverinfoController : ApiController
    {
        private readonly ctms_sys_serverinfoBLL bll = new ctms_sys_serverinfoBLL();
        private readonly ctms_sys_servermonitorBLL smBll = new ctms_sys_servermonitorBLL();
        private readonly ctms_sys_serveralarmBLL saBll = new ctms_sys_serveralarmBLL();

        public IHttpActionResult Get(int pageIndex = 1, int pageSize = 10, string kwd = "")
        {
            Response<IEnumerable<V_ctms_sys_serverinfo>> rsp = new Response<IEnumerable<V_ctms_sys_serverinfo>>();
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "InputTime",
                    Order = OrderEnum.desc
                };

                List<V_ctms_sys_serverinfo> list = bll.GetList(pageInfo, kwd).ToList();

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

        public IHttpActionResult Post([FromBody]Request<V_ctms_sys_serverinfo> request)
        {
            Response<bool> rsp = new Response<bool>();

            V_ctms_sys_serverinfo model = request.Data as V_ctms_sys_serverinfo;
            if (string.IsNullOrEmpty(model.ServerInfoId))
            {
                rsp.Data = string.IsNullOrEmpty(bll.Add(model)) ? false : true;
                if (rsp.Data)
                {
                    var sm = smBll.Get(p => p.IPAddress == model.IPAddress);
                    if (sm != null)
                    {
                        if (model.CPUValue >= sm.CPUMaxValue)
                        {
                            bll.CheckServerInfoValue(sm, "CPU");
                        }
                        if (model.MemoryValue >= sm.MemoryMaxValue)
                        {
                            bll.CheckServerInfoValue(sm, "内存");
                        }
                        if (model.DiskValue >= sm.DiskMaxValue)
                        {
                            bll.CheckServerInfoValue(sm, "硬盘");
                        }
                    }
                }
            }
            else
            {
                rsp.Data = bll.Edit(model);
            }
            rsp.IsSuccess = true;
            return Ok(rsp);
        }

        [Route("GetServerInfoCount"),HttpGet]
        public IHttpActionResult GetServerInfoCount(string ip = "",DateTime? dtStart = null,DateTime? dtEnd = null)
        {
            Response<List<C_ctms_sys_serverinfo>> rsp = new Response<List<C_ctms_sys_serverinfo>>();
            try
            {
                string sql = string.Format(@"select DATE_FORMAT(a.InputTime,'%H:%i') as InputTime,a.CPUValue,a.MemoryValue,a.DiskValue from ctms_sys_serverinfo a where 1=1 {0} {1} {2} order by a.InputTime;", string.IsNullOrEmpty(ip) ? string.Empty : string.Format("and a.IPAddress='{0}'", ip), dtStart == null ? "" : string.Format("and a.InputTime>='{0}'", dtStart), dtEnd == null ? "" : string.Format("and a.InputTime<='{0}'", dtEnd));
                using (DbContext db = new tmpmEntities2())
                {
                    rsp.Data = db.Database.SqlQuery<C_ctms_sys_serverinfo>(sql).ToList();
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
