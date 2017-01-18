using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class ctms_sys_serverinfoDAL : BaseDAL<ctms_sys_serverinfo>
    {
        public string Add(ctms_sys_serverinfo entity)
        {
            base.Insert(entity);
            return entity.ServerInfoId;
        }

        public bool Edit(ctms_sys_serverinfo entity)
        {
            return base.Update(entity);
        }

        public bool Delete(string id)
        {
            return base.DeleteById(id);
        }

        public IQueryable<ctms_sys_serverinfo> GetAll()
        {
            return base.FindAll();
        }

        public IQueryable<ctms_sys_serverinfo> GetAll(Expression<Func<ctms_sys_serverinfo, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        public ctms_sys_serverinfo Get(Expression<Func<ctms_sys_serverinfo, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
