using Project.Common;
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
    public class FunctionBLL
    {
        private readonly string logTitle = "访问FunctionBLL类";
        public FunctionBLL()
        {

        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(Function model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new tmpmEntities2())
            {
                model.FunctionID = Guid.NewGuid().ToString();
                db.Set<ctms_sys_function>().Add(ModelToEntity(model));

                db.SaveChanges();
                return model.FunctionID;
            }
        }

        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(Function model)
        {
            if (string.IsNullOrEmpty(model.FunctionID))
            {
                LogHelper.WriteError("试图修改为空的Function实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new tmpmEntities2())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogHelper.WriteError("试图删除为空的Function实体!");
                throw new KeyNotFoundException();
            }
            Function model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取功能
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public Function Get(string id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_function entity = db.Set<ctms_sys_function>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.functionid)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Function> GetList(string keyWord)
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_sys_function> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<ctms_sys_function>().AsNoTracking().Where(o => o.isdeleted == 0 && (o.functionname.Contains(keyWord) || o.functioncode.Contains(keyWord))).ToList();
                }
                else
                {
                    query = db.Set<ctms_sys_function>().AsNoTracking().Where(o => o.isdeleted == 0).ToList();
                }
                List<Function> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Function> GetList(Expression<Func<ctms_sys_function, bool>> predicate = null)
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_sys_function> query = null;

                query = db.Set<ctms_sys_function>().AsNoTracking().Where(predicate).ToList();
                List<Function> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据userID获取有权限的功能点
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="isMenu">是否为菜单</param>
        /// <returns></returns>
        public List<Function> GetAuthorizedList(string userID, bool isMenu = false)
        {
            using (DbContext db = new tmpmEntities2())
            {
                if (string.IsNullOrEmpty(userID))
                    return new List<Function>();

                var RoleIDList = db.Set<ctms_sys_userrole>()
                    .AsNoTracking()
                    .Where(o => o.isdeleted == 0 && o.userid.Equals(userID))
                    .Select(o => o.roleid)
                    .ToList();

                var FunctionIDList = db.Set<ctms_sys_rolefunction>()
                    .AsNoTracking()
                    .Where(o => o.ISDELETED == 0 && RoleIDList.Contains(o.ROLEID))
                    .Select(o => o.FUNCTIONID).ToList();

                return db.Set<ctms_sys_function>().AsNoTracking()
                     .Where(o => isMenu && o.isdeleted == 0 && (o.ispublic == 1 || FunctionIDList.Contains(o.functionid)))
                     .OrderBy(m => m.sort)
                     .Select(EntityToModel)
                     .ToList();
            }
        }


        private ctms_sys_function ModelToEntity(Function model)
        {
            if (model == null) return null;
            return new ctms_sys_function()
            {
                functionid = string.IsNullOrEmpty(model.FunctionID) ? Guid.NewGuid().ToString() : model.FunctionID,
                functionname = model.FunctionName,
                functioncode = model.FunctionCode,
                parentid = model.ParentID,
                status = (int)model.Status,
                ismenu = model.IsMenu ? 1 : 0,
                menuname = (model.IsMenu && model.Menu != null) ? model.Menu.Name : "",
                menucode = (model.IsMenu && model.Menu != null) ? model.Menu.Code : "",
                menuicon = (model.IsMenu && model.Menu != null) ? model.Menu.Icon : "",
                menuurl = (model.IsMenu && model.Menu != null) ? model.Menu.Url : "",
                isexpand = (model.IsMenu && model.Menu != null) ? model.Menu.IsExpand ? 1 : 0 : 0,
                ispublic = model.IsPublic ? 1 : 0,
                helpertitle = model.HelperTitle,
                helperurl = model.HelperUrl,
                remark = model.Remark,
                sort = model.Sort,
                systemcategory = model.SystemCategory,

                createdatetime = model.CreateDateTime,
                createuserid = model.CreateUserID,
                createusername = model.CreateUserName,
                editdatetime = model.EditTime,
                edituserid = model.EditUserID,
                editusername = model.EditUserName,
                ownerid = model.OwnerID,
                ownername = model.OwnerName,
                isdeleted = model.IsDeleted ? 1 : 0
            };
        }

        private Function EntityToModel(ctms_sys_function entity)
        {
            if (entity == null) return null;
            return new Function()
            {
                FunctionID = entity.functionid,
                FunctionName = entity.functionname,
                FunctionCode = entity.functioncode,
                ParentID = entity.parentid,
                Status = (FunctionStatus)entity.status,
                IsMenu = entity.ismenu == 1 ? true : false,
                Menu = entity.ismenu == 1 ? new MenuInfo()
                {
                    ID = entity.functionid,
                    Code = entity.menucode,
                    Icon = entity.menuicon,
                    IsExpand = entity.isexpand == 1 ? true : false,
                    Name = entity.menuname,
                    Url = entity.menuurl,
                    ParentID = entity.parentid,
                    Sort = entity.sort
                } : null,
                IsPublic = entity.ispublic == 1 ? true : false,
                HelperTitle = entity.helpertitle,
                HelperUrl = entity.helperurl,
                Remark = entity.remark,
                Sort = entity.sort,
                SystemCategory = entity.systemcategory,

                CreateDateTime = entity.createdatetime,
                CreateUserID = entity.createuserid,
                CreateUserName = entity.createusername,
                EditTime = entity.editdatetime,
                EditUserID = entity.edituserid,
                EditUserName = entity.editusername,
                OwnerID = entity.ownerid,
                OwnerName = entity.ownername,
                IsDeleted = entity.isdeleted == 1 ? true : false
            };
        }
    }
}
