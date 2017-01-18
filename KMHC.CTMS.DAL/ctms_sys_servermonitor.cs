using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class ctms_sys_servermonitorDAL : BaseDAL<ctms_sys_servermonitor>
    {
        public string Add(ctms_sys_servermonitor entity)
        {
            base.Insert(entity);
            return entity.ServerMonitorId;
        }

        public bool Edit(ctms_sys_servermonitor entity)
        {
            return base.Update(entity);
        }

        public bool Delete(string id)
        {
            var entity = Get(p => p.ServerMonitorId == id);
            if (entity == null)
                return false;
            entity.IsDeleted = 1;
            return Edit(entity);
        }

        public IQueryable<ctms_sys_servermonitor> GetAll()
        {
            return base.FindAll();
        }

        public IQueryable<ctms_sys_servermonitor> GetAll(Expression<Func<ctms_sys_servermonitor, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        public ctms_sys_servermonitor Get(Expression<Func<ctms_sys_servermonitor, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
