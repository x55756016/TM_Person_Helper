using Project.DAL;
using Project.DAL.Database;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL
{
    public class ctms_sys_userregrecordBLL
    {
        public string Add(V_ctms_sys_userregrecord model)
        {
            if (model == null)
                return string.Empty;

            using (ctms_sys_userregrecordDAL dal = new ctms_sys_userregrecordDAL())
            {
                ctms_sys_userregrecord entity = ModelToEntity(model);
                entity.UserRegRecordId = string.IsNullOrEmpty(model.UserRegRecordId) ? Guid.NewGuid().ToString("N") : model.UserRegRecordId;
                return dal.Add(entity);
            }
        }

        public V_ctms_sys_userregrecord Get(Expression<Func<ctms_sys_userregrecord, bool>> predicate = null)
        {
            using (ctms_sys_userregrecordDAL dal = new ctms_sys_userregrecordDAL())
            {
                ctms_sys_userregrecord entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        public IEnumerable<V_ctms_sys_userregrecord> GetAll()
        {
            using (ctms_sys_userregrecordDAL dal = new ctms_sys_userregrecordDAL())
            {
                return dal.GetAll().Select(EntityToModel).ToList();
            }
        }

        public IEnumerable<V_ctms_sys_userregrecord> GetAll(Expression<Func<ctms_sys_userregrecord, bool>> predicate = null)
        {
            using (ctms_sys_userregrecordDAL dal = new ctms_sys_userregrecordDAL())
            {
                return dal.GetAll(predicate).Select(EntityToModel).ToList();
            }
        }

        public bool Edit(V_ctms_sys_userregrecord model)
        {
            if (model == null)
                return false;

            using (ctms_sys_userregrecordDAL dal = new ctms_sys_userregrecordDAL())
            {
                return dal.Edit(ModelToEntity(model));
            }
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            using (ctms_sys_userregrecordDAL dal = new ctms_sys_userregrecordDAL())
            {
                return dal.Delete(id);
            }
        }

        public ctms_sys_userregrecord ModelToEntity(V_ctms_sys_userregrecord model)
        {
            if (model != null)
            {
                ctms_sys_userregrecord entity = new ctms_sys_userregrecord()
                {
                    UserId = model.UserId,
                    UserName = model.UserName,
                    Sex = model.Sex,
                    Age = model.Age,
                    LoginName = model.LoginName,
                    MobilePhone = model.MobilePhone,
                    Address = model.Address,
                    UserSource = model.UserSource,
                    InputTime = model.InputTime,
                    SystemId = model.SystemId
                };
                return entity;
            }
            return null;
        }

        public V_ctms_sys_userregrecord EntityToModel(ctms_sys_userregrecord entity)
        {
            if (entity != null)
            {
                V_ctms_sys_userregrecord model = new V_ctms_sys_userregrecord()
                {
                    UserId = entity.UserId,
                    UserName = entity.UserName,
                    Sex = entity.Sex,
                    Age = entity.Age,
                    LoginName = entity.LoginName,
                    MobilePhone = entity.MobilePhone,
                    Address = entity.Address,
                    UserSource = entity.UserSource,
                    InputTime = entity.InputTime,
                    SystemId = entity.SystemId
                };
                return model;
            }
            return null;
        }
    }
}
