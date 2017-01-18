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
    public class HRPostBLL
    {
        /// <summary>
        /// 根据条件获取部门
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public HRPost GetOne(Expression<Func<ctms_hr_post, bool>> predicate = null)
        {
            using (HRPostDAL dal = new HRPostDAL())
            {
                return EntityToModel(dal.FindOne(predicate));
            }
        }

        /// <summary>
        /// 根据条件分页获取部门信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<HRPost> GetList(ref PageInfo pager, Expression<Func<ctms_hr_post, bool>> predicate = null)
        {
            using (HRPostDAL dal = new HRPostDAL())
            {
                var list = dal.FindAll(predicate);

                return list.Paging(ref pager).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 根据条件获取部门信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<HRPost> GetList(Expression<Func<ctms_hr_post, bool>> predicate = null)
        {
            using (HRPostDAL dal = new HRPostDAL())
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
        public bool Edit(HRPost model)
        {
            if (model == null) return false;
            using (HRPostDAL dal = new HRPostDAL())
            {
                ctms_hr_post entitys = ModelToEntity(model);

                return dal.Update(entitys);
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(HRPost model)
        {
            if (model == null)
                return false;
            using (HRPostDAL dal = new HRPostDAL())
            {
                if (string.IsNullOrEmpty(model.PostID)) model.PostID = Guid.NewGuid().ToString();
                ctms_hr_post entity = ModelToEntity(model);

                return dal.Insert(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(string id)
        {
            using (HRPostDAL dal = new HRPostDAL())
            {
                return dal.DeleteById(id);
            }
        }

        #region Model与Entity互转
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_hr_post ModelToEntity(HRPost model)
        {
            if (model != null)
            {
                var entity = new ctms_hr_post()
                {
                    POSTID = model.PostID,
                    POSTCODE = model.PostCode,
                    POSTNAME = model.PostName,
                    COMPANYID = model.CompanyID,
                    DEPARTMENTID = model.DepartmentID,
                    DEPARTMENTNAME = model.DepartmentName,
                    POSTFUNCTION = model.PostFunction,
                    POSTNUMBER = (int)model.PostNumber,
                    POSTLEVEL = model.PostLevel,
                    POSTCOEFFICIENT = model.PostCoefficient,
                    POSTGOAL = model.PostGoal,
                    FATHERPOSTID = model.FatherPostID,
                    UNDERNUMBER = (int)model.UnderNumber,
                    PROMOTEDIRECTION = model.PromoteDirection,
                    CHANGEPOST = model.ChangePost,
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
        private HRPost EntityToModel(ctms_hr_post entity)
        {
            if (entity != null)
            {
                var model = new HRPost()
                {
                    PostID = entity.POSTID,
                    PostCode = entity.POSTCODE,
                    PostName = entity.POSTNAME,
                    CompanyID = entity.COMPANYID,
                    DepartmentID = entity.DEPARTMENTID,
                    DepartmentName = entity.DEPARTMENTNAME,
                    PostFunction = entity.POSTFUNCTION,
                    PostNumber = entity.POSTNUMBER,
                    PostLevel = (int)entity.POSTLEVEL,
                    PostCoefficient = entity.POSTCOEFFICIENT,
                    PostGoal = entity.POSTGOAL,
                    FatherPostID = entity.FATHERPOSTID,
                    UnderNumber = entity.UNDERNUMBER,
                    PromoteDirection = entity.PROMOTEDIRECTION,
                    ChangePost = entity.CHANGEPOST,
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
