using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class ctms_sys_serveralarmDAL : BaseDAL<ctms_sys_serveralarm>
    {
        public string Add(ctms_sys_serveralarm entity)
        {
            base.Insert(entity);
            return entity.ServerAlarmId;
        }

        public bool Edit(ctms_sys_serveralarm entity)
        {
            return base.Update(entity);
        }

        public bool Delete(string id)
        {
            return base.DeleteById(id);
        }

        public IQueryable<ctms_sys_serveralarm> GetAll()
        {
            return base.FindAll();
        }

        public IQueryable<ctms_sys_serveralarm> GetAll(Expression<Func<ctms_sys_serveralarm, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        public ctms_sys_serveralarm Get(Expression<Func<ctms_sys_serveralarm, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
