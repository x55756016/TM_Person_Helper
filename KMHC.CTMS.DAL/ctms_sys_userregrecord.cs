using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class ctms_sys_userregrecordDAL : BaseDAL<ctms_sys_userregrecord>
    {
        public string Add(ctms_sys_userregrecord entity)
        {
            base.Insert(entity);
            return entity.UserRegRecordId;
        }

        public bool Edit(ctms_sys_userregrecord entity)
        {
            return base.Update(entity);
        }

        public bool Delete(string id)
        {
            return base.DeleteById(id);
        }

        public IQueryable<ctms_sys_userregrecord> GetAll()
        {
            return base.FindAll();
        }

        public IQueryable<ctms_sys_userregrecord> GetAll(Expression<Func<ctms_sys_userregrecord, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        public ctms_sys_userregrecord Get(Expression<Func<ctms_sys_userregrecord, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
