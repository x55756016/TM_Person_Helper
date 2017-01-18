using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class ctms_sys_errorcountDAL : BaseDAL<ctms_sys_errorcount>
    {
        public string Add(ctms_sys_errorcount entity)
        {
            base.Insert(entity);
            return entity.ErrorId;
        }

        public bool Edit(ctms_sys_errorcount entity)
        {
            return base.Update(entity);
        }

        public bool Delete(string id)
        {
            return base.DeleteById(id);
        }

        public IQueryable<ctms_sys_errorcount> GetAll()
        {
            return base.FindAll();
        }

        public IQueryable<ctms_sys_errorcount> GetAll(Expression<Func<ctms_sys_errorcount, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        public ctms_sys_errorcount Get(Expression<Func<ctms_sys_errorcount, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
