using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model.Authorization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Authorization
{
    public class RoleBLL
    {
        private readonly string logTitle = "访问RoleBLL类";
        public RoleBLL()
        {

        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(Role model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new tmpmEntities2())
            {
                model.RoleID = Guid.NewGuid().ToString();
                db.Set<ctms_sys_role>().Add(ModelToEntity(model));

                db.SaveChanges();
                return model.RoleID;
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(Role model)
        {
            if (string.IsNullOrEmpty(model.RoleID))
            {
                LogHelper.WriteError("试图修改为空的Role实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new tmpmEntities2())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogHelper.WriteError("试图删除为空的Role实体!");
                throw new KeyNotFoundException();
            }
            Role model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public Role Get(string id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_role entity = db.Set<ctms_sys_role>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.ROLEID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Role> GetList(string keyWord)
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_sys_role> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<ctms_sys_role>().AsNoTracking().Where(o => o.ISDELETED == 0 && (o.ROLENAME.Contains(keyWord))).ToList();
                }
                else
                {
                    query = db.Set<ctms_sys_role>().AsNoTracking().Where(o => o.ISDELETED == 0).ToList();
                }
                List<Role> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<Role> GetPageData(PageInfo page, Expression<Func<ctms_sys_role, bool>> predicate = null)
        {
            using (DbContext db = new tmpmEntities2())
            {
                IQueryable<ctms_sys_role> query = null;
                query = db.Set<ctms_sys_role>().AsNoTracking().Where(predicate);

                List<Role> list = query.Paging(ref page).Select(EntityToModel).ToList();
                return list;
            }
        }

        private ctms_sys_role ModelToEntity(Role model)
        {
            if (model == null) return null;
            return new ctms_sys_role()
            {
                ROLEID = string.IsNullOrEmpty(model.RoleID) ? Guid.NewGuid().ToString() : model.RoleID,
                ROLENAME = model.RoleName,
                REMARK = model.Remark,
                SYSTEMCATEGORY = model.SystemCategory,

                CREATEDATETIME = model.CreateDateTime,
                CREATEUSERID = model.CreateUserID,
                CREATEUSERNAME = model.CreateUserName,
                EDITDATETIME = model.EditTime,
                EDITUSERID = model.EditUserID,
                EDITUSERNAME = model.EditUserName,
                OWNERID = model.OwnerID,
                OWNERNAME = model.OwnerName,
                ISDELETED = model.IsDeleted ? 1 : 0
            };
        }

        private Role EntityToModel(ctms_sys_role entity)
        {
            if (entity == null) return null;
            return new Role()
            {
                RoleID = entity.ROLEID,
                RoleName = entity.ROLENAME,
                SystemCategory = (int)entity.SYSTEMCATEGORY,
                Remark = entity.REMARK,

                CreateDateTime = entity.CREATEDATETIME,
                CreateUserID = entity.CREATEUSERID,
                CreateUserName = entity.CREATEUSERNAME,
                EditTime = entity.EDITDATETIME,
                EditUserID = entity.EDITUSERID,
                EditUserName = entity.EDITUSERNAME,
                OwnerID = entity.OWNERID,
                OwnerName = entity.OWNERNAME,
                IsDeleted = entity.ISDELETED == 1 ? true : false
            };
        }
    }
}
