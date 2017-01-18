using Project.Common.Helper;
using Project.DAL.Database;
using Project.DAL.OrganizationManage;
using Project.Model.OrganizationManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.OrganizationManage
{
    public class HRDepartmentBLL
    {
        /// <summary>
        /// 根据条件获取科室
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public HRDepartment GetOne(Expression<Func<ctms_hr_department, bool>> predicate = null)
        {
            using (HRDepartmentDAL dal = new HRDepartmentDAL())
            {
                return EntityToModel(dal.FindOne(predicate));
            }
        }

        /// <summary>
        /// 根据条件分页获取科室
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<HRDepartment> GetList(ref PageInfo pager, Expression<Func<ctms_hr_department, bool>> predicate = null)
        {
            using (HRDepartmentDAL dal = new HRDepartmentDAL())
            {
                var list = dal.FindAll(predicate);

                return list.Paging(ref pager).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 根据条件获取科室
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<HRDepartment> GetList(Expression<Func<ctms_hr_department, bool>> predicate = null)
        {
            using (HRDepartmentDAL dal = new HRDepartmentDAL())
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
        public bool Edit(HRDepartment model)
        {
            if (model == null) return false;
            using (HRDepartmentDAL dal = new HRDepartmentDAL())
            {
                ctms_hr_department entitys = ModelToEntity(model);

                return dal.Update(entitys);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(string id)
        {
            using (HRDepartmentDAL dal = new HRDepartmentDAL())
            {
                return dal.DeleteById(id);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(HRDepartment model)
        {
            if (model == null)
                return false;
            using (HRDepartmentDAL dal = new HRDepartmentDAL())
            {
                if (string.IsNullOrEmpty(model.DepartmentID)) model.DepartmentID = Guid.NewGuid().ToString();
                ctms_hr_department entity = ModelToEntity(model);

                return dal.Insert(entity);
            }
        }

        #region Model与Entity互转
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_hr_department ModelToEntity(HRDepartment model)
        {
            if (model != null)
            {
                var entity = new ctms_hr_department()
                {
                    DEPARTMENTID = model.DepartmentID,
                    DEPARTMENTCODE = model.DepartmentCode,
                    DEPARTMENTNAME = model.DepartmentName,
                    DEPARTMENTLEVEL = model.DepartmentLevel,
                    COMPANYID = model.CompanyID,
                    FATHERTYPE = model.FatherType,
                    FATHERID = model.FatherID,
                    DEPARTMENTBOSSHEAD = model.DepartmentBossHead,
                    DEPARTMENTHEADNAME = model.DepartmentHeadName,
                    DEPARTMENTFUNCTION = model.DepartmentFunction,
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
        public HRDepartment EntityToModel(ctms_hr_department entity)
        {
            if (entity != null)
            {
                var model = new HRDepartment()
                {
                    DepartmentID = entity.DEPARTMENTID,
                    DepartmentCode = entity.DEPARTMENTCODE,
                    DepartmentName = entity.DEPARTMENTNAME,
                    DepartmentLevel = entity.DEPARTMENTLEVEL,
                    CompanyID = entity.COMPANYID,
                    FatherType = entity.FATHERTYPE,
                    FatherID = entity.FATHERID,
                    DepartmentBossHead = entity.DEPARTMENTBOSSHEAD,
                    DepartmentHeadName = entity.DEPARTMENTHEADNAME,
                    DepartmentFunction = entity.DEPARTMENTFUNCTION,
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
