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
    public class ctms_sys_servermonitorBLL
    {
        public string Add(V_ctms_sys_servermonitor model)
        {
            if (model == null)
                return string.Empty;

            using (ctms_sys_servermonitorDAL dal = new ctms_sys_servermonitorDAL())
            {
                ctms_sys_servermonitor entity = ModelToEntity(model);
                entity.ServerMonitorId = string.IsNullOrEmpty(model.ServerMonitorId) ? Guid.NewGuid().ToString("N") : model.ServerMonitorId;
                return dal.Add(entity);
            }
        }

        public V_ctms_sys_servermonitor Get(Expression<Func<ctms_sys_servermonitor, bool>> predicate = null)
        {
            using (ctms_sys_servermonitorDAL dal = new ctms_sys_servermonitorDAL())
            {
                ctms_sys_servermonitor entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        public IEnumerable<V_ctms_sys_servermonitor> GetAll()
        {
            using (ctms_sys_servermonitorDAL dal = new ctms_sys_servermonitorDAL())
            {
                return dal.GetAll(p => p.IsDeleted == 0).Select(EntityToModel).ToList();
            }
        }

        public IEnumerable<V_ctms_sys_servermonitor> GetList(PageInfo page, string kwd)
        {
            using (ctms_sys_servermonitorDAL dal = new ctms_sys_servermonitorDAL())
            {
                if (string.IsNullOrEmpty(kwd))
                    return dal.GetAll(p => p.IsDeleted == 0).Paging(ref page).Select(EntityToModel).ToList();
                else
                    return dal.GetAll(p => p.IsDeleted == 0 && (p.ServerName.Contains(kwd) || p.IPAddress.Contains(kwd))).Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        public bool Edit(V_ctms_sys_servermonitor model)
        {
            if (model == null)
                return false;

            using (ctms_sys_servermonitorDAL dal = new ctms_sys_servermonitorDAL())
            {
                return dal.Edit(ModelToEntity(model));
            }
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            using (ctms_sys_servermonitorDAL dal = new ctms_sys_servermonitorDAL())
            {
                return dal.Delete(id);
            }
        }

        public ctms_sys_servermonitor ModelToEntity(V_ctms_sys_servermonitor model)
        {
            if (model != null)
            {
                ctms_sys_servermonitor entity = new ctms_sys_servermonitor()
                {
                    ServerMonitorId = model.ServerMonitorId,
                    IPAddress = model.IPAddress,
                    ServerName = model.ServerName,
                    CPUMaxValue = model.CPUMaxValue,
                    MemoryMaxValue = model.MemoryMaxValue,
                    DiskMaxValue = model.DiskMaxValue,
                    ContactUserName = model.ContactUserName,
                    MobilePhone = model.MobilePhone,
                    Email = model.Email,
                    Remark = model.Remark,
                    IsValid = model.IsValid,
                    IsDeleted = model.IsDeleted,
                    InputTime = model.InputTime,
                    CreateUserLoginName = model.CreateUserLoginName,
                    CreateUserName = model.CreateUserName,
                    ModifyTime = model.ModifyTime,
                    ModifyUserLoginName = model.ModifyUserLoginName,
                    ModifyUserName = model.ModifyUserName,
                    SystemId = model.SystemId
                };
                return entity;
            }
            return null;
        }

        public V_ctms_sys_servermonitor EntityToModel(ctms_sys_servermonitor entity)
        {
            if (entity != null)
            {
                V_ctms_sys_servermonitor model = new V_ctms_sys_servermonitor()
                {
                    ServerMonitorId = entity.ServerMonitorId,
                    IPAddress = entity.IPAddress,
                    ServerName = entity.ServerName,
                    CPUMaxValue = entity.CPUMaxValue,
                    MemoryMaxValue = entity.MemoryMaxValue,
                    DiskMaxValue = entity.DiskMaxValue,
                    ContactUserName = entity.ContactUserName,
                    MobilePhone = entity.MobilePhone,
                    Email = entity.Email,
                    Remark = entity.Remark,
                    IsValid = entity.IsValid,
                    IsDeleted = entity.IsDeleted,
                    InputTime = entity.InputTime,
                    CreateUserLoginName = entity.CreateUserLoginName,
                    CreateUserName = entity.CreateUserName,
                    ModifyTime = entity.ModifyTime,
                    ModifyUserLoginName = entity.ModifyUserLoginName,
                    ModifyUserName = entity.ModifyUserName,
                    SystemId = entity.SystemId
                };
                return model;
            }
            return null;
        }
    }
}
