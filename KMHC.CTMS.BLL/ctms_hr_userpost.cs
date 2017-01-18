/*
 * 描述:
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2016-11-16                                         创建 
 *  
 */
 
using Project.Common.Helper;
using Project.DAL;
using Project.DAL.Database;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.BLL
{
    public class ctms_hr_userpostBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(V_ctms_hr_userpost model)
        {
             if (model == null)
                return string.Empty;
                
  			using(ctms_hr_userpostDAL dal = new ctms_hr_userpostDAL()){              
            ctms_hr_userpost entity = ModelToEntity(model);
            entity.EMPLOYEEPOSTID = string.IsNullOrEmpty(model.EMPLOYEEPOSTID) ? Guid.NewGuid().ToString("N") : model.EMPLOYEEPOSTID;

            return dal.Add(entity);
            }
        }
		
		/// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public V_ctms_hr_userpost Get(Expression<Func<ctms_hr_userpost, bool>> predicate = null)
        {
        	using(ctms_hr_userpostDAL dal = new ctms_hr_userpostDAL()){        
	            ctms_hr_userpost entity= dal.Get(predicate);
	            return EntityToModel(entity); 
            }
        }
		
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ctms_hr_userpost> Get()
        {
        	using(ctms_hr_userpostDAL dal = new ctms_hr_userpostDAL()){        
	            List<ctms_hr_userpost> entitys = dal.Get().ToList();
	            List<V_ctms_hr_userpost> list = new List<V_ctms_hr_userpost>();
	            foreach (ctms_hr_userpost item in entitys)
	            {
	                list.Add(EntityToModel(item));
	            }
	            return list;
            }
        }
        
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<V_ctms_hr_userpost> GetList(PageInfo page)
        {
        	using(ctms_hr_userpostDAL dal = new ctms_hr_userpostDAL()){        
	            var list = dal.Get();
	
	            return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(V_ctms_hr_userpost model)
        {
            if (model == null) return false;
            using(ctms_hr_userpostDAL dal = new ctms_hr_userpostDAL()){        
	            ctms_hr_userpost entitys = ModelToEntity(model);
	            
	            return dal.Edit(entitys);
            }
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
			using(ctms_hr_userpostDAL dal = new ctms_hr_userpostDAL()){        
           		return dal.Delete(id);
            }
        }
      
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_hr_userpost ModelToEntity(V_ctms_hr_userpost model)
        {
            if (model != null)
            {
                ctms_hr_userpost entity = new ctms_hr_userpost()
                {
                	                    	EMPLOYEEPOSTID = model.EMPLOYEEPOSTID,
                                        	ISAGENCY = model.ISAGENCY,
                                        	USERID = model.USERID,
                                        	USERNAME = model.USERNAME,
                                        	COMPANYID = model.COMPANYID,
                                        	DEPARTMENTID = model.DEPARTMENTID,
                                        	POSTID = model.POSTID,
                                        	CNAME = model.CNAME,
                                        	DEPARTMENTNAME = model.DEPARTMENTNAME,
                                        	POSTNAME = model.POSTNAME,
                                        	POSTLEVEL = model.POSTLEVEL,
                                        	CHECKSTATE = model.CHECKSTATE,
                                        	EDITSTATE = model.EDITSTATE,
                                        	CREATEUSERID = model.CREATEUSERID,
                                        	CREATEDATE = model.CREATEDATE,
                                        	UPDATEUSERID = model.UPDATEUSERID,
                                        	UPDATEDATE = model.UPDATEDATE,
                                    };

                return entity;
            }
            return null;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private V_ctms_hr_userpost  EntityToModel(ctms_hr_userpost  entity)
        {
            if (entity != null)
            {
                V_ctms_hr_userpost  model = new V_ctms_hr_userpost ()
                {
                                       	EMPLOYEEPOSTID = entity.EMPLOYEEPOSTID,
                                        	ISAGENCY = entity.ISAGENCY,
                                        	USERID = entity.USERID,
                                        	USERNAME = entity.USERNAME,
                                        	COMPANYID = entity.COMPANYID,
                                        	DEPARTMENTID = entity.DEPARTMENTID,
                                        	POSTID = entity.POSTID,
                                        	CNAME = entity.CNAME,
                                        	DEPARTMENTNAME = entity.DEPARTMENTNAME,
                                        	POSTNAME = entity.POSTNAME,
                                        	POSTLEVEL = entity.POSTLEVEL,
                                        	CHECKSTATE = entity.CHECKSTATE,
                                        	EDITSTATE = entity.EDITSTATE,
                                        	CREATEUSERID = entity.CREATEUSERID,
                                        	CREATEDATE = entity.CREATEDATE,
                                        	UPDATEUSERID = entity.UPDATEUSERID,
                                        	UPDATEDATE = entity.UPDATEDATE,
                                    };

                return model;
            }

            return null;
        }

       
    }
}
