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
    public class ctms_pm_itemconfirmBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(V_ctms_pm_itemconfirm model)
        {
             if (model == null)
                return string.Empty;
                
  			using(ctms_pm_itemconfirmDAL dal = new ctms_pm_itemconfirmDAL()){              
            ctms_pm_itemconfirm entity = ModelToEntity(model);
            entity.COLUMN_17 = string.IsNullOrEmpty(model.COLUMN_17) ? Guid.NewGuid().ToString("N") : model.COLUMN_17;

            return dal.Add(entity);
            }
        }
		
		/// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public V_ctms_pm_itemconfirm Get(Expression<Func<ctms_pm_itemconfirm, bool>> predicate = null)
        {
        	using(ctms_pm_itemconfirmDAL dal = new ctms_pm_itemconfirmDAL()){        
	            ctms_pm_itemconfirm entity= dal.Get(predicate);
	            return EntityToModel(entity); 
            }
        }
		
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ctms_pm_itemconfirm> Get()
        {
        	using(ctms_pm_itemconfirmDAL dal = new ctms_pm_itemconfirmDAL()){        
	            List<ctms_pm_itemconfirm> entitys = dal.Get().ToList();
	            List<V_ctms_pm_itemconfirm> list = new List<V_ctms_pm_itemconfirm>();
	            foreach (ctms_pm_itemconfirm item in entitys)
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
        public IEnumerable<V_ctms_pm_itemconfirm> GetList(PageInfo page)
        {
        	using(ctms_pm_itemconfirmDAL dal = new ctms_pm_itemconfirmDAL()){        
	            var list = dal.Get();
	
	            return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(V_ctms_pm_itemconfirm model)
        {
            if (model == null) return false;
            using(ctms_pm_itemconfirmDAL dal = new ctms_pm_itemconfirmDAL()){        
	            ctms_pm_itemconfirm entitys = ModelToEntity(model);
	            
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
			using(ctms_pm_itemconfirmDAL dal = new ctms_pm_itemconfirmDAL()){        
           		return dal.Delete(id);
            }
        }
      
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_pm_itemconfirm ModelToEntity(V_ctms_pm_itemconfirm model)
        {
            if (model != null)
            {
                ctms_pm_itemconfirm entity = new ctms_pm_itemconfirm()
                {
                	                    	COLUMN_17 = model.COLUMN_17,
                                        	TASKID = model.TASKID,
                                        	RESULTSCORE = model.RESULTSCORE,
                                        	SUGGESTIONS = model.SUGGESTIONS,
                                        	OWENRID = model.OWENRID,
                                        	OWNERDEPARTMENTID = model.OWNERDEPARTMENTID,
                                        	OWNERCOMPANYID = model.OWNERCOMPANYID,
                                        	CREATETIME = model.CREATETIME,
                                        	CREATEUSER = model.CREATEUSER,
                                        	EDITTIME = model.EDITTIME,
                                        	EDITUSER = model.EDITUSER,
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
        private V_ctms_pm_itemconfirm  EntityToModel(ctms_pm_itemconfirm  entity)
        {
            if (entity != null)
            {
                V_ctms_pm_itemconfirm  model = new V_ctms_pm_itemconfirm ()
                {
                                       	COLUMN_17 = entity.COLUMN_17,
                                        	TASKID = entity.TASKID,
                                        	RESULTSCORE = entity.RESULTSCORE,
                                        	SUGGESTIONS = entity.SUGGESTIONS,
                                        	OWENRID = entity.OWENRID,
                                        	OWNERDEPARTMENTID = entity.OWNERDEPARTMENTID,
                                        	OWNERCOMPANYID = entity.OWNERCOMPANYID,
                                        	CREATETIME = entity.CREATETIME,
                                        	CREATEUSER = entity.CREATEUSER,
                                        	EDITTIME = entity.EDITTIME,
                                        	EDITUSER = entity.EDITUSER,
                                    };

                return model;
            }

            return null;
        }

       
    }
}
