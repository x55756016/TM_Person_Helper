using Project.BLL.Authorization;
using Project.Common;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.DAL.OrganizationManage;
using Project.Model;
using Project.Model.OrganizationManage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project.BLL.OrganizationManage
{
    public class HRUserPostBLL
    {
        protected static List<string> AuthCompanyIDList = new List<string>();
        protected static List<string> AuthDepartmentIDList = new List<string>();
        protected static List<string> AuthPostIDList = new List<string>();
        protected static List<string> AuthUserIDList = new List<string>();
        V_ctms_sys_userinfo user = null;

        public HRUserPostBLL()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
            user = new ctms_sys_userinfoBLL().GetLoginInfo(cookie.Value);
        }

        public HRUserPostBLL(string FunctionCode)
        {

            using (DbContext db = new tmpmEntities2())
            {
                if (user != null)
                {
                    //取对应角色的权限
                    ArrayList args = new ArrayList();
                    switch (FunctionCode.ToUpper())
                    {
                        case "CTMS_SYS_USERINFO":
                            string filterString = new RoleFunctionBLL().GetFilterString(user.USERID, "CTMS_SYS_USERINFO", PermissionType.View, ref args);
                            var model = db.Set<ctms_sys_userinfo>().AsQueryable();
                            if (!string.IsNullOrEmpty(filterString))
                            {
                                model = model.where(filterString, args.ToArray());
                                AuthCompanyIDList = model.Select(o => o.OWNERCOMPANYID).ToList();
                                AuthDepartmentIDList = model.Select(o => o.OWNERDEPARTMENTID).ToList();
                                AuthPostIDList = model.Select(o => o.OWNERPOSTID).ToList();
                                AuthUserIDList = model.Select(o => o.USERID).ToList();
                            }
                            break;

                        case "CTMS_SUP_DOCTOR":
                            string filterString2 = new RoleFunctionBLL().GetFilterString(user.USERID, "CTMS_SUP_DOCTOR", PermissionType.View, ref args);
                            var model2 = db.Set<ctms_sup_doctor>().AsQueryable();
                            if (!string.IsNullOrEmpty(filterString2))
                            {
                                model2 = model2.where(filterString2, args.ToArray());
                                AuthCompanyIDList = model2.Select(o => o.ownercompanyid).ToList();
                                AuthDepartmentIDList = model2.Select(o => o.ownerdepartmentid).ToList();
                                AuthPostIDList = model2.Select(o => o.ownerpostid).ToList();
                                AuthUserIDList = model2.Select(o => o.userid).ToList();
                            }
                            break;
                        default:
                            break;
                    }


                }
            }
        }

        /// <summary>
        /// 根据条件获取员工岗位
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public HRUserPost GetOne(Expression<Func<ctms_hr_userpost, bool>> predicate = null)
        {
            using (HRUserPostDAL dal = new HRUserPostDAL())
            {
                return EntityToModel(dal.FindOne(predicate));
            }
        }

        /// <summary>
        /// 根据条件分页获取员工岗位
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<HRUserPost> GetList(ref PageInfo pager, Expression<Func<ctms_hr_userpost, bool>> predicate = null)
        {
            using (HRUserPostDAL dal = new HRUserPostDAL())
            {
                var list = dal.FindAll(predicate);

                return list.Paging(ref pager).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(HRUserPost model)
        {
            if (model == null) return false;
            using (HRUserPostDAL dal = new HRUserPostDAL())
            {
                ctms_hr_userpost entitys = ModelToEntity(model);

                return dal.Update(entitys);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            using (HRUserPostDAL dal = new HRUserPostDAL())
            {
                return dal.DeleteById(id);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(HRUserPost model)
        {
            if (model == null)
                return false;
            using (HRUserPostDAL dal = new HRUserPostDAL())
            {
                if (string.IsNullOrEmpty(model.EmployeepostID)) model.EmployeepostID = Guid.NewGuid().ToString();
                ctms_hr_userpost entity = ModelToEntity(model);

                return dal.Insert(entity);
            }
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetUserNameByUserID(string userID)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_userinfo entity = db.Set<ctms_sys_userinfo>().Find(userID);

                return entity == null ? "" : entity.LOGINNAME;
            }
        }

        /// <summary>
        /// 获取组织树（选人控件）
        /// </summary>
        /// <param name="ouType">类型</param>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        public List<HROrganUserDTO> GetOrganList(OrganUserType ouType, string keyword)
        {
            using (DbContext db = new tmpmEntities2())
            {
                List<HROrganUserDTO> list = new List<HROrganUserDTO>();
                switch (ouType)
                {
                    //医院
                    case OrganUserType.Company:
                        list = GetOrganListByCompany(keyword);
                        break;
                    //科室
                    case OrganUserType.Department:
                        list = GetOrganListByDepartment(keyword);
                        break;
                    //岗位
                    case OrganUserType.Position:
                        list = GetOrganListByPosition(keyword);
                        break;
                    //人员
                    case OrganUserType.User:
                        list = GetOrganListByPosition(keyword, true);
                        break;
                    default:
                        break;
                }

                return list;
            }
        }
        /// <summary>
        /// 获取公司树
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<HROrganUserDTO> GetOrganListByCompany(string keyword)
        {
            List<HROrganUserDTO> list = new List<HROrganUserDTO>();
            if (string.IsNullOrEmpty(keyword))
            {
                list = new HRCompanyBLL().GetList(o => AuthCompanyIDList.Contains(o.COMPANYID)).OrderBy(o => o.CName).Select(o => new HROrganUserDTO()
                {
                    name = o.CName,
                    id = o.CompanyID,
                    level = ((int)OrganUserType.Company).ToString(),
                    data = string.Format("{0}|{1}////", o.CompanyID, o.CName)
                }).ToList();
            }
            else
            {
                list = new HRCompanyBLL().GetList(o => AuthCompanyIDList.Contains(o.COMPANYID) && o.CNAME.Contains(keyword)).OrderBy(o => o.CName).Select(o => new HROrganUserDTO()
                {
                    name = o.CName,
                    id = o.CompanyID,
                    level = ((int)OrganUserType.Company).ToString(),
                    data = string.Format("{0}|{1}////", o.CompanyID, o.CName)
                }).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取科室树
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<HROrganUserDTO> GetOrganListByDepartment(string keyword)
        {
            using (DbContext db = new tmpmEntities2())
            {
                List<HROrganUserDTO> list = new List<HROrganUserDTO>();
                IEnumerable<ctms_hr_department> enityList;
                if (string.IsNullOrEmpty(keyword))
                {
                    enityList = db.Set<ctms_hr_department>().Where(o => AuthDepartmentIDList.Contains(o.COMPANYID)).OrderBy(o => o.DEPARTMENTNAME);
                }
                else
                {
                    enityList = db.Set<ctms_hr_department>().Where(o => AuthDepartmentIDList.Contains(o.COMPANYID) && o.DEPARTMENTNAME.Contains(keyword));
                }
                List<string> CompanyIDs = enityList.Select(o => o.COMPANYID).ToList();
                foreach (ctms_hr_company com in db.Set<ctms_hr_company>().Where(o => CompanyIDs.Contains(o.COMPANYID) && AuthCompanyIDList.Contains(o.COMPANYID)).OrderBy(o => o.CNAME))
                {
                    list.Add(new HROrganUserDTO()
                    {
                        name = com.CNAME,
                        id = com.COMPANYID,
                        level = ((int)OrganUserType.Company).ToString(),
                        data = string.Format("{0}|{1}////", com.COMPANYID, com.CNAME),
                        nocheck = true,
                        open = true,
                        children = enityList.Where(o => o.COMPANYID.Equals(com.COMPANYID)).Select(o => new HROrganUserDTO()
                        {
                            name = o.DEPARTMENTNAME,
                            id = o.DEPARTMENTID,
                            level = ((int)OrganUserType.Department).ToString(),
                            nocheck = false,
                            open = false,
                            data = string.Format("{0}|{1}/{2}|{3}///", com.COMPANYID, com.CNAME, o.DEPARTMENTID, o.DEPARTMENTNAME),
                        }).ToList()
                    });
                }
                return list;
            }
        }

        /// <summary>
        /// 获取岗位树
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="userInclude"></param>
        /// <returns></returns>
        public List<HROrganUserDTO> GetOrganListByPosition(string keyword, bool userInclude = false)
        {
            using (DbContext db = new tmpmEntities2())
            {
                List<HROrganUserDTO> list = new List<HROrganUserDTO>();
                IEnumerable<ctms_hr_post> enityList;
                if (string.IsNullOrEmpty(keyword))
                {
                    enityList = db.Set<ctms_hr_post>().Where(o => AuthPostIDList.Contains(o.POSTID)).OrderBy(o => o.POSTNAME);
                }
                else
                {
                    enityList = db.Set<ctms_hr_post>().Where(o => AuthPostIDList.Contains(o.POSTID) && o.POSTNAME.Contains(keyword));
                }
                List<string> CompanyIDs = enityList.Select(o => o.COMPANYID).Distinct().ToList();
                List<string> DepartmentIDs = enityList.Select(o => o.DEPARTMENTID).Distinct().ToList();
                List<string> PostIDs = enityList.Select(o => o.POSTID).Distinct().ToList();
                foreach (string companyID in CompanyIDs)
                {
                    ctms_hr_company com = db.Set<ctms_hr_company>().Find(companyID);
                    if (com == null) continue;
                    HROrganUserDTO comDTO = new HROrganUserDTO()
                    {
                        name = com.CNAME,
                        id = com.COMPANYID,
                        level = ((int)OrganUserType.Company).ToString(),
                        data = string.Format("{0}|{1}////", com.COMPANYID, com.CNAME),
                        nocheck = true,
                        open = true,
                        children = new List<HROrganUserDTO>()
                    };
                    foreach (ctms_hr_department dept in db.Set<ctms_hr_department>().Where(o => o.COMPANYID.Equals(com.COMPANYID) && DepartmentIDs.Contains(o.DEPARTMENTID) && AuthDepartmentIDList.Contains(o.DEPARTMENTID)))
                    {
                        HROrganUserDTO deptDTO = new HROrganUserDTO()
                        {
                            name = dept.DEPARTMENTNAME,
                            id = dept.DEPARTMENTID,
                            level = ((int)OrganUserType.Department).ToString(),
                            data = string.Format("{0}|{1}/{2}|{3}///", com.COMPANYID, com.CNAME, dept.DEPARTMENTID, dept.DEPARTMENTNAME),
                            nocheck = true,
                            open = true,
                            children = new List<HROrganUserDTO>()
                        };
                        foreach (ctms_hr_post post in db.Set<ctms_hr_post>().Where(o => o.DEPARTMENTID.Equals(dept.DEPARTMENTID) && PostIDs.Contains(o.POSTID) && AuthPostIDList.Contains(o.POSTID)))
                        {
                            HROrganUserDTO postDTO = new HROrganUserDTO()
                            {
                                name = post.POSTNAME,
                                id = post.POSTID,
                                level = ((int)OrganUserType.Position).ToString(),
                                data = string.Format("{0}|{1}/{2}|{3}/{4}|{5}//", com.COMPANYID, com.CNAME, dept.DEPARTMENTID, dept.DEPARTMENTNAME, post.POSTID, post.POSTNAME),
                                nocheck = userInclude,
                                open = !userInclude,
                                children = new List<HROrganUserDTO>()
                            };
                            deptDTO.children.Add(postDTO);
                        }
                        comDTO.children.Add(deptDTO);
                    }
                    list.Add(comDTO);
                }
                return list;
            }
        }

        /// <summary>
        /// 根据岗位ID查询用户
        /// </summary>
        /// <param name="PostionID"></param>
        /// <returns></returns>
        public List<HROrganUserDTO> GetUserList(OrganUserType ouType, string keyword)
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_hr_userpost> entityList = null;
                if (string.IsNullOrEmpty(keyword))
                {
                    entityList = db.Set<ctms_hr_userpost>().Where(o => AuthCompanyIDList.Contains(o.COMPANYID) && AuthDepartmentIDList.Contains(o.DEPARTMENTID) && AuthPostIDList.Contains(o.POSTID) && AuthUserIDList.Contains(o.USERID));
                }
                else
                {
                    switch (ouType)
                    {
                        case OrganUserType.Company:
                            entityList = db.Set<ctms_hr_userpost>().Where(o => o.COMPANYID.Equals(keyword) && AuthCompanyIDList.Contains(o.COMPANYID));
                            break;
                        case OrganUserType.Department:
                            entityList = db.Set<ctms_hr_userpost>().Where(o => o.DEPARTMENTID.Equals(keyword) && AuthDepartmentIDList.Contains(o.DEPARTMENTID));
                            break;
                        case OrganUserType.Position:
                            entityList = db.Set<ctms_hr_userpost>().Where(o => o.POSTID.Equals(keyword) && AuthPostIDList.Contains(o.POSTID));
                            break;
                        case OrganUserType.User:
                            entityList = db.Set<ctms_hr_userpost>().Where(o => o.USERNAME.Contains(keyword) && AuthUserIDList.Contains(o.USERID));
                            break;
                        default:
                            break;
                    }
                }
                if (entityList == null) return new List<HROrganUserDTO>();
                return entityList.Select(o => new HROrganUserDTO()
                {
                    name = o.USERNAME,
                    id = o.USERID,
                    level = ((int)OrganUserType.User).ToString(),
                    //data = c.COMPANYID + "|" + c.CNAME + "/" + d.DEPARTMENTID + "|" + d.DEPARTMENTNAME + "/" + p.POSTID + "|" + p.POSTNAME + "/" + ui.USERID + "|" + ui.USERNAME + "/",
                    data = string.Format("{0}|{1}/{2}|{3}/{4}|{5}/{6}|{7}/", o.COMPANYID, o.CNAME, o.DEPARTMENTID, o.DEPARTMENTNAME, o.POSTID, o.POSTNAME, o.USERID, o.USERNAME),
                    nocheck = true,
                    open = false,
                }).ToList();
            }
        }

        /// <summary>
        /// 更新用户对应表组织架构
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool UpdateUserOrgan(string userID, string companyID, string departmentID, string postID)
        {
            try
            {
                using (tmpmEntities2 context = new tmpmEntities2())
                {
                    int userType = (int)context.ctms_sys_userinfo.FirstOrDefault(p => p.USERID == userID).USERTYPE;
                    switch (userType)
                    {
                        case ((int)UserType.患者):
                            hr_cnr_user cnUser = context.hr_cnr_user.First(p => p.userid == userID);
                            cnUser.ownercompanyid = companyID;
                            cnUser.ownerdepartmentid = departmentID;
                            cnUser.ownerpostid = postID;
                            context.Entry(cnUser).State = EntityState.Modified;
                            break;
                        case ((int)UserType.平台医生):
                        case ((int)UserType.肿瘤专家):
                            ctms_sup_doctor doctor = context.ctms_sup_doctor.First(p => p.userid == userID);
                            doctor.ownercompanyid = companyID;
                            doctor.ownerdepartmentid = departmentID;
                            doctor.ownerpostid = postID;
                            context.Entry(doctor).State = EntityState.Modified;
                            break;
                        default:
                            break;
                    }

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Model与Entity互转
        // <summary>
        // Model转Entity
        // </summary>
        // <param name="model"></param>
        // <returns></returns>
        private ctms_hr_userpost ModelToEntity(HRUserPost model)
        {
            if (model != null)
            {
                var entity = new ctms_hr_userpost()
                {
                    EMPLOYEEPOSTID = model.EmployeepostID,
                    ISAGENCY = model.IsAgency,
                    USERID = model.UserID,
                    USERNAME = model.UserName,
                    POSTID = model.PostID,
                    POSTLEVEL = model.PostLevel,
                    CHECKSTATE = model.CheckState,
                    EDITSTATE = model.EditState,
                    CREATEUSERID = model.CreateUserID,
                    CREATEDATE = model.CreateDate,
                    UPDATEUSERID = model.UpdateUserID,
                    UPDATEDATE = model.UpdateDate,
                    DEPARTMENTID = model.DepartmentID,
                    COMPANYID = model.CompanyID,
                    POSTNAME = model.PostName,
                    DEPARTMENTNAME = model.DepartmentName,
                    CNAME = model.CName
                };

                return entity;
            }
            return null;
        }

        // <summary>
        // Entity转Model
        // </summary>
        // <param name="entity"></param>
        // <returns></returns>
        private HRUserPost EntityToModel(ctms_hr_userpost entity)
        {
            if (entity != null)
            {
                var model = new HRUserPost()
                {
                    EmployeepostID = entity.EMPLOYEEPOSTID,
                    IsAgency = entity.ISAGENCY,
                    UserID = entity.USERID,
                    UserName = entity.USERNAME,
                    PostID = entity.POSTID,
                    PostLevel = (int)entity.POSTLEVEL,
                    CheckState = entity.CHECKSTATE,
                    EditState = entity.EDITSTATE,
                    CreateUserID = entity.CREATEUSERID,
                    CreateDate = entity.CREATEDATE,
                    UpdateUserID = entity.UPDATEUSERID,
                    UpdateDate = entity.UPDATEDATE,
                    DepartmentID = entity.DEPARTMENTID,
                    CompanyID = entity.COMPANYID,
                    PostName = entity.POSTNAME,
                    DepartmentName = entity.DEPARTMENTNAME,
                    CName = entity.CNAME
                };

                return model;
            }
            return null;
        }
        #endregion
    }
}
