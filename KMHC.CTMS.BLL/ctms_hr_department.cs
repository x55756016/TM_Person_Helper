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
    public class ctms_hr_departmentBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(V_ctms_hr_department model)
        {
             if (model == null)
                return string.Empty;
                
  			using(ctms_hr_departmentDAL dal = new ctms_hr_departmentDAL()){              
            ctms_hr_department entity = ModelToEntity(model);
            entity.DEPARTMENTID = string.IsNullOrEmpty(model.DEPARTMENTID) ? Guid.NewGuid().ToString("N") : model.DEPARTMENTID;

            return dal.Add(entity);
            }
        }
		
		/// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public V_ctms_hr_department Get(Expression<Func<ctms_hr_department, bool>> predicate = null)
        {
        	using(ctms_hr_departmentDAL dal = new ctms_hr_departmentDAL()){        
	            ctms_hr_department entity= dal.Get(predicate);
	            return EntityToModel(entity); 
            }
        }
		
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ctms_hr_department> Get()
        {
        	using(ctms_hr_departmentDAL dal = new ctms_hr_departmentDAL()){        
	            List<ctms_hr_department> entitys = dal.Get().ToList();
	            List<V_ctms_hr_department> list = new List<V_ctms_hr_department>();
	            foreach (ctms_hr_department item in entitys)
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
        public IEnumerable<V_ctms_hr_department> GetList(PageInfo page)
        {
        	using(ctms_hr_departmentDAL dal = new ctms_hr_departmentDAL()){        
	            var list = dal.Get();
	
	            return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(V_ctms_hr_department model)
        {
            if (model == null) return false;
            using(ctms_hr_departmentDAL dal = new ctms_hr_departmentDAL()){        
	            ctms_hr_department entitys = ModelToEntity(model);
	            
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
			using(ctms_hr_departmentDAL dal = new ctms_hr_departmentDAL()){        
           		return dal.Delete(id);
            }
        }
      
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_hr_department ModelToEntity(V_ctms_hr_department model)
        {
            if (model != null)
            {
                ctms_hr_department entity = new ctms_hr_department()
                {
                	                    	DEPARTMENTID = model.DEPARTMENTID,
                                        	DEPARTMENTCODE = model.DEPARTMENTCODE,
                                        	DEPARTMENTNAME = model.DEPARTMENTNAME,
                                        	DEPARTMENTLEVEL = model.DEPARTMENTLEVEL,
                                        	COMPANYID = model.COMPANYID,
                                        	FATHERTYPE = model.FATHERTYPE,
                                        	FATHERID = model.FATHERID,
                                        	DEPARTMENTBOSSHEAD = model.DEPARTMENTBOSSHEAD,
                                        	DEPARTMENTHEADNAME = model.DEPARTMENTHEADNAME,
                                        	DEPARTMENTFUNCTION = model.DEPARTMENTFUNCTION,
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
        private V_ctms_hr_department  EntityToModel(ctms_hr_department  entity)
        {
            if (entity != null)
            {
                V_ctms_hr_department  model = new V_ctms_hr_department ()
                {
                                       	DEPARTMENTID = entity.DEPARTMENTID,
                                        	DEPARTMENTCODE = entity.DEPARTMENTCODE,
                                        	DEPARTMENTNAME = entity.DEPARTMENTNAME,
                                        	DEPARTMENTLEVEL = entity.DEPARTMENTLEVEL,
                                        	COMPANYID = entity.COMPANYID,
                                        	FATHERTYPE = entity.FATHERTYPE,
                                        	FATHERID = entity.FATHERID,
                                        	DEPARTMENTBOSSHEAD = entity.DEPARTMENTBOSSHEAD,
                                        	DEPARTMENTHEADNAME = entity.DEPARTMENTHEADNAME,
                                        	DEPARTMENTFUNCTION = entity.DEPARTMENTFUNCTION,
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
