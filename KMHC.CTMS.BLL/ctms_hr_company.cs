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
    public class ctms_hr_companyBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(V_ctms_hr_company model)
        {
             if (model == null)
                return string.Empty;
                
  			using(ctms_hr_companyDAL dal = new ctms_hr_companyDAL()){              
            ctms_hr_company entity = ModelToEntity(model);
            entity.COMPANYID = string.IsNullOrEmpty(model.COMPANYID) ? Guid.NewGuid().ToString("N") : model.COMPANYID;

            return dal.Add(entity);
            }
        }
		
		/// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public V_ctms_hr_company Get(Expression<Func<ctms_hr_company, bool>> predicate = null)
        {
        	using(ctms_hr_companyDAL dal = new ctms_hr_companyDAL()){        
	            ctms_hr_company entity= dal.Get(predicate);
	            return EntityToModel(entity); 
            }
        }
		
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ctms_hr_company> Get()
        {
        	using(ctms_hr_companyDAL dal = new ctms_hr_companyDAL()){        
	            List<ctms_hr_company> entitys = dal.Get().ToList();
	            List<V_ctms_hr_company> list = new List<V_ctms_hr_company>();
	            foreach (ctms_hr_company item in entitys)
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
        public IEnumerable<V_ctms_hr_company> GetList(PageInfo page)
        {
        	using(ctms_hr_companyDAL dal = new ctms_hr_companyDAL()){        
	            var list = dal.Get();
	
	            return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }
		
		/// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(V_ctms_hr_company model)
        {
            if (model == null) return false;
            using(ctms_hr_companyDAL dal = new ctms_hr_companyDAL()){        
	            ctms_hr_company entitys = ModelToEntity(model);
	            
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
			using(ctms_hr_companyDAL dal = new ctms_hr_companyDAL()){        
           		return dal.Delete(id);
            }
        }
      
        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_hr_company ModelToEntity(V_ctms_hr_company model)
        {
            if (model != null)
            {
                ctms_hr_company entity = new ctms_hr_company()
                {
                	                    	COMPANYID = model.COMPANYID,
                                        	COMPANYTYPE = model.COMPANYTYPE,
                                        	COMPANRYCODE = model.COMPANRYCODE,
                                        	ENAME = model.ENAME,
                                        	CNAME = model.CNAME,
                                        	COMPANYCATEGORY = model.COMPANYCATEGORY,
                                        	CITY = model.CITY,
                                        	COUNTYTYPE = model.COUNTYTYPE,
                                        	COMPANYLEVEL = model.COMPANYLEVEL,
                                        	FATHERCOMPANYID = model.FATHERCOMPANYID,
                                        	FATHERTYPE = model.FATHERTYPE,
                                        	ADDRESS = model.ADDRESS,
                                        	LEGALPERSON = model.LEGALPERSON,
                                        	LINKMAN = model.LINKMAN,
                                        	TELNUMBER = model.TELNUMBER,
                                        	LEGALPERSONID = model.LEGALPERSONID,
                                        	BUSSINESSLICENCENO = model.BUSSINESSLICENCENO,
                                        	BUSSINESSAREA = model.BUSSINESSAREA,
                                        	ACCOUNTCODE = model.ACCOUNTCODE,
                                        	BANKID = model.BANKID,
                                        	EMAIL = model.EMAIL,
                                        	ZIPCODE = model.ZIPCODE,
                                        	FAXNUMBER = model.FAXNUMBER,
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
        private V_ctms_hr_company  EntityToModel(ctms_hr_company  entity)
        {
            if (entity != null)
            {
                V_ctms_hr_company  model = new V_ctms_hr_company ()
                {
                                       	COMPANYID = entity.COMPANYID,
                                        	COMPANYTYPE = entity.COMPANYTYPE,
                                        	COMPANRYCODE = entity.COMPANRYCODE,
                                        	ENAME = entity.ENAME,
                                        	CNAME = entity.CNAME,
                                        	COMPANYCATEGORY = entity.COMPANYCATEGORY,
                                        	CITY = entity.CITY,
                                        	COUNTYTYPE = entity.COUNTYTYPE,
                                        	COMPANYLEVEL = entity.COMPANYLEVEL,
                                        	FATHERCOMPANYID = entity.FATHERCOMPANYID,
                                        	FATHERTYPE = entity.FATHERTYPE,
                                        	ADDRESS = entity.ADDRESS,
                                        	LEGALPERSON = entity.LEGALPERSON,
                                        	LINKMAN = entity.LINKMAN,
                                        	TELNUMBER = entity.TELNUMBER,
                                        	LEGALPERSONID = entity.LEGALPERSONID,
                                        	BUSSINESSLICENCENO = entity.BUSSINESSLICENCENO,
                                        	BUSSINESSAREA = entity.BUSSINESSAREA,
                                        	ACCOUNTCODE = entity.ACCOUNTCODE,
                                        	BANKID = entity.BANKID,
                                        	EMAIL = entity.EMAIL,
                                        	ZIPCODE = entity.ZIPCODE,
                                        	FAXNUMBER = entity.FAXNUMBER,
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
