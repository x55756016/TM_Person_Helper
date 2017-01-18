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
    public class ctms_hr_userpostDAL : BaseDAL<ctms_hr_userpost>
    {
    	/// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(ctms_hr_userpost entity)
        {
            base.Insert(entity);
            return entity.EMPLOYEEPOSTID;
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(ctms_hr_userpost entity)
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
        public IQueryable<ctms_hr_userpost> Get()
        {
            return base.FindAll();
        }
        
        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public ctms_hr_userpost Get(Expression<Func<ctms_hr_userpost, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}