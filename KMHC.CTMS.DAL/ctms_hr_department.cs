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
    public class ctms_hr_departmentDAL : BaseDAL<ctms_hr_department>
    {
    	/// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(ctms_hr_department entity)
        {
            base.Insert(entity);
            return entity.DEPARTMENTID;
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(ctms_hr_department entity)
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
        public IQueryable<ctms_hr_department> Get()
        {
            return base.FindAll();
        }
        
        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public ctms_hr_department Get(Expression<Func<ctms_hr_department, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }
    }
}