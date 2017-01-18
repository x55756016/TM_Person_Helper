/*
 * 描述:
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2016-11-16                                         创建 
 *  
 */
 
using Project.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.DAL
{
    public class ctms_pm_projectDAL : BaseDAL<ctms_pm_project>
    {
    	/// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(ctms_pm_project entity)
        {
            base.Insert(entity);
            return entity.PROJECTID;
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(ctms_pm_project entity)
        {
            return base.Update(entity);
        }
		
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            return base.DeleteById(id);
        }
		
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<ctms_pm_project> Get()
        {
            return base.FindAll();
        }
        
        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public ctms_pm_project Get(Expression<Func<ctms_pm_project, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}