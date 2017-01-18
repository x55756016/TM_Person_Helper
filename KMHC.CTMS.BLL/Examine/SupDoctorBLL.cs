using Project.Common;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.DAL.Examine;
using Project.Model;
using Project.Model.Examine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Examine
{
    public class SupDoctorBLL
    {
        private readonly ctms_sys_userinfoBLL userService = new ctms_sys_userinfoBLL();

        /// <summary>
        /// 根据ID获取肿瘤医生
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public SupDoctor GetOne(Expression<Func<ctms_sup_doctor, bool>> predicate = null)
        {
            using (SupDoctorDAL dal = new SupDoctorDAL())
            {
                return EntityToModel(dal.FindOne(predicate));
            }
        }

        /// <summary>
        /// 搜索同个组织架构的医生
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<SupDoctor> GetList(ref PageInfo pager, string userId, Expression<Func<ctms_sup_doctor, bool>> predicate = null)
        {
            List<decimal?> typeList = new List<decimal?>() { (int)UserType.肿瘤专家, (int)UserType.医学编辑 }; //只返回肿瘤专家  医学编辑
            using (DbContext context = new tmpmEntities2())
            {
                var list = from x in context.Set<ctms_sup_doctor>()
                           //where (from y in context.CTMS_HR_USERPOST
                           //           where (from z in context.CTMS_HR_USERPOST
                           //                  where z.USERID == user.UserId
                           //                  select z.COMPANYID).Contains(y.COMPANYID)
                           //       select y.USERID).Contains(x.DOCTORID)
                           join userinfo in context.Set<ctms_sys_userinfo>() on x.userid equals userinfo.USERID
                           where typeList.Contains(userinfo.USERTYPE)
                           select x;
                if (predicate != null)
                    list = list.Where(predicate);
                var result = list.Paging(ref pager).Select(EntityToModel).ToList();
                //取出患者已经购买的东西
                //var buyProductList =context.Set<CTMS_MYPRODUCT>().Where(p => p.USERID == userId && !p.ISUSED && !p.ISDELETED);

                result.ForEach(p =>
                {
                    var model = context.Set<ctms_sys_userinfo>().FirstOrDefault(k => k.USERID == p.UserID);
                    p.LoginName = model != null ? model.LOGINNAME : "";
                    p.IsAttention = "0";

                    if (context.Set<ctms_sup_doctorpatient>().Any(k => k.userid == p.UserID && k.fansuserid == userId && k.isdeleted == 0 && k.isdoctorpatientrelated == "0"))
                    {
                        p.IsAttention = "1";
                    }

                    var prov = context.Set<hr_area>().FirstOrDefault(a => a.areaid == p.DocProvince);
                    if (prov != null)
                        p.DocProvinceName = prov.areaname;

                    var city = context.Set<hr_area>().FirstOrDefault(a => a.areaid == p.DocCity);
                    if (city != null)
                        p.DocCityName = city.areaname;
                    //p.DoctorServiceList.ForEach(k => k.IsBuy = buyProductList.Any(o => o.PRODUCTID == k.DoctorServiceID) == true ? "1" : "0");
                });
                return result;
            }
        }

        /// <summary>
        /// 获取医生信息  因为包含是否关注， 是否我的医生字段   所以要传入userid字段
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SupDoctor GetOneForPatient(string doctorId, string userId)
        {
            using (DbContext db = new tmpmEntities2())
            {
                var model = EntityToModel((from x in db.Set<ctms_sup_doctor>() where x.doctorid == doctorId select x).FirstOrDefault());

                //取出患者已经购买的东西
                var buyProductList = db.Set<ctms_myproduct>().Where(p => p.userid == userId && p.isused == 0 && p.isdeleted == 0);
                if (model != null)
                {
                    var userinfo = db.Set<ctms_sys_userinfo>().FirstOrDefault(k => k.USERID == model.UserID);
                    model.LoginName = userinfo != null ? userinfo.LOGINNAME : "";
                    model.IsAttention = "0";
                    model.IsMyFollowDoctor = "0";

                    //判断是否关注了该医生
                    if (db.Set<ctms_sup_doctorpatient>().Any(k => k.userid == model.UserID
                                                                  && k.fansuserid == userId
                                                                  && k.isdeleted == 0
                                                                  && k.isdoctorpatientrelated == "0"))
                    {
                        model.IsAttention = "1";
                    }

                    //判断该医生是否是  “我的医生”
                    if (db.Set<ctms_sup_doctorpatient>().Any(p => p.userid == userId && p.fansuserid == doctorId && p.isdeleted == 0 && p.isdoctorpatientrelated == "1"))
                    {
                        model.IsMyFollowDoctor = "1";
                    }
                }
                return model;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(SupDoctor model)
        {
            if (model == null) return false;
            using (SupDoctorDAL dal = new SupDoctorDAL())
            {
                ctms_sup_doctor entitys = ModelToEntity(model);

                return dal.Update(entitys);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(SupDoctor model)
        {
            if (model == null)
                return false;
            using (SupDoctorDAL dal = new SupDoctorDAL())
            {
                ctms_sup_doctor entity = ModelToEntity(model);

                return dal.Insert(entity);
            }
        }

        #region 模型映射
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_sup_doctor ModelToEntity(SupDoctor model)
        {
            if (model != null)
            {
                var entity = new ctms_sup_doctor()
                {
                    doctorid = model.DoctorID,
                    userid = model.UserID,
                    docname = model.DocName,
                    docsex = model.DocSex,
                    brithday = model.BrithDay,
                    idnumber = model.IDNumber,
                    docprovince = model.DocProvince,
                    doccity = model.DocCity,
                    dochospital = model.DocHospital,
                    docdepartment = model.DocDepartment,
                    doctitle = model.DocTitle,
                    docmajor = model.DocMajor,
                    docdisease = model.DocDisease,
                    diseasecode = model.DiseaseCode,
                    docdescrip = model.DocDescrip,
                    checkstatus = model.CheckStatus,
                    createuserid = model.CreateUserID,
                    createusername = model.CreateUserName,
                    createdatetime = model.CreateDateTime,
                    edituserid = model.EditUserID,
                    editusername = model.EditUserName,
                    editdatetime = model.EditTime,
                    ownerid = model.OwnerID,
                    ownername = model.OwnerName,
                    isdeleted = model.IsDeleted ? 1 : 0,
                    onlinestatus = model.OnlineStatus,
                    checkremark = model.CheckRemark,
                    ownerpostid = model.OwnerPostId,
                    ownerdepartmentid = model.OwnerDeptId,
                    ownercompanyid = model.OwnerCompanyId,
                    commoncontact = model.CommonContact,
                };

                return entity;
            }
            return null;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SupDoctor EntityToModel(ctms_sup_doctor entity)
        {
            if (entity != null)
            {
                V_ctms_sys_userinfo user = userService.GetUserInfoByID(entity.doctorid);

                var model = new SupDoctor()
                {
                    DoctorID = entity.doctorid,
                    UserID = entity.userid,
                    DocName = entity.docname,
                    DocSex = entity.docsex == null ? -1 : entity.docsex.Value,
                    BrithDay = entity.brithday,
                    IDNumber = entity.idnumber,
                    DocProvince = entity.docprovince,
                    DocCity = entity.doccity,
                    DocHospital = entity.dochospital,
                    DocDepartment = entity.docdepartment,
                    DocTitle = entity.doctitle,
                    DocMajor = entity.docmajor,
                    DocDisease = entity.docdisease,
                    DiseaseCode = entity.diseasecode,
                    DocDescrip = entity.docdescrip,
                    CheckStatus = entity.checkstatus,
                    CreateUserID = entity.createuserid,
                    CreateUserName = entity.createusername,
                    CreateDateTime = entity.createdatetime,
                    EditUserID = entity.edituserid,
                    EditUserName = entity.editusername,
                    EditTime = entity.editdatetime,
                    OwnerID = entity.ownerid,
                    OwnerName = entity.ownername,
                    IsDeleted = entity.isdeleted == 1 ? true : false,
                    OnlineStatus = entity.onlinestatus,
                    CheckRemark = entity.checkremark,
                    MobilePhone = user == null ? "" : user.MOBILEPHONE,
                    LoginName = user == null ? "" : user.LOGINNAME,
                    IconPath = user == null ? "" : user.ICONPATH,
                    OwnerCompanyId = entity.ownercompanyid,
                    OwnerDeptId = entity.ownerdepartmentid,
                    OwnerPostId = entity.ownerpostid,
                    CommonContact = entity.commoncontact,
                };

                return model;
            }
            return null;
        }

        #endregion
    }
}
