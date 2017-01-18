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
    public class ctms_pm_dotaskBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(V_ctms_pm_dotask model)
        {
             if (model == null)
                return string.Empty;
                
  			using(ctms_pm_dotaskDAL dal = new ctms_pm_dotaskDAL()){              
            ctms_pm_dotask entity = ModelToEntity(model);
            entity.TASKID = string.IsNullOrEmpty(model.TASKID) ? Guid.NewGuid().ToString("N") : model.TASKID;

            return dal.Add(entity);
            }
        }
		
		/// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public V_ctms_pm_dotask Get(Expression<Func<ctms_pm_dotask, bool>> predicate = null)
        {
        	using(ctms_pm_dotaskDAL dal = new ctms_pm_dotaskDAL()){        
	            ctms_pm_dotask entity= dal.Get(predicate);
	            return EntityToModel(entity); 
            }
        }
		
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ctms_pm_dotask> Get()
        {
        	using(ctms_pm_dotaskDAL dal = new ctms_pm_dotaskDAL()){        
	            List<ctms_pm_dotask> entitys = dal.Get().ToList();
	            List<V_ctms_pm_dotask> list = new List<V_ctms_pm_dotask>();
	            foreach (ctms_pm_dotask item in entitys)
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
        public IEnumerable<V_ctms_pm_dotask> GetList(PageInfo page)
        {
        	using(ctms_pm_dotaskDAL dal = new ctms_pm_dotaskDAL()){        
	            var list = dal.Get();
	
	            return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(V_ctms_pm_dotask model)
        {
            if (model == null) return false;
            using(ctms_pm_dotaskDAL dal = new ctms_pm_dotaskDAL()){        
	            ctms_pm_dotask entitys = ModelToEntity(model);
	            
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
			using(ctms_pm_dotaskDAL dal = new ctms_pm_dotaskDAL()){        
           		return dal.Delete(id);
            }
        }
      
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_pm_dotask ModelToEntity(V_ctms_pm_dotask model)
        {
            if (model != null)
            {
                ctms_pm_dotask entity = new ctms_pm_dotask()
                {
                	                    	TASKID = model.TASKID,
                                        	TITLE = model.TITLE,
                                        	MODELCODE = model.MODELCODE,
                                        	MODELID = model.MODELID,
                                        	ARRIVEDATE = model.ARRIVEDATE,
                                        	EXPIREDTIME = model.EXPIREDTIME,
                                        	ISCLOSED = model.ISCLOSED,
                                        	CLOSEDTIME = model.CLOSEDTIME,
                                        	OWENRID = model.OWENRID,
                                        	OWNERDEPARTMENTID = model.OWNERDEPARTMENTID,
                                        	OWNERCOMPANYID = model.OWNERCOMPANYID,
                                        	CREATETIME = model.CREATETIME,
                                        	CREATEUSER = model.CREATEUSER,
                                        	EDITTIME = model.EDITTIME,
                                        	EDITUSER = model.EDITUSER,
                                        	OWNERPOSTID = model.OWNERPOSTID,
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
        private V_ctms_pm_dotask  EntityToModel(ctms_pm_dotask  entity)
        {
            if (entity != null)
            {
                V_ctms_pm_dotask  model = new V_ctms_pm_dotask ()
                {
                                       	TASKID = entity.TASKID,
                                        	TITLE = entity.TITLE,
                                        	MODELCODE = entity.MODELCODE,
                                        	MODELID = entity.MODELID,
                                        	ARRIVEDATE = entity.ARRIVEDATE,
                                        	EXPIREDTIME = entity.EXPIREDTIME,
                                        	ISCLOSED = entity.ISCLOSED,
                                        	CLOSEDTIME = entity.CLOSEDTIME,
                                        	OWENRID = entity.OWENRID,
                                        	OWNERDEPARTMENTID = entity.OWNERDEPARTMENTID,
                                        	OWNERCOMPANYID = entity.OWNERCOMPANYID,
                                        	CREATETIME = entity.CREATETIME,
                                        	CREATEUSER = entity.CREATEUSER,
                                        	EDITTIME = entity.EDITTIME,
                                        	EDITUSER = entity.EDITUSER,
                                        	OWNERPOSTID = entity.OWNERPOSTID,
                                    };

                return model;
            }

            return null;
        }

       
    }
}
