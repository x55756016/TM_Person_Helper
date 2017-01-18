using Newtonsoft.Json;
using Project.BLL;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Project.UI.Controllers.API
{
    [RoutePrefix("api/monitor")]
    public class MonitorController : ApiController
    {
        private V_ctms_sys_userinfo curUser = new ctms_sys_userinfoBLL().GetCurrentUser();
        private readonly ctms_sys_modelsettingBLL msBll = new ctms_sys_modelsettingBLL();
        private readonly ctms_sys_sysmonitorBLL monitorBll = new ctms_sys_sysmonitorBLL();
        private readonly ctms_sys_errorcountBLL errorBll = new ctms_sys_errorcountBLL();
        private readonly ctms_sys_servermonitorBLL smonitorBll = new ctms_sys_servermonitorBLL();
        private readonly ctms_sys_serverinfoBLL sinfoBll = new ctms_sys_serverinfoBLL();
        private readonly ctms_sys_serveralarmBLL saBll = new ctms_sys_serveralarmBLL();

        [Route("GetModelSettingList"), HttpGet]
        public IHttpActionResult GetModelSettingList(int pageIndex, int pageSize = 10, string kwd = "", int type = 0)
        {
            Response<List<V_ctms_sys_modelsetting>> rsp = new Response<List<V_ctms_sys_modelsetting>>();
            try
            {
                WebClient client = new WebClient();
                //string data = client.DownloadString(ConfigurationManager.AppSettings["MONITOR_URL"] + "api/ctms_sys_modelsetting?CurrentPage=" + pageIndex + "&PageSize=" + pageSize + "&kwd=" + kwd);
                //byte[] data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_modelsetting?CurrentPage=" + pageIndex + "&PageSize=" + pageSize + "&kwd=" + kwd + "&type=" + type);
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "LastModifyDateTime",
                    Order = OrderEnum.desc
                };

                List<V_ctms_sys_modelsetting> list = msBll.GetList(pageInfo, type, kwd).ToList();

                rsp.Data = list;
                rsp.PagesCount = pageInfo.PagesCount;
                rsp.IsSuccess = true;
                return Ok(rsp);
                //rsp = data.JsonDeserialize<Response<List<V_ctms_sys_modelsetting>>>();
                //string result = System.Text.Encoding.UTF8.GetString(data);
                //rsp = JsonConvert.DeserializeObject<Response<List<V_ctms_sys_modelsetting>>>(result);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.Data = null;
                rsp.IsSuccess = true;
                rsp.ErrorMsg = "服务器异常";
                return Ok(rsp);
            }
        }

        [Route("SaveModelSetting"), HttpPost]
        public IHttpActionResult SaveModelSetting([FromBody]Request<V_ctms_sys_modelsetting> request)
        {
            Response<bool> rsp = new Response<bool>();
            try
            {
                /*V_ctms_sys_modelsetting model = request.Data as V_ctms_sys_modelsetting;
                if (model == null)
                {
                    rsp.Data = false;
                    rsp.IsSuccess = true;
                    rsp.ErrorMsg = "参数错误";
                    return Ok(rsp);
                }
                if (string.IsNullOrEmpty(model.ModelSettingId))
                {
                    model.CreateUserAccount = curUser.LOGINNAME;
                    model.CreateDateTime = DateTime.Now;
                    model.IsValid = 1;
                    model.IsDeleted = 0;
                }
                model.LastModifyUserAccount = curUser.LOGINNAME;
                model.LastModifyDateTime = DateTime.Now;
                WebClient client = new WebClient();
                byte[] b = client.UploadValues(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_modelsetting", model.GetParameters());
                rsp = System.Text.Encoding.Default.GetString(b).JsonDeserialize<Response<bool>>();
                return Ok(rsp);*/


                V_ctms_sys_modelsetting model = request.Data as V_ctms_sys_modelsetting;
                if (string.IsNullOrEmpty(model.ModelSettingId))
                {
                    model.CreateUserAccount = curUser.LOGINNAME;
                    model.CreateDateTime = DateTime.Now;
                    model.IsValid = 1;
                    model.IsDeleted = 0;
                }
                model.LastModifyUserAccount = curUser.LOGINNAME;
                model.LastModifyDateTime = DateTime.Now;

                if (string.IsNullOrEmpty(model.ModelSettingId))
                {
                    rsp.Data = string.IsNullOrEmpty(msBll.Add(model)) ? false : true;
                }
                else
                {
                    rsp.Data = msBll.Edit(model);
                }
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.Data = false;
                rsp.IsSuccess = true;
                rsp.ErrorMsg = "服务器异常";
                return Ok(rsp);
            }
        }

        [Route("GetActiveCount"), HttpGet]
        public IHttpActionResult GetActiveCount(DateTime? dtStart = null, DateTime? dtEnd = null)
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

                ActiveCountModel model = new ActiveCountModel();
                model.title = "康爱365用户活跃度统计";

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = 1,
                    PageSize = 100,
                    OrderField = "LastModifyDateTime",
                    Order = OrderEnum.desc
                };

                List<V_ctms_sys_modelsetting> modelSettingRSPList = msBll.GetList(pageInfo, 0).ToList();
                if (modelSettingRSPList.Count > 0)
                {
                    model.legendData = modelSettingRSPList.Select(p => p.ModelName).ToList();
                }

                /*Response<List<V_ctms_sys_modelsetting>> modelSettingRSP = new Response<List<V_ctms_sys_modelsetting>>();
                WebClient client = new WebClient();
                byte[] data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_modelsetting?CurrentPage=" + 1 + "&PageSize=" + 100 + "&type=" + 0);
                string result = System.Text.Encoding.UTF8.GetString(data);
                modelSettingRSP = JsonConvert.DeserializeObject<Response<List<V_ctms_sys_modelsetting>>>(result);
                if (modelSettingRSP.Data != null)
                {
                    model.legendData = modelSettingRSP.Data.Select(p => p.ModelName).ToList();
                }*/

                string sql = string.Format(@"SELECT c.datelist,d.ModelName,d.count from calendar c
LEFT JOIN(
SELECT DATE_FORMAT(b.OPERATETIME,'%Y-%m-%d') days,a.ModelName,COUNT(a.ModelName) count FROM ctms_sys_modelsetting a
LEFT JOIN ctms_sys_sysmonitor b ON a.ModelCode=b.MODELCODE
WHERE a.IsDeleted=0 AND a.IsValid=1 AND a.ModelSource=0 AND b.OPERATETIME BETWEEN '{0}' AND '{1}'
GROUP BY days,a.ModelName
) d ON c.datelist=d.days
WHERE c.datelist BETWEEN '{0}' AND '{1}'
ORDER BY c.datelist", dtStart, dtEnd);
                using (DbContext db = new tmpmEntities2())
                {
                    List < ActiveCountDataModel> acdList = db.Database.SqlQuery<ActiveCountDataModel>(sql).ToList();
                    if (acdList.Count > 0)
                    {
                        foreach (var item in acdList)
                        {
                            DateTime dtTemp = DateTime.Parse(item.datelist);
                            item.datelist = dtTemp.ToString("yyyy年MM月dd日");
                        }
                        model.xAxisData = acdList.Select(p => p.datelist).Distinct().ToList();
                        model.seriesData = new List<SeriesDataModel>();
                        foreach (string item in model.legendData)
                        {
                            SeriesDataModel series = new SeriesDataModel();
                            series.name = item;
                            series.type = "bar";
                            series.stack = model.legendData[0];
                            series.barWidth = 50;
                            series.data = new List<double>();

                            for (int i = 0; i < model.xAxisData.Count; i++)
                            {
                                ActiveCountDataModel temp = acdList.FirstOrDefault(p => p.datelist == model.xAxisData[i] && p.ModelName == item);
                                if (temp != null)
                                    series.data.Add(temp.count.Value);
                                else
                                    series.data.Add(0);
                            }
                            model.seriesData.Add(series);
                        }
                    }
                }

                /*data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_sysmonitor/GetActiveCount?dtStart=" + dtStart + "&dtEnd=" + dtEnd);
                Response<List<ActiveCountDataModel>> activeCountRSP = new Response<List<ActiveCountDataModel>>();
                result = System.Text.Encoding.UTF8.GetString(data);
                activeCountRSP = JsonConvert.DeserializeObject<Response<List<ActiveCountDataModel>>>(result);*/
                
                rsp.IsSuccess = true;
                rsp.Data = model;
                return Ok(rsp);
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

        [Route("GetActiveDetails"), HttpGet]
        public IHttpActionResult GetActiveDetails(int pageIndex = 1, DateTime? dtStart = null, DateTime? dtEnd = null, string kwd = "")
        {
            Response<List<V_ctms_sys_sysmonitor>> rsp = new Response<List<V_ctms_sys_sysmonitor>>();
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

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = 10,
                    OrderField = "OPERATETIME",
                    Order = OrderEnum.desc
                };
                var list = monitorBll.GetList(pageInfo, kwd, dtStart, dtEnd).ToList();
                rsp.Data = list;
                rsp.PagesCount = pageInfo.PagesCount;
                rsp.IsSuccess = true;
                return Ok(rsp);

                /*WebClient client = new WebClient();
                byte[] data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_sysmonitor?CurrentPage=" + pageIndex + "&kwd=" + kwd + "&dtStart=" + dtStart + "&dtEnd=" + dtEnd);
                string result = System.Text.Encoding.UTF8.GetString(data);
                rsp = JsonConvert.DeserializeObject<Response<List<V_ctms_sys_sysmonitor>>>(result);
                return Ok(rsp);*/
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

        [Route("GetUserStatisticsCount"), HttpGet]
        public IHttpActionResult GetUserStatisticsCount(DateTime? dtStart = null, DateTime? dtEnd = null, string type = "DD")
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

                string sql = string.Format(@"select trunc(CREATEDATETIME, '{2}') as RegDate,count(1) as RegCount from (
select a.CREATEDATETIME,a.LOGINNAME,b.USERNAME,
case when b.SEX='1' then '男' when b.SEX='0' then '女' else '未知' end as SEX,
a.MOBILEPHONE,
prov.AREANAME||' '||city.AREANAME||' '||region.AREANAME||' '||town.AREANAME||' '||b.ADDRESSDETAIL as ADDRESS,
a.EMAIL,(CASE a.USERSOURCE when n'0' then 'WEB' when n'1' then 'IOS' when n'2' then 'ANDROID' end) as USERSOURCE,
to_char(sysdate,'yyyy')-to_char(b.BIRTHDATE,'yyyy') as AGE
from CTMS_SYS_USERINFO A 
inner join HR_CNR_USER b on a.USERID=b.USERID
left join HR_AREA prov on b.PROVINCE=prov.AREAID
left join HR_AREA city on b.CITY=city.AREAID
left join HR_AREA region on b.REGION=region.AREAID
left join HR_AREA town on b.TOWN=town.AREAID
where a.USERTYPE=2
and a.CREATEDATETIME>=To_date('{0}', 'yyyy/mm/dd hh24:mi:ss')
and a.CREATEDATETIME<To_date('{1}', 'yyyy/mm/dd hh24:mi:ss')
order by a.CREATEDATETIME desc) result
group by trunc(CREATEDATETIME, '{2}')
order by trunc(CREATEDATETIME, '{2}')", dtStart, dtEnd, type);
                string strType = "";
                switch (type)
                {
                    case "DD":
                        strType = "";
                        break;
                    case "iw":
                        strType = "周";
                        break;
                    case "mm":
                        strType = "月";
                        break;
                    case "q":
                        strType = "季度";
                        break;
                    case "yyyy":
                        strType = "年";
                        break;
                    default:
                        strType = "";
                        break;
                }
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

        [Route("GetErrorDetail"), HttpGet]
        public IHttpActionResult GetErrorDetail(DateTime? dtStart = null, DateTime? dtEnd = null, int pageIndex = 1, int pageSize = 10, string kwd = "")
        {
            Response<IEnumerable<V_ctms_sys_errorcount>> rsp = new Response<IEnumerable<V_ctms_sys_errorcount>>();
            try
            {
                /*WebClient client = new WebClient();
                byte[] data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_errorcount?kwd=" + kwd + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&dtStart=" + dtStart + "&dtEnd=" + dtEnd);
                string result = System.Text.Encoding.UTF8.GetString(data);
                rsp = JsonConvert.DeserializeObject<Response<IEnumerable<V_ctms_sys_errorcount>>>(result);
                return Ok(rsp);*/

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "InputTime",
                    Order = OrderEnum.desc
                };
                var list = errorBll.GetList(pageInfo, kwd, dtStart, dtEnd);
                rsp.Data = list;
                rsp.PagesCount = pageInfo.PagesCount;
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.Message + ex.InnerException);
                rsp.IsSuccess = true;
                rsp.Data = null;
                rsp.ErrorMsg = ex.Message;
                return Ok(rsp);
            }
        }

        [Route("GetServerMonitorSetting"), HttpGet]
        public IHttpActionResult GetServerMonitorSetting(int pageIndex = 1, int pageSize = 10, string kwd = "")
        {
            Response<List<V_ctms_sys_servermonitor>> rsp = new Response<List<V_ctms_sys_servermonitor>>();
            try
            {
                /*WebClient client = new WebClient();
                byte[] data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_servermonitor?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&kwd=" + kwd);
                string result = System.Text.Encoding.UTF8.GetString(data);
                rsp = JsonConvert.DeserializeObject<Response<List<V_ctms_sys_servermonitor>>>(result);
                return Ok(rsp);*/


                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "ModifyTime",
                    Order = OrderEnum.desc
                };

                List<V_ctms_sys_servermonitor> list = smonitorBll.GetList(pageInfo, kwd).ToList();
                foreach (var item in list)
                {
                    var infoList = sinfoBll.GetAll(p => p.IPAddress == item.IPAddress).OrderByDescending(p => p.InputTime).ToList();
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

        [Route("SaveServerMonitorSetting"), HttpPost]
        public IHttpActionResult SaveServerMonitorSetting([FromBody]Request<V_ctms_sys_servermonitor> request)
        {
            Response<bool> rsp = new Response<bool>();
            try
            {
                V_ctms_sys_servermonitor model = request.Data as V_ctms_sys_servermonitor;
                if (model == null)
                {
                    rsp.Data = false;
                    rsp.IsSuccess = true;
                    rsp.ErrorMsg = "参数错误";
                    return Ok(rsp);
                }
                if (string.IsNullOrEmpty(model.ServerMonitorId))
                {
                    model.CreateUserLoginName = curUser.LOGINNAME;
                    model.InputTime = DateTime.Now;
                    model.CreateUserName = curUser.LOGINNAME;
                    model.IsValid = 1;
                    model.IsDeleted = 0;
                }
                model.ModifyUserLoginName = curUser.LOGINNAME;
                model.ModifyTime = DateTime.Now;
                model.ModifyUserName = curUser.LOGINNAME;


                if (string.IsNullOrEmpty(model.ServerMonitorId))
                {
                    var temp = smonitorBll.Get(p => p.IPAddress == model.IPAddress && p.IsDeleted == 0);
                    if (temp != null)
                    {
                        rsp.Data = false;
                        rsp.ErrorMsg = "数据重复，IP地址为：" + model.IPAddress;
                        return Ok(rsp);
                    }
                    rsp.Data = string.IsNullOrEmpty(smonitorBll.Add(model)) ? false : true;
                }
                else
                {
                    rsp.Data = smonitorBll.Edit(model);
                }
                rsp.IsSuccess = true;
                return Ok(rsp);



                /*WebClient client = new WebClient();
                byte[] b = client.UploadValues(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_servermonitor", model.GetParameters());
                rsp = System.Text.Encoding.Default.GetString(b).JsonDeserialize<Response<bool>>();
                return Ok(rsp);*/
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.Data = false;
                rsp.IsSuccess = true;
                rsp.ErrorMsg = "服务器异常";
                return Ok(rsp);
            }
        }

        [Route("GetDistinctServerList"), HttpGet]
        public IHttpActionResult GetDistinctServerList()
        {
            Response<List<V_ctms_sys_servermonitor>> rsp = new Response<List<V_ctms_sys_servermonitor>>();
            try
            {
                /*WebClient client = new WebClient();
                byte[] data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_servermonitor/GetDistinctServerList");
                string result = System.Text.Encoding.UTF8.GetString(data);
                rsp = JsonConvert.DeserializeObject<Response<List<V_ctms_sys_servermonitor>>>(result);
                return Ok(rsp);*/

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

        [Route("GetServerInfoCount"), HttpGet]
        public IHttpActionResult GetServerInfoCount(string ip = "", DateTime? dt = null)
        {
            Response<ActiveCountModel> rsp = new Response<ActiveCountModel>();
            try
            {
                WebClient client = new WebClient();
                DateTime? dtStart = null;
                DateTime? dtEnd = null;
                if (dt != null)
                {
                    dtStart = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, 0, 0, 0);
                    dtEnd = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, 23, 59, 59);
                }
                /*byte[] data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_serverinfo/GetServerInfoCount?ip=" + ip + "&dtStart=" + dtStart + "&dtEnd=" + dtEnd);
                string result = System.Text.Encoding.UTF8.GetString(data);
                Response<List<C_ctms_sys_serverinfo>> rspData = JsonConvert.DeserializeObject<Response<List<C_ctms_sys_serverinfo>>>(result);*/



                ActiveCountModel model = new ActiveCountModel();
                string sql = string.Format(@"select DATE_FORMAT(a.InputTime,'%H:%i') as InputTime,a.CPUValue,a.MemoryValue,a.DiskValue from ctms_sys_serverinfo a where 1=1 {0} {1} {2} order by a.InputTime;", string.IsNullOrEmpty(ip) ? string.Empty : string.Format("and a.IPAddress='{0}'", ip), dtStart == null ? "" : string.Format("and a.InputTime>='{0}'", dtStart), dtEnd == null ? "" : string.Format("and a.InputTime<='{0}'", dtEnd));
                using (DbContext db = new tmpmEntities2())
                {
                    List < C_ctms_sys_serverinfo> serverinfoList = db.Database.SqlQuery<C_ctms_sys_serverinfo>(sql).ToList();
                    if (serverinfoList.Count > 0)
                    {
                        model.legendData = new List<string>() { "CPU使用率", "内存使用率", "磁盘使用率" };
                        model.xAxisData = serverinfoList.Select(p => p.InputTime).ToList();
                        model.seriesData = new List<SeriesDataModel>();

                        SeriesDataModel cpuSeriesDataModel = new SeriesDataModel();
                        cpuSeriesDataModel.name = "CPU使用率";
                        cpuSeriesDataModel.type = "line";
                        cpuSeriesDataModel.data = serverinfoList.Select(p => double.Parse(p.CPUValue.Value.ToString())).ToList();
                        model.seriesData.Add(cpuSeriesDataModel);

                        SeriesDataModel memorySeriesDataModel = new SeriesDataModel();
                        memorySeriesDataModel.name = "内存使用率";
                        memorySeriesDataModel.type = "line";
                        memorySeriesDataModel.data = serverinfoList.Select(p => double.Parse(p.MemoryValue.Value.ToString())).ToList();
                        model.seriesData.Add(memorySeriesDataModel);

                        SeriesDataModel diskSeriesDataModel = new SeriesDataModel();
                        diskSeriesDataModel.name = "磁盘使用率";
                        diskSeriesDataModel.type = "line";
                        diskSeriesDataModel.data = serverinfoList.Select(p => double.Parse(p.DiskValue.Value.ToString())).ToList();
                        model.seriesData.Add(diskSeriesDataModel);

                        rsp.Data = model;
                    }
                }

                
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

        [Route("GetServerAlarmList"), HttpGet]
        public IHttpActionResult GetServerAlarmList(int pageIndex = 1, int pageSize = 10, string kwd = "", int? type = null)
        {
            Response<List<V_ctms_sys_serveralarm>> rsp = new Response<List<V_ctms_sys_serveralarm>>();
            try
            {
                /*WebClient client = new WebClient();
                byte[] data = client.DownloadData(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/" + "api/ctms_sys_serveralarm?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&kwd=" + kwd + "&type=" + type);
                string result = System.Text.Encoding.UTF8.GetString(data);
                rsp = JsonConvert.DeserializeObject<Response<List<V_ctms_sys_serveralarm>>>(result);
                return Ok(rsp);*/

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "InputTime",
                    Order = OrderEnum.desc
                };

                List<V_ctms_sys_serveralarm> list = saBll.GetList(pageInfo, kwd, type);

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

    public class ActiveCountModel
    {
        public string title { get; set; }

        public List<string> legendData { get; set; }

        public List<string> xAxisData { get; set; }

        public List<SeriesDataModel> seriesData { get; set; }
    }

    public class ActiveCountDataModel
    {
        public string datelist { get; set; }
        public string ModelName { get; set; }
        public int? count { get; set; }
    }

    public class UserRegCountDataModel
    {
        public DateTime RegDate { get; set; }
        public int RegCount { get; set; }
    }

    public class SeriesDataModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public string stack { get; set; }
        public int barWidth { get; set; }
        public List<double> data { get; set; }
    }
}
