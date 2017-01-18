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
    public class ctms_sys_serveralarmBLL
    {
        public string Add(V_ctms_sys_serveralarm model)
        {
            if (model == null)
                return string.Empty;

            using (ctms_sys_serveralarmDAL dal = new ctms_sys_serveralarmDAL())
            {
                ctms_sys_serveralarm entity = ModelToEntity(model);
                entity.ServerAlarmId = string.IsNullOrEmpty(model.ServerAlarmId) ? Guid.NewGuid().ToString("N") : model.ServerAlarmId;
                return dal.Add(entity);
            }
        }

        public V_ctms_sys_serveralarm Get(Expression<Func<ctms_sys_serveralarm, bool>> predicate = null)
        {
            using (ctms_sys_serveralarmDAL dal = new ctms_sys_serveralarmDAL())
            {
                ctms_sys_serveralarm entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        public IEnumerable<V_ctms_sys_serveralarm> GetAll()
        {
            using (ctms_sys_serveralarmDAL dal = new ctms_sys_serveralarmDAL())
            {
                return dal.GetAll().Select(EntityToModel).ToList();
            }
        }

        public IEnumerable<V_ctms_sys_serveralarm> GetAll(Expression<Func<ctms_sys_serveralarm, bool>> predicate = null)
        {
            using (ctms_sys_serveralarmDAL dal = new ctms_sys_serveralarmDAL())
            {
                return dal.GetAll(predicate).Select(EntityToModel).ToList();
            }
        }

        public List<V_ctms_sys_serveralarm> GetList(PageInfo page, string ip,int? type = null)
        {
            using (ctms_sys_serveralarmDAL dal = new ctms_sys_serveralarmDAL())
            {
                var list = dal.GetAll();
                if (!string.IsNullOrEmpty(ip))
                    list = list.Where(p => p.IPAddress == ip);
                if (type != null)
                    list = list.Where(p => p.Type == type);
                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        public bool Edit(V_ctms_sys_serveralarm model)
        {
            if (model == null)
                return false;

            using (ctms_sys_serveralarmDAL dal = new ctms_sys_serveralarmDAL())
            {
                return dal.Edit(ModelToEntity(model));
            }
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            using (ctms_sys_serveralarmDAL dal = new ctms_sys_serveralarmDAL())
            {
                return dal.Delete(id);
            }
        }

        public ctms_sys_serveralarm ModelToEntity(V_ctms_sys_serveralarm model)
        {
            if (model != null)
            {
                ctms_sys_serveralarm entity = new ctms_sys_serveralarm()
                {
                    ServerAlarmId = model.ServerAlarmId,
                    InputTime = model.InputTime,
                    IPAddress = model.IPAddress,
                    Message = model.Message,
                    Status = model.Status,
                    Type = model.Type,
                    SystemId = model.SystemId
                };
                return entity;
            }
            return null;
        }

        public V_ctms_sys_serveralarm EntityToModel(ctms_sys_serveralarm entity)
        {
            if (entity != null)
            {
                V_ctms_sys_serveralarm model = new V_ctms_sys_serveralarm()
                {
                    ServerAlarmId = entity.ServerAlarmId,
                    InputTime = entity.InputTime,
                    IPAddress = entity.IPAddress,
                    Message = entity.Message,
                    Status = entity.Status,
                    Type = entity.Type,
                    SystemId = entity.SystemId
                };
                return model;
            }
            return null;
        }
    }
}
