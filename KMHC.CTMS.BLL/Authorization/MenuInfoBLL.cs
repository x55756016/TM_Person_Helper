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
    public class MenuInfoBLL
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(MenuInfo model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new tmpmEntities2())
            {
                model.ID = Guid.NewGuid().ToString();
                db.Set<ctms_sys_function>().Add(ModelToEntity(model));

                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MenuInfo model)
        {
            if (string.IsNullOrEmpty(model.ID))
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
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MenuInfo> GetList(string userID)
        {
            using (DbContext db = new tmpmEntities2())
            {
                if (string.IsNullOrEmpty(userID))
                    return db.Set<ctms_sys_function>().AsNoTracking()
                    .Where(o => o.ismenu == 1 && o.isdeleted == 0 && string.IsNullOrEmpty(o.parentid) && o.ispublic == 1)
                    .OrderBy(m => m.sort).ToList()
                    .Select(m => EntityToModel(m))
                    .ToList();

                var RoleIDList = db.Set<ctms_sys_userrole>()
                    .AsNoTracking()
                    .Where(o => o.isdeleted == 0 && o.userid.Equals(userID))
                    .Select(o => o.roleid)
                    .ToList();

                var FunctionIDList = db.Set<ctms_sys_rolefunction>()
                    .AsNoTracking()
                    .Where(o => o.ISDELETED == 0 && RoleIDList.Contains(o.ROLEID))
                    .Select(o => o.FUNCTIONID).ToList();

                var ParentFunctionIDList = db.Set<ctms_sys_function>()
                    .AsNoTracking()
                    .Where(o => o.ismenu == 1 && o.isdeleted == 0 && !string.IsNullOrEmpty(o.parentid) && FunctionIDList.Contains(o.functionid))
                    .Select(o => o.parentid).ToList();

                var query = db.Set<ctms_sys_function>().AsNoTracking()
                    .Where(o => o.ismenu == 1 && o.isdeleted == 0 && (o.ispublic == 1 || FunctionIDList.Contains(o.functionid) || ParentFunctionIDList.Contains(o.functionid)))
                    .OrderBy(m => m.sort)
                    .ToList();

                List<MenuInfo> list = new List<MenuInfo>();
                foreach (ctms_sys_function entity in query.Where(o => string.IsNullOrEmpty(o.parentid)))
                {
                    MenuInfo model = EntityToModel(entity);
                    model.ChildrenList = query.Where(o => o.parentid != null && o.parentid.Equals(entity.functionid)).Select(EntityToModel).ToList();
                    list.Add(model);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MenuInfo> GetList()
        {
            using (DbContext db = new tmpmEntities2())
            {
                var query = db.Set<ctms_sys_function>().AsNoTracking()
                     .Where(o => o.ismenu == 1 && o.isdeleted == 0 && string.IsNullOrEmpty(o.parentid))
                     .OrderBy(m => m.sort).ToList();

                List<MenuInfo> list = new List<MenuInfo>();
                foreach (ctms_sys_function entity in query)
                {
                    MenuInfo model = EntityToModel(entity);
                    model.ChildrenList = GetChildrenList(model.ID);
                    list.Add(model);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MenuInfo> GetList(Expression<Func<ctms_sys_function, bool>> predicate)
        {
            using (DbContext db = new tmpmEntities2())
            {
                var query = db.Set<ctms_sys_function>().AsNoTracking()
                     .Where(predicate);

                List<MenuInfo> list = query.Select(EntityToModel).ToList();

                return list;
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public MenuInfo GetOne(string ID)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_function entity = db.Set<ctms_sys_function>().Find(ID);
                if (entity == null || string.IsNullOrEmpty(entity.functionid)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取菜单选择器查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MenuInfo> GetChildrenList(string patherID)
        {
            using (DbContext db = new tmpmEntities2())
            {
                //Todo 权限过滤
                var query = db.Set<ctms_sys_function>().AsNoTracking().Where(o => o.ismenu == 1 && o.isdeleted == 0 && o.parentid.Equals(patherID)).OrderBy(m => m.sort).ToList();
                return query.Select(m => EntityToModel(m)).ToList();
            }
        }

        private MenuInfo EntityToModel(ctms_sys_function entity)
        {
            if (entity == null) return null;
            return new MenuInfo()
            {
                ID = entity.functionid,
                Name = entity.menuname,
                Code = entity.menucode,
                Icon = entity.menuicon,
                ParentID = entity.parentid,
                IsExpand = entity.isexpand == 1 ? true : false,
                Url = entity.menuurl,
                Sort = entity.sort,
                IsMenu = entity.ismenu == 1 ? true : false
            };
        }

        private ctms_sys_function ModelToEntity(MenuInfo model)
        {
            if (model == null) return null;
            return new ctms_sys_function()
            {
                functionid = model.ID,
                functioncode = model.Code,
                functionname = model.Name,
                menuname = model.Name,
                menucode = model.Code,
                menuicon = model.Icon,
                parentid = model.ParentID,
                isexpand = model.IsExpand ? 1 : 0,
                menuurl = model.Url,
                ismenu = model.IsMenu ? 1 : 0,
                sort = model.Sort
            };
        }
    }
}
