using Project.BLL;
using Project.BLL.OrganizationManage;
using Project.Common;
using Project.Common.Cached;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
using Project.Model.OrganizationManage;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.UI.Controllers.API.OrganizationManage
{
    [RoutePrefix("API/HRUserPost")]
    public class HRUserPostController : ApiController
    {
        [Route("Index"), HttpGet]
        public IHttpActionResult Index(int CurrentPage, string postID = "", string userName = "")
        {
            Response<List<HRUserPostDTO>> response = new Response<List<HRUserPostDTO>>();
            HRUserPostBLL bll = new HRUserPostBLL();
            Expression<Func<ctms_hr_userpost, bool>> pridicate = p => (string.IsNullOrEmpty(postID) ? true : (p.POSTID.Equals(postID) || p.COMPANYID.Equals(postID) || p.DEPARTMENTID.Equals(postID))) && (string.IsNullOrEmpty(userName) ? true : p.USERNAME.Contains(userName));
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CREATEDATE",
                Order = OrderEnum.asc
            };
            try
            {
                List<HRUserPost> list = bll.GetList(ref pageInfo, pridicate);
                response.Data = DataDTO(list);
                response.PagesCount = pageInfo.PagesCount;
                response.RecordsCount = pageInfo.Total;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        private List<HRUserPostDTO> DataDTO(List<HRUserPost> list)
        {
            List<HRUserPostDTO> data = new List<HRUserPostDTO>();
            List<HRPost> posts = new HRPostBLL().GetList(p => true);
            List<HRDepartment> departments = new HRDepartmentBLL().GetList(p => true);
            List<HRCompany> companys = new HRCompanyBLL().GetList(p => true);
            ctms_sys_userinfoBLL userBll = new ctms_sys_userinfoBLL();
            foreach (HRUserPost item in list)
            {
                HRPost currentPost = posts.FirstOrDefault(p => p.PostID.Equals(item.PostID));
                HRDepartment currentDepartment = departments.FirstOrDefault(p => p.DepartmentID.Equals(currentPost.DepartmentID));
                HRCompany currentCompany = companys.FirstOrDefault(p => p.CompanyID.Equals(currentDepartment.CompanyID));
                var user = userBll.Get(p=>p.USERID == item.UserID);

                HRUserPostDTO dto = new HRUserPostDTO();
                if (user != null)
                {
                    item.UserName = user.USERNAME;
                    dto.UserLoginName = user.LOGINNAME;
                }
                dto.EmployeepostID = item.EmployeepostID;
                dto.IsAgency = item.IsAgency;
                dto.UserID = item.UserID;
                dto.PostID = item.PostID;
                dto.PostLevel = item.PostLevel;
                dto.CheckState = item.CheckState;
                dto.EditState = item.EditState;
                dto.CreateUserID = item.CreateUserID;
                dto.CreateDate = item.CreateDate;
                dto.UpdateUserID = item.UpdateUserID;
                dto.UpdateDate = item.UpdateDate;
                dto.PostName = currentPost.PostName;
                dto.DepartmentName = currentDepartment.DepartmentName;
                dto.DepartmentID = currentDepartment.DepartmentID;
                dto.CName = currentCompany.CName;
                dto.CompanyID = currentCompany.CompanyID;
                dto.UserName = item.UserName;

                data.Add(dto);
            }

            return data;
        }

        /// <summary>
        /// 公司/医院新增
        /// </summary>
        /// <returns></returns>
        [Route("SaveCompany"), HttpPost]
        public IHttpActionResult SaveCompany([FromBody] Request<HRCompany> request)
        {
            //申明返回
            Response<string> response = new Response<string>();
            HRCompany company = request.Data;
            HRCompanyBLL bll = new HRCompanyBLL();
            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(company.CompanyID))
                {
                    company.CreateDate = DateTime.Now;
                    result = bll.Add(company);
                }
                else
                {
                    string cName = company.CName;
                    string id = company.CompanyID;
                    company = bll.GetOne(p => p.COMPANYID.Equals(id));
                    if (company != null)
                    {
                        company.CName = cName;
                        result = bll.Edit(company);
                    }
                }

                if (result)
                {
                    response.IsSuccess = true;
                    response.Data = "ok";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = "false";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        /// <summary>
        /// 获取医院、公司名分页数据
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        [Route("CompanyList"), HttpGet]
        public IHttpActionResult CompanyList(int CurrentPage)
        {
            Response<List<HRCompany>> response = new Response<List<HRCompany>>();
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CREATEDATE",
                Order = OrderEnum.asc
            };

            try
            {
                List<HRCompany> data = new HRCompanyBLL().GetList(ref pageInfo, p => true).ToList();
                response.IsSuccess = true;
                response.Data = data;
                response.PagesCount = pageInfo.PagesCount;
                response.RecordsCount = pageInfo.Total;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        /// <summary>
        /// 科室（部门）新增
        /// </summary>
        /// <returns></returns>
        [Route("SaveDepartment"), HttpPost]
        public IHttpActionResult SaveDepartment([FromBody] Request<HRDepartment> request)
        {
            //申明返回
            Response<string> response = new Response<string>();
            HRDepartment department = request.Data;
            HRDepartmentBLL bll = new HRDepartmentBLL();

            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(department.DepartmentID))
                {
                    department.CreateDate = DateTime.Now;
                    department.DepartmentBossHead = string.Empty;
                    result = bll.Add(department);
                }
                else
                {
                    string id = department.DepartmentID;
                    HRDepartment model = bll.GetOne(p => p.DEPARTMENTID.Equals(id));
                    if (model != null)
                    {
                        model.CompanyID = department.CompanyID;
                        model.DepartmentName = department.DepartmentName;
                        result = bll.Edit(model);
                    }
                }

                if (result)
                {
                    response.IsSuccess = true;
                    response.Data = "ok";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = "false";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        /// <summary>
        /// 获取科室（部门）分页数据
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        [Route("DepartmentList"), HttpGet]
        public IHttpActionResult DepartmentList(int CurrentPage)
        {
            Response<List<HRDepartmentDTO>> response = new Response<List<HRDepartmentDTO>>();
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CREATEDATE",
                Order = OrderEnum.asc
            };

            try
            {
                List<HRDepartment> data = new HRDepartmentBLL().GetList(ref pageInfo, p => true).ToList();
                response.IsSuccess = true;
                response.Data = FetchCompany(data);
                response.PagesCount = pageInfo.PagesCount;
                response.RecordsCount = pageInfo.Total;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        private List<HRDepartmentDTO> FetchCompany(List<HRDepartment> list)
        {
            List<HRDepartmentDTO> data = new List<HRDepartmentDTO>();
            List<HRCompany> companys = new HRCompanyBLL().GetList(p => true);
            foreach (HRDepartment item in list)
            {
                HRDepartmentDTO dto = new HRDepartmentDTO()
                {
                    CompanyID = item.CompanyID,
                    CompanyName = companys.FirstOrDefault(p => p.CompanyID.Equals(item.CompanyID)).CName,
                    DepartmentCode = item.DepartmentCode,
                    DepartmentID = item.DepartmentID,
                    DepartmentLevel = item.DepartmentLevel,
                    DepartmentName = item.DepartmentName
                };
                data.Add(dto);
            }

            return data;
        }

        /// <summary>
        /// 岗位新增
        /// </summary>
        /// <returns></returns>
        [Route("SavePost"), HttpPost]
        public IHttpActionResult SavePost([FromBody] Request<HRPost> request)
        {
            //申明返回
            Response<string> response = new Response<string>();
            HRPost post = request.Data;
            HRPostBLL bll = new HRPostBLL();

            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(post.PostID))
                {
                    post.CreateDate = DateTime.Now;
                    result = bll.Add(post);
                }
                else
                {
                    string id = post.PostID;
                    HRPost model = bll.GetOne(p => p.POSTID.Equals(id));
                    if (model != null)
                    {
                        model.CompanyID = post.CompanyID;
                        model.DepartmentName = post.DepartmentName;
                        model.DepartmentID = post.DepartmentID;
                        model.PostName = post.PostName;
                        result = bll.Edit(model);
                    }
                }

                if (result)
                {
                    response.IsSuccess = true;
                    response.Data = "ok";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = "false";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        /// <summary>
        /// 岗位分页数据
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        [Route("PostList"), HttpGet]
        public IHttpActionResult PostList(int CurrentPage)
        {
            Response<List<HRPost>> response = new Response<List<HRPost>>();
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CREATEDATE",
                Order = OrderEnum.asc
            };

            try
            {
                List<HRPost> data = new HRPostBLL().GetList(ref pageInfo, p => true).ToList();
                HRCompanyBLL companyBLL = new HRCompanyBLL();
                List<HRCompany> list = companyBLL.GetList(p => true);
                data.ForEach(p => p.PostCode = list.FirstOrDefault(k => k.CompanyID.Equals(p.CompanyID)).CName);

                response.IsSuccess = true;
                response.Data = data;
                response.PagesCount = pageInfo.PagesCount;
                response.RecordsCount = pageInfo.Total;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        /// <summary>
        /// 用户分页数据
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        [Route("UserList"), HttpGet]
        public IHttpActionResult UserList(int CurrentPage, string Name = "")
        {
            Response<List<V_ctms_sys_userinfo>> response = new Response<List<V_ctms_sys_userinfo>>();
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CREATEDATETIME",
                Order = OrderEnum.asc
            };

            try
            {
                List<V_ctms_sys_userinfo> data = new ctms_sys_userinfoBLL().GetList(ref pageInfo, Name);
                response.IsSuccess = true;
                response.Data = data;
                response.PagesCount = pageInfo.PagesCount;
                response.RecordsCount = pageInfo.Total;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        /// <summary>
        /// 获取组织架构(选择控件使用)
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [Route("GetOrganList"), HttpGet]
        public IHttpActionResult GetOrganList(int ouType, string keyword, string functionCode = "CTMS_SYS_USERINFO")
        {
            try
            {
                Response<List<HROrganUserDTO>> response = new Response<List<HROrganUserDTO>>()
                {
                    Data = new HRUserPostBLL(functionCode).GetOrganList((OrganUserType)ouType, keyword),
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return Ok(new Response<string>()
                {
                    IsSuccess = false,
                    ErrorMsg = ex.Message,
                });
            }

        }
        /// <summary>
        /// 获取组织架构(选择控件使用)
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [Route("GetUserList"), HttpGet]
        public IHttpActionResult GetUserList(int ouType, string keyword, string functionCode = "CTMS_SYS_USERINFO")
        {
            try
            {
                Response<List<HROrganUserDTO>> response = new Response<List<HROrganUserDTO>>()
                {
                    Data = new HRUserPostBLL(functionCode).GetUserList((OrganUserType)ouType, keyword),
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return Ok(new Response<string>()
                {
                    IsSuccess = false,
                    ErrorMsg = ex.Message,
                });
            }
        }
        /// <summary>
        /// 员工岗位新增
        /// </summary>
        /// <returns></returns>
        [Route("SaveUserPost"), HttpPost]
        public IHttpActionResult SaveUserPost([FromBody] Request<HRUserPost> request)
        {
            //申明返回
            Response<string> response = new Response<string>();
            HRUserPost post = request.Data;
            HRUserPostBLL bll = new HRUserPostBLL();
            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(post.UserID) || string.IsNullOrEmpty(post.UserName))
                    throw new NotSupportedException();

                if (string.IsNullOrEmpty(post.EmployeepostID))
                {
                    if (bll.GetOne(p => p.USERID == post.UserID && p.POSTID == post.PostID) != null)
                    {
                        response.IsSuccess = true;
                        response.Data = "ok";
                        return Ok(response);
                    }

                    post.CreateDate = DateTime.Now;
                    result = bll.Add(post);
                }
                else
                {
                    HRUserPost model = bll.GetOne(p => p.EMPLOYEEPOSTID == post.EmployeepostID);
                    if (model == null)
                        throw new KeyNotFoundException();

                    model.CName = post.CName;
                    model.CompanyID = post.CompanyID;
                    model.DepartmentID = post.DepartmentID;
                    model.DepartmentName = post.DepartmentName;
                    model.PostID = post.PostID;
                    model.PostLevel = post.PostLevel;
                    model.PostName = post.PostName;
                    model.UpdateDate = DateTime.Now;
                    model.UserID = post.UserID;
                    model.UserName = post.UserName;

                    result = bll.Edit(model);
                }

                bll.UpdateUserOrgan(post.UserID, post.CompanyID, post.DepartmentID, post.PostID);
                if (result)
                {
                    response.IsSuccess = true;
                    response.Data = "ok";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = "false";
                }
            }
            catch (NotSupportedException)
            {
                LogHelper.WriteError("用户信息为空！");
                response.IsSuccess = false;
                response.ErrorMsg = "用户信息为空！";
            }
            catch (KeyNotFoundException)
            {
                LogHelper.WriteError("该记录不存在！");
                response.IsSuccess = false;
                response.ErrorMsg = "该记录不存在！";
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = "保存失败！";
            }

            return Ok(response);
        }

        /// <summary>
        /// 删除用户岗位数据
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        [Route("Delete"), HttpGet]
        public IHttpActionResult Delete(string ID)
        {
            Response<string> response = new Response<string>();
            try
            {
                if (new HRUserPostBLL().Delete(ID))
                    response.IsSuccess = true;
                else
                    response.IsSuccess = false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        /// <summary>
        /// 获取组织架构树形数据
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            Response<string> response = new Response<string>();
            try
            {
                //var tree = LocalCachedProvider.Instance.Get("OrganizationManageTree").ToString();
                //if (tree != null) return Ok(tree);
                List<HRCompany> list = new HRCompanyBLL().GetList(p => true);
                string tree = CreateTree(list).JsonSerialize();
                LocalCachedProvider.Instance.Set("OrganizationManageTree", tree);
                response.Data = tree;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        private List<OrganizationTree> CreateTree(List<HRCompany> list)
        {
            if (!list.Any())
            {
                return null;
            }
            List<HRDepartment> departments = new HRDepartmentBLL().GetList(p => true);
            List<HRPost> posts = new HRPostBLL().GetList(p => true);

            return list.Select(type => new OrganizationTree()
            {
                text = type.CName,
                value = type.CompanyID,
                tags = "0",
                nodes = CreateDepartmentNode(departments.Where(p => p.CompanyID.Equals(type.CompanyID)), posts)
            }).ToList();
        }

        private List<OrganizationTree> CreateDepartmentNode(IEnumerable<HRDepartment> departments, List<HRPost> posts)
        {
            return departments.Select(type => new OrganizationTree()
            {
                text = type.DepartmentName,
                value = type.DepartmentID,
                tags = "1",
                nodes = CreatePostNode(posts.Where(p => p.DepartmentID.Equals(type.DepartmentID)))
            }).ToList();
        }

        private List<OrganizationTree> CreatePostNode(IEnumerable<HRPost> posts)
        {
            return posts.Select(type => new OrganizationTree()
            {
                text = type.PostName,
                value = type.PostID,
                tags = "2",
                nodes = null
            }).ToList();
        }

        [Route("DelOrg"), HttpGet]
        public IHttpActionResult DelOrg(string id, string type)
        {
            Response<string> response = new Response<string>();
            bool result = true;
            try
            {

                switch (type)
                {
                    case "0"://医院、公司
                        result = new HRCompanyBLL().Del(id);
                        break;
                    case "1"://部门
                        result = new HRDepartmentBLL().Del(id);
                        break;
                    case "2"://岗位
                        result = new HRPostBLL().Del(id);
                        break;
                    default:
                        break;
                }

                if (result)
                {
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMsg = "该记录不存在！";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        [Route("SaveOrg"), HttpGet]
        public IHttpActionResult SaveOrg(string id, string type, string text)
        {
            Response<string> response = new Response<string>();
            bool result = true;
            try
            {
                switch (type)
                {
                    case "0"://医院、公司
                        HRCompanyBLL companyBLL = new HRCompanyBLL();
                        HRCompany company = companyBLL.GetOne(p => p.COMPANYID.Equals(id));
                        if (company != null)
                        {
                            company.CName = text;
                            result = companyBLL.Edit(company);
                        }
                        break;
                    case "1"://部门
                        HRDepartmentBLL departmentBLL = new HRDepartmentBLL();
                        HRDepartment department = departmentBLL.GetOne(p => p.DEPARTMENTID.Equals(id));
                        if (department != null)
                        {
                            department.DepartmentName = text;
                            result = departmentBLL.Edit(department);
                        }
                        break;
                    case "2"://岗位
                        HRPostBLL postBLL = new HRPostBLL();
                        HRPost post = postBLL.GetOne(p => p.POSTID.Equals(id));
                        if (post != null)
                        {
                            post.PostName = text;
                            result = postBLL.Edit(post);
                        }
                        break;
                    default:
                        break;
                }

                if (result)
                {
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMsg = "修改记录失败！";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }

        [Route("GetDict"), HttpGet]
        public IHttpActionResult GetDict(string code)
        {
            Response<IEnumerable<DictItem>> response = new Response<IEnumerable<DictItem>>();
            DictionaryBLL bll = new DictionaryBLL();
            try
            {
                response.Data = bll.GetItemList(p => p.dictioncategory.Equals(code) && p.isdeleted == 0);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                response.IsSuccess = false;
                response.ErrorMsg = ex.ToString();
            }

            return Ok(response);
        }
    }
}
