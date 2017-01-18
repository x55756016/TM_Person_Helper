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
    public class ctms_sys_userinfoDAL : BaseDAL<ctms_sys_userinfo>
    {
    	/// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Add(ctms_sys_userinfo entity)
        {
            base.Insert(entity);
            return entity.USERID;
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Edit(ctms_sys_userinfo entity)
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
        public IQueryable<ctms_sys_userinfo> Get()
        {
            return base.FindAll();
        }
        
        /// <summary>
        /// 单条数据
        /// </summary>
        /// <returns></returns>
        public ctms_sys_userinfo Get(Expression<Func<ctms_sys_userinfo, bool>> predicate = null)
        {
            return base.FindOne(predicate);
        }



        public ctms_sys_userinfo GetUserInfoByLoginName(string loginName)
        {
            ctms_sys_userinfo userInfo = base.FindOne(p => p.LOGINNAME == loginName);
            return userInfo;
        }

        public bool AddUserInfo(ctms_sys_userinfo user)
        {
            bool result = base.Insert(user);
            return result;
        }

        public ctms_sys_userinfo GetUserInfoByEmail(string email)
        {
            ctms_sys_userinfo userInfo = base.FindOne(p => p.EMAIL == email);
            return userInfo;
        }

        public ctms_sys_userinfo GetUserInfoByMobilePhone(string mobilePhone)
        {
            ctms_sys_userinfo userInfo = base.FindOne(p => p.MOBILEPHONE == mobilePhone);
            return userInfo;
        }


        public ctms_sys_userinfo GetUserInfoByID(string id)
        {
            ctms_sys_userinfo userInfo = base.FindOne(p => p.USERID == id);
            return userInfo;
        }


        public bool UpdateUserInfo(ctms_sys_userinfo user)
        {
            return base.Update(user);
        }
    }
}