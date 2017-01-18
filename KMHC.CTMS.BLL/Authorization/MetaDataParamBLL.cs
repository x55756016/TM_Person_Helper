using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model.Authorization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Authorization
{
    public class MetaDataParamBLL
    {
        private readonly string logTitle = "访问MetaDataParamBLL类";
        public MetaDataParamBLL()
        {
        }

        /// <summary>
        /// 新增元数据参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(MetaDataParam model)
        {
            if (model == null) return 0;
            using (DbContext db = new tmpmEntities2())
            {
                db.Set<ctms_sys_metadataparam>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MetaDataParam model)
        {
            if (model == null || model.ID <= 0)
            {
                LogHelper.WriteError("试图修改为空的MetaDataParam实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new tmpmEntities2())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            if (id <= 0)
            {
                LogHelper.WriteError("试图删除为空的MetaDataParam实体!");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_metadataparam entity = db.Set<ctms_sys_metadataparam>().Find(id);
                if (entity != null)
                {
                    db.Set<ctms_sys_metadataparam>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 根据ID获取元数据
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public MetaDataParam Get(int id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_metadataparam entity = db.Set<ctms_sys_metadataparam>().Find(id);
                if (entity == null || entity.Id <= 0) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MetaDataParam> GetList(int metaDataID)
        {
            using (DbContext db = new tmpmEntities2())
            {
                var query = db.Set<ctms_sys_metadataparam>().AsNoTracking().Where(o => o.MetaDataId == metaDataID).ToList();
                List<MetaDataParam> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        public ctms_sys_metadataparam ModelToEntity(MetaDataParam model)
        {
            if (model == null) return null;
            return new ctms_sys_metadataparam()
            {
                MetaDataId = model.MetaDataID,
                ParamName = model.ParamName,
                ParamValue = model.ParamValue
            };
        }

        public MetaDataParam EntityToModel(ctms_sys_metadataparam entity)
        {
            if (entity == null) return null;
            return new MetaDataParam()
            {
                ID = entity.Id,
                MetaDataID = entity.MetaDataId.Value,
                ParamName = entity.ParamName,
                ParamValue = entity.ParamValue
            };
        }
    }
}
