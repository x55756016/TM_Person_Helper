using Project.Common.Helper;
using Project.DAL;
using Project.DAL.Database;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL
{
    public class ctms_sys_errorcountBLL
    {
        public string Add(V_ctms_sys_errorcount model)
        {
            if (model == null)
                return string.Empty;

            using (ctms_sys_errorcountDAL dal = new ctms_sys_errorcountDAL())
            {
                ctms_sys_errorcount entity = ModelToEntity(model);
                entity.ErrorId = string.IsNullOrEmpty(model.ErrorId) ? Guid.NewGuid().ToString("N") : model.ErrorId;
                entity.Type = model.Type == null ? 0 : model.Type;
                return dal.Add(entity);
            }
        }

        public V_ctms_sys_errorcount Get(Expression<Func<ctms_sys_errorcount, bool>> predicate = null)
        {
            using (ctms_sys_errorcountDAL dal = new ctms_sys_errorcountDAL())
            {
                ctms_sys_errorcount entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        public IEnumerable<V_ctms_sys_errorcount> GetAll(int type = 0)
        {
            using (ctms_sys_errorcountDAL dal = new ctms_sys_errorcountDAL())
            {
                List<ctms_sys_errorcount> entitys = dal.GetAll(p=>p.Type == type).ToList();
                List<V_ctms_sys_errorcount> list = new List<V_ctms_sys_errorcount>();
                foreach (ctms_sys_errorcount item in entitys)
                {
                    list.Add(EntityToModel(item));
                }
                return list;
            }
        }

        public IEnumerable<V_ctms_sys_errorcount> GetList(PageInfo page, string kwd = "", DateTime? dtStart = null, DateTime? dtEnd = null)
        {
            using (DbContext db = new tmpmEntities2())
            {
                var list = (from x in db.Set<ctms_sys_errorcount>()
                            join y in db.Set<ctms_sys_modelsetting>()
                                on x.LocalPath equals y.ModelCode
                            where y.ModelSource == 1
                            select new V_ctms_sys_errorcount
                            {
                                ErrorId = x.ErrorId,
                                Message = x.Message,
                                ErrorName = y.ModelName,
                                Source = x.Source,
                                StackTrace = x.StackTrace,
                                LocalPath = x.LocalPath,
                                InnerException = x.InnerException,
                                Port = x.Port,
                                LoginName = x.LoginName,
                                UserName = x.UserName,
                                InputTime = x.InputTime,
                                Platform = x.Platform,
                                Type = x.Type
                            });
                if (!string.IsNullOrEmpty(kwd))
                    list = list.Where(p => p.ErrorName.Contains(kwd) || p.LocalPath.Contains(kwd));
                if (dtStart != null)
                    list = list.Where(p => p.InputTime >= dtStart);
                if (dtEnd != null)
                    list = list.Where(p => p.InputTime <= dtEnd);

                return list.Paging(ref page).ToList();
            }
        }

        public bool Edit(V_ctms_sys_errorcount model)
        {
            if (model == null)
                return false;
            using (ctms_sys_errorcountDAL dal = new ctms_sys_errorcountDAL())
            {
                ctms_sys_errorcount entity = ModelToEntity(model);
                return dal.Edit(entity);
            }
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            using (ctms_sys_errorcountDAL dal = new ctms_sys_errorcountDAL())
            {
                return dal.Delete(id);
            }
        }

        public ctms_sys_errorcount ModelToEntity(V_ctms_sys_errorcount model)
        {
            if (model != null)
            {
                ctms_sys_errorcount entity = new ctms_sys_errorcount()
                {
                    ErrorId = model.ErrorId,
                    Message = model.Message,
                    Source = model.Source,
                    StackTrace = model.StackTrace,
                    LocalPath = model.LocalPath,
                    InnerException = model.InnerException,
                    Port = model.Port,
                    LoginName = model.LoginName,
                    UserName = model.UserName,
                    InputTime = model.InputTime,
                    Platform = model.Platform,
                    Type = model.Type,
                    SystemId = model.SystemId
                };
                return entity;
            }
            return null;
        }

        public V_ctms_sys_errorcount EntityToModel(ctms_sys_errorcount entity)
        {
            if (entity != null)
            {
                V_ctms_sys_errorcount model = new V_ctms_sys_errorcount()
                {
                    ErrorId = entity.ErrorId,
                    Message = entity.Message,
                    Source = entity.Source,
                    StackTrace = entity.StackTrace,
                    LocalPath = entity.LocalPath,
                    InnerException = entity.InnerException,
                    Port = entity.Port,
                    LoginName = entity.LoginName,
                    UserName = entity.UserName,
                    InputTime = entity.InputTime,
                    Platform = entity.Platform,
                    Type = entity.Type,
                    SystemId = entity.SystemId
                };
                return model;
            }
            return null;
        }
    }
}
