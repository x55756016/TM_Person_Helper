using Project.Common.Helper;
using Project.DAL;
using Project.DAL.Database;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL
{
    public class ctms_sys_modelsettingBLL
    {
        public string Add(V_ctms_sys_modelsetting model)
        {
            if (model == null)
                return string.Empty;
            using (ctms_sys_modelsettingDAL dal = new ctms_sys_modelsettingDAL())
            {
                ctms_sys_modelsetting entity = ModelToEntity(model);
                entity.ModelSettingId = string.IsNullOrEmpty(entity.ModelSettingId) ? Guid.NewGuid().ToString("N") : model.ModelSettingId;
                entity.ModelSource = entity.ModelSource == null ? 0 : 1;
                return dal.Add(entity);
            }
        }

        public V_ctms_sys_modelsetting Get(Expression<Func<ctms_sys_modelsetting, bool>> predicate = null)
        {
            using (ctms_sys_modelsettingDAL dal = new ctms_sys_modelsettingDAL())
            {
                ctms_sys_modelsetting entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        public List<V_ctms_sys_modelsetting> GetAll()
        {
            using (ctms_sys_modelsettingDAL dal = new ctms_sys_modelsettingDAL())
            {
                return dal.GetAll().Select(EntityToModel).ToList();
            }
        }

        public List<V_ctms_sys_modelsetting> GetList(PageInfo page, int type, string kwd = "")
        {
            using (ctms_sys_modelsettingDAL dal = new ctms_sys_modelsettingDAL())
            {
                if (string.IsNullOrEmpty(kwd))
                    return dal.GetAll(p=>p.ModelSource == type).Paging(ref page).Select(EntityToModel).ToList();
                else
                    return dal.GetAll(p => (p.ModelName.Contains(kwd) || p.ModelCode.Contains(kwd)) && p.ModelSource == type).Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        public bool Edit(V_ctms_sys_modelsetting model)
        {
            if (model == null)
                return false;
            using (ctms_sys_modelsettingDAL dal = new ctms_sys_modelsettingDAL())
            {
                return dal.Edit(ModelToEntity(model));
            }
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            using (ctms_sys_modelsettingDAL dal = new ctms_sys_modelsettingDAL())
            {
                return dal.Delete(id);
            }
        }

        public ctms_sys_modelsetting ModelToEntity(V_ctms_sys_modelsetting model)
        {
            if (model != null)
            {
                ctms_sys_modelsetting entity = new ctms_sys_modelsetting()
                {
                    ModelSettingId = model.ModelSettingId,
                    ModelName = model.ModelName,
                    ModelCode = model.ModelCode,
                    Remark = model.Remark,
                    IsValid = model.IsValid,
                    IsDeleted = model.IsDeleted,
                    CreateUserAccount = model.CreateUserAccount,
                    CreateDateTime = model.CreateDateTime,
                    LastModifyDateTime = model.LastModifyDateTime,
                    LastModifyUserAccount = model.LastModifyUserAccount,
                    ModelSource = model.ModelSource,
                    SystemId = model.SystemId
                };
                return entity;
            }
            return null;
        }

        public V_ctms_sys_modelsetting EntityToModel(ctms_sys_modelsetting entity)
        {
            if (entity != null)
            {
                V_ctms_sys_modelsetting model = new V_ctms_sys_modelsetting()
                {
                    ModelSettingId = entity.ModelSettingId,
                    ModelName = entity.ModelName,
                    ModelCode = entity.ModelCode,
                    Remark = entity.Remark,
                    IsValid = entity.IsValid,
                    IsDeleted = entity.IsDeleted,
                    CreateUserAccount = entity.CreateUserAccount,
                    CreateDateTime = entity.CreateDateTime,
                    LastModifyDateTime = entity.LastModifyDateTime,
                    LastModifyUserAccount = entity.LastModifyUserAccount,
                    ModelSource = entity.ModelSource,
                    SystemId = entity.SystemId
                };
                return model;
            }

            return null;
        }
    }
}
