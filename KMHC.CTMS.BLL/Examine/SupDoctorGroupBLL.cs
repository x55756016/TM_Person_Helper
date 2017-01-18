using Project.BLL.OrganizationManage;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
using Project.Model.Examine;
using Project.Model.OrganizationManage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project.BLL.Examine
{
    public class SupDoctorGroupBLL
    {
        private readonly string logTitle = "访问SupDoctorGroupBLL类";

        V_ctms_sys_userinfo user = null;
        public SupDoctorGroupBLL()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
            user = new ctms_sys_userinfoBLL().GetLoginInfo(cookie.Value);
        }

        /// <summary>
        /// 新增医生协作组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(SupDoctorGroup model)
        {
            using (DbContext db = new tmpmEntities2())
            {
                if (string.IsNullOrEmpty(model.ID)) model.ID = Guid.NewGuid().ToString();
                model.CreateDateTime = DateTime.Now;
                model.CreateUserID = user.USERID;
                model.CreateUserName = user.USERNAME;
                model.OwnerID = user.USERID;
                model.OwnerName = user.USERNAME;
                db.Set<ctms_sup_doctorgroup>().Add(ModelToEntity(model));
                if (!string.IsNullOrEmpty(model.GroupAdminID))
                {
                    db.Set<ctms_sup_doctorgroupdetail>().Add(new ctms_sup_doctorgroupdetail()
                    {
                        createdatetime = DateTime.Now,
                        createuserid = user.USERID,
                        createusername = user.USERNAME,
                        doctorid = model.GroupAdminID,
                        docname = model.GroupAdminName,
                        groupdetailid = Guid.NewGuid().ToString(),
                        groupid = model.ID,
                        groupname = model.GroupName,
                        memberlevel = 9,
                    });
                }
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改医生协作组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(SupDoctorGroup model)
        {
            using (DbContext db = new tmpmEntities2())
            {
                model.EditTime = DateTime.Now;
                model.EditUserID = user.USERID;
                model.EditUserName = user.USERNAME;
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                var admin = db.Set<ctms_sup_doctorgroupdetail>().FirstOrDefault(o => o.isdeleted == 0 && o.groupid.Equals(model.ID) && o.memberlevel == 9);
                if (admin != null && !admin.doctorid.Equals(model.GroupAdminID))
                {
                    if (string.IsNullOrEmpty(model.GroupAdminID))
                    {
                        admin.isdeleted = 1;
                    }
                    else
                    {
                        admin.doctorid = model.GroupAdminID;
                        admin.docname = model.GroupAdminName;
                    }
                    db.Entry(admin).State = EntityState.Modified;
                }
                else if (admin == null && !string.IsNullOrEmpty(model.GroupAdminID))
                {
                    db.Set<ctms_sup_doctorgroupdetail>().Add(new ctms_sup_doctorgroupdetail()
                    {
                        createdatetime = DateTime.Now,
                        createuserid = user.USERID,
                        createusername = user.USERNAME,
                        doctorid = model.GroupAdminID,
                        docname = model.GroupAdminName,
                        groupdetailid = Guid.NewGuid().ToString(),
                        groupid = model.ID,
                        groupname = model.GroupName,
                        memberlevel = 9,
                    });
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除医生协作组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sup_doctorgroup entity = db.Set<ctms_sup_doctorgroup>().Find(id);
                if (entity != null)
                {
                    entity.isdeleted = 1;
                    db.Entry(entity).State = EntityState.Modified;
                    //db.Set<CTMS_SUP_DOCTORGROUP>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// 根据ID获取医生协作组
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public SupDoctorGroup Get(string id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sup_doctorgroup entity = db.Set<ctms_sup_doctorgroup>().Find(id);
                if (entity == null) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 根据ID获取医生协作组
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<SupDoctorGroup> GetList(string userID, string keyword, ref PageInfo pager)
        {
            using (DbContext db = new tmpmEntities2())
            {
                List<string> myGroupIDList = db.Set<ctms_sup_doctorgroupdetail>().Where(o => o.isdeleted == 0 && o.doctorid.Equals(userID)).Select(o => o.groupid).ToList();
                if (string.IsNullOrEmpty(keyword))
                {
                    return db.Set<ctms_sup_doctorgroup>().Where(o => o.isdeleted == 0 && (o.createuserid.Equals(userID) || o.groupadminid.Equals(userID) || myGroupIDList.Contains(o.groupid))).Paging(ref pager).Select(EntityToModel).ToList();
                }
                else
                {
                    return db.Set<ctms_sup_doctorgroup>().Where(o => o.isdeleted == 0 && (o.createuserid.Equals(userID) || o.groupadminid.Equals(userID) || myGroupIDList.Contains(o.groupid)) && o.groupname.Contains(keyword)).Paging(ref pager).Select(EntityToModel).ToList();
                }
            }
        }

        public List<SupDoctorGroup> GetList(ref PageInfo pager, Expression<Func<ctms_sup_doctorgroup, bool>> predicate = null)
        {
            using (DbContext db = new tmpmEntities2())
            {
                var list = db.Set<ctms_sup_doctorgroup>().Where(predicate);
                return list.Paging(ref pager).Select(EntityToModel).ToList();
            }
        }

        public List<SupDoctorPatientDto> GetPatientList(string doctorID, ref PageInfo pager, string keyword)
        {
            using (DbContext db = new tmpmEntities2())
            {
                List<string> doctorIDList = new List<string>();
                var groupQuery = db.Set<ctms_sup_doctorgroupdetail>().Where(o => o.isdeleted == 0 && o.doctorid.Equals(doctorID) && o.memberlevel >= 8);
                if (groupQuery.Count() > 0)
                {
                    foreach (string groupID in groupQuery.Select(o => o.groupid).ToList())
                    {
                        List<string> groupDoctorIDList = db.Set<ctms_sup_doctorgroupdetail>().Where(o => o.isdeleted == 0 && o.groupid.Equals(groupID)).Select(o => o.doctorid).ToList();
                        doctorIDList.AddRange(groupDoctorIDList);
                    }
                }
                else
                {
                    doctorIDList.Add(doctorID);
                }
                if (string.IsNullOrEmpty(keyword))
                {
                    var temList = (from d in db.Set<ctms_sup_doctorpatient>()
                                   join u in db.Set<ctms_sys_userinfo>() on d.fansuserid equals u.USERID
                                   join cnr in db.Set<hr_cnr_user>() on d.fansuserid equals cnr.userid
                                   join hrArea in db.Set<hr_area>() on cnr.province equals hrArea.areaid into leftPro
                                   from m in leftPro.DefaultIfEmpty()
                                   join hrArea2 in db.Set<hr_area>() on cnr.city equals hrArea2.areaid into leftCity
                                   from m2 in leftCity.DefaultIfEmpty()
                                   where d.isdeleted == 0 && doctorIDList.Contains(d.userid) && d.isdoctorpatientrelated.Equals("1")
                                   select new SupDoctorPatientDto()
                                   {
                                       USERID = d.fansuserid,
                                       UserName = cnr.username,
                                       Age = cnr.birthdate.HasValue ? (DateTime.Now.Year - cnr.birthdate.Value.Year) : new Nullable<int>(),
                                       PHONENUM = u.MOBILEPHONE,
                                       Sex = cnr.sex,
                                       SexName = string.IsNullOrEmpty(cnr.sex) ? "" : (cnr.sex == "1" ? "男" : "女"),
                                       CityName = m2.areaname,
                                       ProvinceName = m.areaname,
                                       DiseaseName = cnr.disease,
                                       CurrentStep = cnr.currentstep
                                   }).Distinct();
                    pager.Total = temList.Count();
                    return temList.OrderBy(o => o.UserName).Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
                }
                else
                {
                    var temList = (from d in db.Set<ctms_sup_doctorpatient>()
                                   join u in db.Set<ctms_sys_userinfo>() on d.fansuserid equals u.USERID
                                   join cnr in db.Set<hr_cnr_user>() on d.fansuserid equals cnr.userid
                                   join hrArea in db.Set<hr_area>() on cnr.province equals hrArea.areaid into leftPro
                                   from m in leftPro.DefaultIfEmpty()
                                   join hrArea2 in db.Set<hr_area>() on cnr.city equals hrArea2.areaid into leftCity
                                   from m2 in leftCity.DefaultIfEmpty()
                                   where d.isdeleted == 0 && doctorIDList.Contains(d.userid) && (cnr.username.Equals(keyword) || u.MOBILEPHONE.Equals(keyword))
                                   select new SupDoctorPatientDto()
                                   {
                                       UserName = cnr.username,
                                       Age = cnr.birthdate.HasValue ? (DateTime.Now.Year - cnr.birthdate.Value.Year) : new Nullable<int>(),
                                       PHONENUM = u.MOBILEPHONE,
                                       Sex = cnr.sex,
                                       CityName = m2.areaname,
                                       ProvinceName = m.areaname,
                                       DiseaseName = cnr.disease,
                                       CurrentStep = cnr.currentstep
                                   }).Distinct();
                    pager.Total = temList.Count();
                    return temList.OrderBy(o => o.UserName).Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
                }
            }
        }



        public ctms_sup_doctorgroup ModelToEntity(SupDoctorGroup model)
        {
            if (model == null) return null;
            ctms_sup_doctorgroup entity = new ctms_sup_doctorgroup()
            {
                groupid = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID,
                groupname = model.GroupName,
                groupicon = model.GroupIcon,
                groupdescription = model.GroupDescription,
                groupadminid = model.GroupAdminID,
                groupadminname = model.GroupAdminName,
                ownercompanyid = model.OwnerCompanyID,
                ownerdepartmentid = model.OwnerDepartID,
                ownerpostid = model.OwnerPostID,
            };
            return entity;
        }

        public SupDoctorGroup EntityToModel(ctms_sup_doctorgroup entity)
        {
            if (entity == null) return null;
            string domainUrl = (ConfigurationManager.AppSettings["DomainUrl"] + "").TrimEnd('/');
            SupDoctorGroup model = new SupDoctorGroup()
            {
                ID = entity.groupid,
                GroupName = entity.groupname,
                GroupIcon = string.IsNullOrEmpty(entity.groupicon) ? entity.groupicon : domainUrl + entity.groupicon,
                GroupDescription = entity.groupdescription,
                GroupAdminID = entity.groupadminid,
                GroupAdminName = entity.groupadminname,
            };
            if (!string.IsNullOrEmpty(entity.ownercompanyid))
            {
                HRCompany com = new HRCompanyBLL().GetOne(o => o.COMPANYID.Equals(entity.ownercompanyid));
                if (com != null)
                {
                    model.OwnerCompanyName = com.CName;
                }
            }
            return model;
        }
    }
}
