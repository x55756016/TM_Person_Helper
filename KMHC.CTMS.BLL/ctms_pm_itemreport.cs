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
    public class ctms_pm_itemreportBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(V_ctms_pm_itemreport model)
        {
             if (model == null)
                return string.Empty;
                
  			using(ctms_pm_itemreportDAL dal = new ctms_pm_itemreportDAL()){              
            ctms_pm_itemreport entity = ModelToEntity(model);
            entity.TASKID = string.IsNullOrEmpty(model.TASKID) ? Guid.NewGuid().ToString("N") : model.TASKID;

            return dal.Add(entity);
            }
        }
		
		/// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public V_ctms_pm_itemreport Get(Expression<Func<ctms_pm_itemreport, bool>> predicate = null)
        {
        	using(ctms_pm_itemreportDAL dal = new ctms_pm_itemreportDAL()){        
	            ctms_pm_itemreport entity= dal.Get(predicate);
	            return EntityToModel(entity); 
            }
        }
		
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ctms_pm_itemreport> Get()
        {
        	using(ctms_pm_itemreportDAL dal = new ctms_pm_itemreportDAL()){        
	            List<ctms_pm_itemreport> entitys = dal.Get().ToList();
	            List<V_ctms_pm_itemreport> list = new List<V_ctms_pm_itemreport>();
	            foreach (ctms_pm_itemreport item in entitys)
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
        public IEnumerable<V_ctms_pm_itemreport> GetList(PageInfo page)
        {
        	using(ctms_pm_itemreportDAL dal = new ctms_pm_itemreportDAL()){        
	            var list = dal.Get();
	
	            return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(V_ctms_pm_itemreport model)
        {
            if (model == null) return false;
            using(ctms_pm_itemreportDAL dal = new ctms_pm_itemreportDAL()){        
	            ctms_pm_itemreport entitys = ModelToEntity(model);
	            
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
			using(ctms_pm_itemreportDAL dal = new ctms_pm_itemreportDAL()){        
           		return dal.Delete(id);
            }
        }
      
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_pm_itemreport ModelToEntity(V_ctms_pm_itemreport model)
        {
            if (model != null)
            {
                ctms_pm_itemreport entity = new ctms_pm_itemreport()
                {
                	                    	TASKID = model.TASKID,
                                        	PRETASK = model.PRETASK,
                                        	SUBMITDATE = model.SUBMITDATE,
                                        	SUBMITPROGRESS = model.SUBMITPROGRESS,
                                        	SUBMITREMARK = model.SUBMITREMARK,
                                        	PMANAGER = model.PMANAGER,
                                        	ISCOMFIRM = model.ISCOMFIRM,
                                        	COMFIRMRMARK = model.COMFIRMRMARK,
                                        	COMFIRMDATE = model.COMFIRMDATE,
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
        private V_ctms_pm_itemreport  EntityToModel(ctms_pm_itemreport  entity)
        {
            if (entity != null)
            {
                V_ctms_pm_itemreport  model = new V_ctms_pm_itemreport ()
                {
                                       	TASKID = entity.TASKID,
                                        	PRETASK = entity.PRETASK,
                                        	SUBMITDATE = entity.SUBMITDATE,
                                        	SUBMITPROGRESS = entity.SUBMITPROGRESS,
                                        	SUBMITREMARK = entity.SUBMITREMARK,
                                        	PMANAGER = entity.PMANAGER,
                                        	ISCOMFIRM = entity.ISCOMFIRM,
                                        	COMFIRMRMARK = entity.COMFIRMRMARK,
                                        	COMFIRMDATE = entity.COMFIRMDATE,
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
