using Project.BLL;
using Project.BLL.Authorization;
using Project.BLL.Examine;
using Project.BLL.OrganizationManage;
using Project.Common;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
using Project.Model.Examine;
using Project.Model.OrganizationManage;
using Project.UI.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Project.UI.Controllers.API.OrganizationManage
{
    [RoutePrefix("API/HrCompany")]
    public class HrCompanyController : ApiController
    {
        private V_ctms_sys_userinfo user = null;

        public HrCompanyController()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
            if (cookie != null)
            {
                string tokenValue = cookie.Value;
                if (!string.IsNullOrEmpty(tokenValue))
                {
                    user = new ctms_sys_userinfoBLL().GetLoginInfo(tokenValue);
                }
            }
        }

        [Route("Get"), HttpGet]
        public IHttpActionResult Get(int CurrentPage, int PageSize = 10)
        {
            Response<List<HRCompany>> response = new Response<List<HRCompany>>();
            try
            {
                //申明操作
                HRCompanyBLL bll = new HRCompanyBLL();
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = PageSize,
                    OrderField = "CreateDate",
                    Order = OrderEnum.asc
                };


                ArrayList args = new ArrayList();
                string filterString = new RoleFunctionBLL().GetFilterString(user.USERID, "CTMS_SUP_DOCTOR", PermissionType.View, ref args);
                LogHelper.WriteError("搜索医生的条件为：" + filterString);
                List<HRCompany> list = bll.GetList(args, filterString, p => true);
                List<HRCompany> result = list.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                SupDoctorBLL docBll = new SupDoctorBLL();
                response.IsSuccess = true;
                response.Data = result;
                response.PagesCount = list.Count % PageSize == 0 ? list.Count / PageSize : list.Count / PageSize + 1;
                response.RecordsCount = list.Count;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        [Route("SearchDept"), HttpGet]
        public IHttpActionResult GetDept(int currentPage = 1, string cmpId = "", int pageSize = 10)
        {
            Response<List<SearchDeptModel>> rsp = new Response<List<SearchDeptModel>>();
            try
            {
                HRDepartmentBLL deptBll = new HRDepartmentBLL();
                SupDoctorGroupBLL dgBll = new SupDoctorGroupBLL();
                HRCompanyBLL bll = new HRCompanyBLL();

                HRCompany cmp = null;
                if (!string.IsNullOrEmpty(cmpId))
                    cmp = bll.GetOne(p => p.COMPANYID == cmpId);

                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = currentPage,
                    PageSize = pageSize,
                    OrderField = "CreateDate",
                    Order = OrderEnum.desc
                };

                List<HRDepartment> deptList = new List<HRDepartment>();
                ArrayList args = new ArrayList();
                string filterString = new RoleFunctionBLL().GetFilterString(user.USERID, "CTMS_SUP_DOCTOR", PermissionType.View, ref args);
                using (DbContext db = new tmpmEntities2())
                {
                    var listTemp = db.Set<ctms_hr_department>().where(filterString, args.ToArray()).Select(deptBll.EntityToModel).ToList();
                    if (!string.IsNullOrEmpty(cmpId))
                    {
                        //deptList = deptBll.GetList(ref pageInfo, p => p.COMPANYID == cmpId);
                        deptList = listTemp.Where(p => p.CompanyID == cmpId).ToList();
                    }
                    else
                    {
                        //deptList = deptBll.GetList(ref pageInfo, p => true);
                        deptList = listTemp;
                    }
                }

                List<SearchDeptModel> list = new List<SearchDeptModel>();

                for (int i = 0; i < deptList.Count; i++)
                {
                    var item = deptList[i];
                    SearchDeptModel temp = new SearchDeptModel()
                    {
                        DeptId = item.DepartmentID,
                        Type = 0,
                        Name = item.DepartmentName,
                        HospitalName = cmp == null ? "" : cmp.CName,
                        IconPath = ""
                    };
                    list.Add(temp);
                }

                pageInfo = new PageInfo()
                {
                    PageIndex = currentPage,
                    PageSize = 10000,
                    OrderField = "CreateDateTime",
                    Order = OrderEnum.desc
                };

                List<SupDoctorGroup> dgList = new List<SupDoctorGroup>();
                if (!string.IsNullOrEmpty(cmpId))
                    dgList = dgBll.GetList(ref pageInfo, p => p.ownercompanyid == cmpId && p.isdeleted == 0);
                else
                    dgList = dgBll.GetList(ref pageInfo, p => p.isdeleted == 0);

                for (int i = 0; i < dgList.Count; i++)
                {
                    var item = dgList[i];
                    SearchDeptModel temp = new SearchDeptModel()
                    {
                        DeptId = item.ID,
                        Type = 1,
                        Name = item.GroupName,
                        HospitalName = cmp == null ? "" : cmp.CName,
                        IconPath = item.GroupIcon
                    };
                    list.Add(temp);
                }

                rsp.IsSuccess = true;
                rsp.Data = list.OrderBy(p => p.Type).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError("获取科室失败，错误信息为：" + ex.ToString() + ex.InnerException);
                rsp.IsSuccess = false;
                rsp.ErrorMsg = ex.Message;
                return Ok(rsp);
            }
        }
    }

    public class SearchDeptModel
    {
        /// <summary>
        /// 协作组/科室id
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 类型：0科室  1协作组
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        public string HospitalName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string IconPath { get; set; }
    }
}
