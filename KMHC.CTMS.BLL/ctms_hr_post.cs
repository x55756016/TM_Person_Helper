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
    public class ctms_hr_postBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(V_ctms_hr_post model)
        {
             if (model == null)
                return string.Empty;
                
  			using(ctms_hr_postDAL dal = new ctms_hr_postDAL()){              
            ctms_hr_post entity = ModelToEntity(model);
            entity.POSTID = string.IsNullOrEmpty(model.POSTID) ? Guid.NewGuid().ToString("N") : model.POSTID;

            return dal.Add(entity);
            }
        }
		
		/// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public V_ctms_hr_post Get(Expression<Func<ctms_hr_post, bool>> predicate = null)
        {
        	using(ctms_hr_postDAL dal = new ctms_hr_postDAL()){        
	            ctms_hr_post entity= dal.Get(predicate);
	            return EntityToModel(entity); 
            }
        }
		
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ctms_hr_post> Get()
        {
        	using(ctms_hr_postDAL dal = new ctms_hr_postDAL()){        
	            List<ctms_hr_post> entitys = dal.Get().ToList();
	            List<V_ctms_hr_post> list = new List<V_ctms_hr_post>();
	            foreach (ctms_hr_post item in entitys)
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
        public IEnumerable<V_ctms_hr_post> GetList(PageInfo page)
        {
        	using(ctms_hr_postDAL dal = new ctms_hr_postDAL()){        
	            var list = dal.Get();
	
	            return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(V_ctms_hr_post model)
        {
            if (model == null) return false;
            using(ctms_hr_postDAL dal = new ctms_hr_postDAL()){        
	            ctms_hr_post entitys = ModelToEntity(model);
	            
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
			using(ctms_hr_postDAL dal = new ctms_hr_postDAL()){        
           		return dal.Delete(id);
            }
        }
      
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_hr_post ModelToEntity(V_ctms_hr_post model)
        {
            if (model != null)
            {
                ctms_hr_post entity = new ctms_hr_post()
                {
                	                    	POSTID = model.POSTID,
                                        	POSTCODE = model.POSTCODE,
                                        	POSTNAME = model.POSTNAME,
                                        	COMPANYID = model.COMPANYID,
                                        	DEPARTMENTID = model.DEPARTMENTID,
                                        	DEPARTMENTNAME = model.DEPARTMENTNAME,
                                        	POSTFUNCTION = model.POSTFUNCTION,
                                        	POSTNUMBER = model.POSTNUMBER,
                                        	POSTLEVEL = model.POSTLEVEL,
                                        	POSTCOEFFICIENT = model.POSTCOEFFICIENT,
                                        	POSTGOAL = model.POSTGOAL,
                                        	FATHERPOSTID = model.FATHERPOSTID,
                                        	UNDERNUMBER = model.UNDERNUMBER,
                                        	PROMOTEDIRECTION = model.PROMOTEDIRECTION,
                                        	CHANGEPOST = model.CHANGEPOST,
                                        	CHECKSTATE = model.CHECKSTATE,
                                        	EDITSTATE = model.EDITSTATE,
                                        	CREATEUSERID = model.CREATEUSERID,
                                        	CREATEDATE = model.CREATEDATE,
                                        	UPDATEUSERID = model.UPDATEUSERID,
                                        	UPDATEDATE = model.UPDATEDATE,
                                        	OWNERID = model.OWNERID,
                                        	OWNERPOSTID = model.OWNERPOSTID,
                                        	OWNERDEPARTMENTID = model.OWNERDEPARTMENTID,
                                        	OWNERCOMPANYID = model.OWNERCOMPANYID,
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
        private V_ctms_hr_post  EntityToModel(ctms_hr_post  entity)
        {
            if (entity != null)
            {
                V_ctms_hr_post  model = new V_ctms_hr_post ()
                {
                                       	POSTID = entity.POSTID,
                                        	POSTCODE = entity.POSTCODE,
                                        	POSTNAME = entity.POSTNAME,
                                        	COMPANYID = entity.COMPANYID,
                                        	DEPARTMENTID = entity.DEPARTMENTID,
                                        	DEPARTMENTNAME = entity.DEPARTMENTNAME,
                                        	POSTFUNCTION = entity.POSTFUNCTION,
                                        	POSTNUMBER = entity.POSTNUMBER,
                                        	POSTLEVEL = entity.POSTLEVEL,
                                        	POSTCOEFFICIENT = entity.POSTCOEFFICIENT,
                                        	POSTGOAL = entity.POSTGOAL,
                                        	FATHERPOSTID = entity.FATHERPOSTID,
                                        	UNDERNUMBER = entity.UNDERNUMBER,
                                        	PROMOTEDIRECTION = entity.PROMOTEDIRECTION,
                                        	CHANGEPOST = entity.CHANGEPOST,
                                        	CHECKSTATE = entity.CHECKSTATE,
                                        	EDITSTATE = entity.EDITSTATE,
                                        	CREATEUSERID = entity.CREATEUSERID,
                                        	CREATEDATE = entity.CREATEDATE,
                                        	UPDATEUSERID = entity.UPDATEUSERID,
                                        	UPDATEDATE = entity.UPDATEDATE,
                                        	OWNERID = entity.OWNERID,
                                        	OWNERPOSTID = entity.OWNERPOSTID,
                                        	OWNERDEPARTMENTID = entity.OWNERDEPARTMENTID,
                                        	OWNERCOMPANYID = entity.OWNERCOMPANYID,
                                    };

                return model;
            }

            return null;
        }

       
    }
}
