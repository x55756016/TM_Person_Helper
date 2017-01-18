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
    [System.Web.Http.RoutePrefix("api/ctms_sys_userregrecord")]
    public class ctms_sys_userregrecordController : ApiController
    {
        private readonly ctms_sys_userregrecordBLL bll = new ctms_sys_userregrecordBLL();

        public IHttpActionResult Get(string ID)
        {
            Response<V_ctms_sys_userregrecord> rsp = new Response<V_ctms_sys_userregrecord>();
            V_ctms_sys_userregrecord model = bll.Get(p => p.UserId == ID);
            rsp.IsSuccess = true;
            rsp.Data = model;
            return Ok(rsp);
        }

        public IHttpActionResult Post([FromBody]Request<V_ctms_sys_userregrecord> request)
        {
            Response<bool> rsp = new Response<bool>();
            try
            {
                V_ctms_sys_userregrecord model = request.Data as V_ctms_sys_userregrecord;
                if (string.IsNullOrEmpty(model.UserRegRecordId))
                {
                    string result = bll.Add(model);
                    rsp.Data = string.IsNullOrEmpty(result) ? false : true;
                }
                else
                {
                    rsp.Data = bll.Edit(model);
                }
                rsp.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.IsSuccess = true;
                rsp.Data = true;
                rsp.ErrorMsg = "服务器异常";
            }

            return Ok(rsp);
        }

        [Route("GetUserStatistics"), HttpGet]
        public IHttpActionResult Get(DateTime? dtFrom = null, DateTime? dtTo = null, int pageIndex = 1, int pageSize = 10)
        {
            Response<List<V_ctms_sys_userregrecord>> rsp = new Response<List<V_ctms_sys_userregrecord>>();
            try
            {
                if (dtFrom == null)
                    dtFrom = DateTime.Today.AddDays(-1);
                if (dtTo == null)
                    dtTo = DateTime.Today;
                //string sql = string.Format(@"select UserId,UserName,CASE Sex WHEN '1' THEN n'男' WHEN '0' THEN n'女' ELSE n'未知' END AS Sex,Age,LoginName,MobilePhone,Address,InputTime,(CASE UserSource WHEN '0' THEN 'WEB' WHEN '1' THEN 'IOS' WHEN '2' THEN 'ANDROID' WHEN '3' THEN '微信' END) AS UserSource from ctms_sys_userregrecord a WHERE InputTime>='{0}' AND InputTime<'{1}';", dtFrom, dtTo);
                string sql = string.Format(@"select UserId,UserName,Sex,Age,LoginName,MobilePhone,Address,InputTime,(CASE UserSource WHEN '0' THEN 'WEB' WHEN '1' THEN 'IOS' WHEN '2' THEN 'ANDROID' WHEN '3' THEN '微信' END) AS UserSource from ctms_sys_userregrecord a WHERE InputTime>='{0}' AND InputTime<'{1}';", dtFrom, dtTo);

                using (DbContext db = new tmpmEntities2())
                {
                    List<V_ctms_sys_userregrecord> list = db.Database.SqlQuery<V_ctms_sys_userregrecord>(sql).ToList();
                    rsp.Data = list;
                    rsp.IsSuccess = true;
                    rsp.PagesCount = 0;
                    if (list.Count > 0)
                    {
                        rsp.Data = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                        rsp.PagesCount = list.Count % pageSize == 0 ? list.Count / pageSize : list.Count / pageSize + 1;
                    }
                    return Ok(rsp);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.Data = null;
                rsp.IsSuccess = true;
                rsp.ErrorMsg = "服务器异常";
                rsp.PagesCount = 0;
                return Ok(rsp);
            }
        }

        [Route("GetUserStatisticsCount"), HttpGet]
        public IHttpActionResult Get(DateTime? dtStart = null, DateTime? dtEnd = null, string type = "DD")
        {
            Response<ActiveCountModel> rsp = new Response<ActiveCountModel>();
            try
            {
                #region 初始化两个时间变量
                if (dtStart == null && dtEnd == null)
                {
                    dtEnd = DateTime.Now;
                    dtStart = DateTime.Now.AddMonths(-1);
                }
                if (dtStart == null && dtEnd != null)
                {
                    dtStart = dtEnd.Value.AddMonths(-1);
                    if (dtEnd > DateTime.Now)
                    {
                        dtEnd = DateTime.Now;
                    }
                }
                else if (dtStart != null && dtEnd == null)
                {
                    if (dtStart >= DateTime.Now)
                    {
                        rsp.Data = null;
                        rsp.IsSuccess = true;
                        rsp.ErrorMsg = "起始日期不能大于当前日期";
                        return Ok(rsp);
                    }
                    dtEnd = dtStart.Value.AddMonths(1);
                    if (dtEnd > DateTime.Now)
                    {
                        dtEnd = DateTime.Now;
                    }
                }
                dtStart = new DateTime(dtStart.Value.Year, dtStart.Value.Month, dtStart.Value.Day, 0, 0, 0);
                dtEnd = new DateTime(dtEnd.Value.Year, dtEnd.Value.Month, dtEnd.Value.Day, 23, 59, 59);
                #endregion

                string strType = "", tempType = "%Y%m%d";
                switch (type)
                {
                    case "DD":
                        strType = "";
                        tempType = "%Y%m%d";
                        break;
                    case "iw":
                        strType = "周";
                        tempType = "%Y%u";
                        break;
                    case "mm":
                        strType = "月";
                        tempType = "%Y%m";
                        break;
                    case "yyyy":
                        strType = "年";
                        tempType = "%Y";
                        break;
                    default:
                        strType = "";
                        tempType = "%Y%m%d";
                        break;
                }

                string sql = string.Format(@"select DATE_FORMAT(a.InputTime,'{2}') temptime,MIN(a.InputTime) RegDate,COUNT(1) as RegCount from ctms_sys_userregrecord a WHERE InputTime>='{0}' AND InputTime<'{1}' GROUP BY temptime;", dtStart, dtEnd, tempType);
                
                string title = "用户注册趋势图";
                using (DbContext db = new tmpmEntities2())
                {
                    List<UserRegCountDataModel> list = db.Database.SqlQuery<UserRegCountDataModel>(sql).ToList();
                    if (list.Count > 0)
                    {
                        if (type != "DD" && !string.IsNullOrEmpty(type))
                        {
                            title = list[0].RegDate.ToString("yyyy年MM月dd日") + "至" + list[list.Count - 1].RegDate.ToString("yyyy年MM月dd日") + "用户注册" + strType + "趋势图";
                        }
                        ActiveCountModel model = new ActiveCountModel();
                        model.title = title;
                        model.legendData = new List<string>() { "注册人数" };
                        model.xAxisData = list.Select(p => type == "iw" ? p.RegDate.ToString("yy.MM.dd") + "-" + p.RegDate.AddDays(7).ToString("yy.MM.dd") : type == "mm" ? p.RegDate.ToString("yy.MM.dd") + "-" + p.RegDate.AddMonths(1).ToString("yy.MM.dd") : type == "yyyy" ? p.RegDate.ToString("yy.MM.dd") + "-" + p.RegDate.AddYears(1).ToString("yy.MM.dd") : p.RegDate.ToString("yyyy年MM月dd日")).ToList();
                        SeriesDataModel series = new SeriesDataModel()
                        {
                            name = "注册人数",
                            type = "line",
                            barWidth = 50,
                            data = list.Select(p => (double)p.RegCount).ToList()
                        };
                        model.seriesData = new List<SeriesDataModel>() { series };
                        rsp.Data = model;
                        rsp.IsSuccess = true;
                        return Ok(rsp);
                    }
                    else
                    {
                        rsp.Data = null;
                        rsp.IsSuccess = true;
                        rsp.ErrorMsg = "查询不到数据";
                        return Ok(rsp);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError("获取新用户注册统计信息失败，错误信息为：" + ex + ex.InnerException);
                rsp.Data = null;
                rsp.IsSuccess = true;
                rsp.ErrorMsg = "获取新用户注册统计信息失败";
                return Ok(rsp);
            }
        }
    }
}
