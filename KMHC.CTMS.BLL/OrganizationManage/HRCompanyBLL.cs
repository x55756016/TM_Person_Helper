using Project.Common.Helper;
using Project.DAL.Database;
using Project.DAL.OrganizationManage;
using Project.Model.OrganizationManage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Project.Common.Helper;

namespace Project.BLL.OrganizationManage
{
    public class HRCompanyBLL
    {
        /// <summary>
        /// 根据ID获取公司
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public HRCompany GetOne(Expression<Func<ctms_hr_company, bool>> predicate = null)
        {
            using (HRCompanyDAL dal = new HRCompanyDAL())
            {
                return EntityToModel(dal.FindOne(predicate));
            }
        }

        /// <summary>
        /// 根据条件分页获取公司信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<HRCompany> GetList(ref PageInfo pager, Expression<Func<ctms_hr_company, bool>> predicate = null)
        {
            using (HRCompanyDAL dal = new HRCompanyDAL())
            {
                var list = dal.FindAll(predicate);

                return list.Paging(ref pager).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 根据条件获取公司信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<HRCompany> GetList(ArrayList args, string filterString = "", Expression<Func<ctms_hr_company, bool>> predicate = null)
        {
            using (HRCompanyDAL dal = new HRCompanyDAL())
            {
                var list = dal.FindAll(predicate);
                if (!string.IsNullOrEmpty(filterString) && args != null)
                    list = list.where(filterString, args.ToArray());
                return list.Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 根据条件获取公司信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<HRCompany> GetList(Expression<Func<ctms_hr_company, bool>> predicate = null)
        {
            using (HRCompanyDAL dal = new HRCompanyDAL())
            {
                var list = dal.FindAll(predicate);

                return list.Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(HRCompany model)
        {
            if (model == null) return false;
            using (HRCompanyDAL dal = new HRCompanyDAL())
            {
                ctms_hr_company entitys = ModelToEntity(model);

                return dal.Update(entitys);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Del(string id)
        {
            using (HRCompanyDAL dal = new HRCompanyDAL())
            {
                return dal.DeleteById(id);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(HRCompany model)
        {
            if (model == null)
                return false;
            using (HRCompanyDAL dal = new HRCompanyDAL())
            {
                if (string.IsNullOrEmpty(model.CompanyID)) model.CompanyID = Guid.NewGuid().ToString();
                ctms_hr_company entity = ModelToEntity(model);

                return dal.Insert(entity);
            }
        }

        #region Model与Entity互转
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_hr_company ModelToEntity(HRCompany model)
        {
            if (model != null)
            {
                var entity = new ctms_hr_company()
                {
                    COMPANYID = model.CompanyID,
                    COMPANYTYPE = model.CompanyType,
                    COMPANRYCODE = model.CompanryCode,
                    ENAME = model.EName,
                    CNAME = model.CName,
                    COMPANYCATEGORY = model.CompanyCategory,
                    CITY = model.City,
                    COUNTYTYPE = model.CountyType,
                    COMPANYLEVEL = model.CompanyLevel,
                    FATHERCOMPANYID = model.FatherCompanyID,
                    FATHERTYPE = model.FatherType,
                    ADDRESS = model.Address,
                    LEGALPERSON = model.LegalPerson,
                    LINKMAN = model.LinkMan,
                    TELNUMBER = model.TelNumber,
                    LEGALPERSONID = model.LegalPersonID,
                    BUSSINESSLICENCENO = model.BussinessLicenceNo,
                    BUSSINESSAREA = model.BussinessArea,
                    ACCOUNTCODE = model.AccountCode,
                    BANKID = model.BankID,
                    EMAIL = model.Email,
                    ZIPCODE = model.ZipCode,
                    FAXNUMBER = model.FaxNumber,
                    CHECKSTATE = model.CheckState,
                    EDITSTATE = model.EditState,
                    CREATEUSERID = model.CreateUserID,
                    CREATEDATE = model.CreateDate,
                    UPDATEUSERID = model.UpdateUserID,
                    UPDATEDATE = model.UpdateDate,
                    OWNERID = model.OwnerID,
                    OWNERPOSTID = model.OwnerPostID,
                    OWNERDEPARTMENTID = model.OwnerDepartmentID,
                    OWNERCOMPANYID = model.OwnerCompanyID
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
        private HRCompany EntityToModel(ctms_hr_company entity)
        {
            if (entity != null)
            {
                var model = new HRCompany()
                {
                    CompanyID = entity.COMPANYID,
                    CompanyType = entity.COMPANYTYPE,
                    CompanryCode = entity.COMPANRYCODE,
                    EName = entity.ENAME,
                    CName = entity.CNAME,
                    CompanyCategory = entity.COMPANYCATEGORY,
                    City = entity.CITY,
                    CountyType = entity.COUNTYTYPE,
                    CompanyLevel = entity.COMPANYLEVEL,
                    FatherCompanyID = entity.FATHERCOMPANYID,
                    FatherType = entity.FATHERTYPE,
                    Address = entity.ADDRESS,
                    LegalPerson = entity.LEGALPERSON,
                    LinkMan = entity.LINKMAN,
                    TelNumber = entity.TELNUMBER,
                    LegalPersonID = entity.LEGALPERSONID,
                    BussinessLicenceNo = entity.BUSSINESSLICENCENO,
                    BussinessArea = entity.BUSSINESSAREA,
                    AccountCode = entity.ACCOUNTCODE,
                    BankID = entity.BANKID,
                    Email = entity.EMAIL,
                    ZipCode = entity.ZIPCODE,
                    FaxNumber = entity.FAXNUMBER,
                    CheckState = entity.CHECKSTATE,
                    EditState = entity.EDITSTATE,
                    CreateUserID = entity.CREATEUSERID,
                    CreateDate = entity.CREATEDATE,
                    UpdateUserID = entity.UPDATEUSERID,
                    UpdateDate = entity.UPDATEDATE,
                    OwnerID = entity.OWNERID,
                    OwnerPostID = entity.OWNERPOSTID,
                    OwnerDepartmentID = entity.OWNERDEPARTMENTID,
                    OwnerCompanyID = entity.OWNERCOMPANYID
                };

                return model;
            }
            return null;
        }
        #endregion
    }
}
