using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Authorization
{
    public class RoleFunctionOrgDAL : BaseDAL<ctms_sys_rolefunctionorg>
    {
        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public ctms_sys_rolefunctionorg GetOne(Expression<Func<ctms_sys_rolefunctionorg, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<ctms_sys_rolefunctionorg> Get(Expression<Func<ctms_sys_rolefunctionorg, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(ctms_sys_rolefunctionorg entity)
        {
            return base.Update(entity);
        }
    }
}
