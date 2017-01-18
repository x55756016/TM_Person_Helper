/*
 * 描述:
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2016-11-16                            创建 
 *  
 */

using Project.BLL;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ctms_sys_sysmonitor")]
    public class ctms_sys_sysmonitorController : ApiController
    {
        private ctms_sys_sysmonitorBLL bll = new ctms_sys_sysmonitorBLL();

        public IHttpActionResult Get(int CurrentPage,string kwd = "",DateTime? dtStart = null,DateTime? dtEnd = null)
        {
            //申明参数
            int _pageSize = 10;

            Response<IEnumerable<V_ctms_sys_sysmonitor>> rsp = new Response<IEnumerable<V_ctms_sys_sysmonitor>>();

            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "OPERATETIME",
                    Order = OrderEnum.desc
                };
                var list = bll.GetList(pageInfo,kwd,dtStart,dtEnd);
                rsp.Data = list;
                rsp.PagesCount = pageInfo.PagesCount;
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                rsp.Data = null;
                rsp.ErrorMsg = ex.Message;
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
        }

        public IHttpActionResult Get(string ID)
        {
            V_ctms_sys_sysmonitor model = bll.Get(p=>p.OPERATEID==ID);
            return Ok(model);
        }

        public IHttpActionResult Post([FromBody]Request<V_ctms_sys_sysmonitor> request)
        {
            V_ctms_sys_sysmonitor model = request.Data as V_ctms_sys_sysmonitor;
            if (string.IsNullOrEmpty(model.OPERATEID))
            {
                bll.Add(model);
            }
            else
            {
                bll.Edit(model);
            }

            return Ok("ok");
        }

        public IHttpActionResult Delete(string ID)
        {
            bll.Delete(ID);

            return Ok("ok");
        }

        [Route("GetActiveCount"),HttpGet]
        public IHttpActionResult GetActiveCount(DateTime dtStart, DateTime dtEnd)
        {
            Response<List<ActiveCountDataModel>> rsp = new Response<List<ActiveCountDataModel>>();
            try
            {
                string sql = string.Format(@"SELECT c.datelist,d.ModelName,d.count from calendar c
LEFT JOIN(
SELECT DATE_FORMAT(b.OPERATETIME,'%Y-%m-%d') days,a.ModelName,COUNT(a.ModelName) count FROM ctms_sys_modelsetting a
LEFT JOIN ctms_sys_sysmonitor b ON a.ModelCode=b.MODELCODE
WHERE a.IsDeleted=0 AND a.IsValid=1 AND a.ModelSource=0 AND b.OPERATETIME BETWEEN '{0}' AND '{1}'
GROUP BY days,a.ModelName
) d ON c.datelist=d.days
WHERE c.datelist BETWEEN '{0}' AND '{1}'
ORDER BY c.datelist",dtStart,dtEnd);
                using (DbContext db = new tmpmEntities2())
                {
                    rsp.Data = db.Database.SqlQuery<ActiveCountDataModel>(sql).ToList();
                    rsp.IsSuccess = true;
                    return Ok(rsp);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.Data = null;
                rsp.IsSuccess = true;
                rsp.ErrorMsg = ex.Message;
                return Ok(rsp);
            }
        }
        
    }

    public class ActiveCountDataModel
    {
        public string datelist { get; set; }
        public string ModelName { get; set; }
        public int? count { get; set; }
    }
}
