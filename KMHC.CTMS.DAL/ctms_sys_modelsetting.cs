using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class ctms_sys_modelsettingDAL : BaseDAL<ctms_sys_modelsetting>
    {
        public string Add(ctms_sys_modelsetting entity)
        {
            base.Insert(entity);
            return entity.ModelSettingId;
        }

        public bool Edit(ctms_sys_modelsetting entity)
        {
            return base.Update(entity);
        }

        public bool Delete(string id)
        {
            var model = base.FindOne(p => p.ModelSettingId == id);
            if (model != null)
            {
                model.IsDeleted = 1;
                return Edit(model);
            }
            return false;
        }

        public IQueryable<ctms_sys_modelsetting> GetAll()
        {
            return base.FindAll(p=>p.IsDeleted == 0);
        }

        public IQueryable<ctms_sys_modelsetting> GetAll(Expression<Func<ctms_sys_modelsetting, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        public ctms_sys_modelsetting Get(Expression<Func<ctms_sys_modelsetting, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}
