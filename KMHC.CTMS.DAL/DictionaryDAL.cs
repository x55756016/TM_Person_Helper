using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class DictionaryDAL : BaseDAL<hr_dictionary>
    {
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(hr_dictionary entity)
        {
            base.Insert(entity);
            return entity.dictionaryid;
        }

        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public hr_dictionary GetOne(Expression<Func<hr_dictionary, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<hr_dictionary> Get(Expression<Func<hr_dictionary, bool>> predicate = null)
        {
            return base.FindAll(predicate);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(hr_dictionary entity)
        {
            return base.Update(entity);
        }
    }
}
