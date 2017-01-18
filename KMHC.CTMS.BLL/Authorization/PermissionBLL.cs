using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model.Authorization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Authorization
{
    public class PermissionBLL
    {
        private readonly string logTitle = "访问PermissionBLL类";
        public PermissionBLL()
        {

        }

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(Permission model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new tmpmEntities2())
            {
                model.PermissionID = Guid.NewGuid().ToString();
                db.Set<ctms_sys_permission>().Add(ModelToEntity(model));

                db.SaveChanges();
                return model.PermissionID;
            }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(Permission model)
        {
            if (string.IsNullOrEmpty(model.PermissionID))
            {
                LogHelper.WriteError("试图修改为空的Permission实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new tmpmEntities2())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogHelper.WriteError("试图删除为空的Permission实体!");
                throw new KeyNotFoundException();
            }
            Permission model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取权限
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public Permission Get(string id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_permission entity = db.Set<ctms_sys_permission>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.PERMISSIONID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Permission> GetList()
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_sys_permission> query = db.Set<ctms_sys_permission>().AsNoTracking().Where(o => o.ISDELETED == 0).ToList();
                List<Permission> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取指定权限值的权限列表
        /// </summary>
        /// <param name="permissionValue">加总后的权限值</param>
        /// <returns></returns>
        public List<Permission> GetListByValue(int permissionValue)
        {
            if (permissionValue <= 0) return new List<Permission>();
            using (DbContext db = new tmpmEntities2())
            {
                Char[] permissionArray = Convert.ToString(permissionValue, 2).ToCharArray();
                Array.Reverse(permissionArray);
                List<int> permissionValueList = new List<int>();
                for (int i = 0; i < permissionArray.Length; i++)
                {
                    if (permissionArray[i] == '0') continue;
                    permissionValueList.Add((int)Math.Pow(2, i));
                }
                IEnumerable<ctms_sys_permission> query = db.Set<ctms_sys_permission>().AsNoTracking().Where(o => o.ISDELETED == 0 && permissionValueList.Contains((int)o.PERMISSIONVALUE)).ToList();
                List<Permission> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }


        private ctms_sys_permission ModelToEntity(Permission model)
        {
            if (model == null) return null;
            return new ctms_sys_permission()
            {
                PERMISSIONID = string.IsNullOrEmpty(model.PermissionID) ? Guid.NewGuid().ToString() : model.PermissionID,
                PERMISSIONNAME = model.PermissionName,
                PERMISSIONCODE = model.PermissionCode,
                PERMISSIONVALUE = model.PermissionValue,
                REMARK = model.Remark,

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

        private Permission EntityToModel(ctms_sys_permission entity)
        {
            if (entity == null) return null;
            return new Permission()
            {
                PermissionID = entity.PERMISSIONID,
                PermissionName = entity.PERMISSIONNAME,
                PermissionCode = entity.PERMISSIONCODE,
                PermissionValue = (int)entity.PERMISSIONVALUE,
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
