using Project.Common;
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
    public class MetaDataBLL
    {
        private readonly string logTitle = "访问MetaDataBLL类";
        public MetaDataBLL()
        {

        }

        /// <summary>
        /// 新增元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(MetaData model)
        {
            if (model == null) return 0;
            using (DbContext db = new tmpmEntities2())
            {
                db.Set<ctms_sys_metadata>().Add(ModelToEntity(model));
                db.SaveChanges();
                return model.ID;
            }
        }

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MetaData model)
        {
            if (model == null || model.ID <= 0)
            {
                LogHelper.WriteError("试图修改为空的MetaData实体!");
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
                LogHelper.WriteError("试图删除为空的MetaData实体!");
                throw new KeyNotFoundException();
            }
            MetaData model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }


        /// <summary>
        /// 根据ID获取元数据
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public MetaData Get(int id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_metadata entity = db.Set<ctms_sys_metadata>().Find(id);
                if (entity == null || entity.Id <= 0) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<MetaData> GetList(string keyWord)
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_sys_metadata> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<ctms_sys_metadata>().AsNoTracking().Where(o => o.IsDeleted == 0 && o.DisplayName.Contains(keyWord)).ToList();
                }
                else
                {
                    query = db.Set<ctms_sys_metadata>().AsNoTracking().Where(o => o.IsDeleted == 0).ToList();
                }
                List<MetaData> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取元数据选择器查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<TreeItem> GetTreeList(string keyWord)
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_sys_metadata> query = null;
                if (!string.IsNullOrEmpty(keyWord))
                {
                    query = db.Set<ctms_sys_metadata>().AsNoTracking().Where(o => o.IsDeleted == 0 && o.DisplayName.Contains(keyWord)).ToList();
                }
                else
                {
                    query = db.Set<ctms_sys_metadata>().AsNoTracking().Where(o => o.IsDeleted == 0).ToList();
                }
                List<TreeItem> treeList = new List<TreeItem>();
                int forderID = -1;
                foreach (ctms_sys_metadata entity in query)
                {
                    var find = treeList.Find(o => o.text.Equals(entity.Category ?? ""));
                    if (find == null)
                    {
                        find = new TreeItem() { text = entity.Category ?? "", value = forderID--, nodes = new List<TreeItem>() };
                        treeList.Add(find);
                    }
                    find.nodes.Add(new TreeItem() { text = entity.DisplayName, value = entity.Id });
                }
                return treeList;
            }
        }

        /// <summary>
        /// 获取元数据的值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetValueOfMetaDataByID(int id, List<string> paramNames, List<string> paramValues)
        {
            MetaData data = Get(id);
            if (data == null) return string.Empty;
            return GetValueOfMetaData(data, paramNames, paramValues);
        }

        /// <summary>
        /// 获取元数据的值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetValueOfMetaData(MetaData data, List<string> paramNames, List<string> paramValues)
        {
            using (DbContext db = new tmpmEntities2())
            {
                if (data == null)
                {
                    return string.Empty;
                }
                string retVal = null;
                switch (data.DataSourceType)
                {
                    case DataSourceType.Table:
                        retVal = db.Database.SqlQuery<string>(string.Format("select {0} from {1} where 1=1 {2}", data.DataSourceColumn, data.DataSource, GetParamsOfMetaData(data, paramNames, paramValues))).FirstOrDefault();
                        break;
                    case DataSourceType.Func:
                        retVal = db.Database.SqlQuery<string>(string.Format("select {0}({1}) from dual", data.DataSource, GetParamsOfMetaData(data, paramNames, paramValues))).FirstOrDefault();
                        break;
                    case DataSourceType.StoreProcess:
                        retVal = "";//Todo
                        break;
                    default:
                        break;
                }
                return retVal ?? string.Empty;
            }
        }

        /// <summary>
        /// 获取元数据参数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetParamsOfMetaData(MetaData data, List<string> paramNames, List<string> paramValues)
        {
            if (data == null)
            {
                return string.Empty;
            }
            string retVal = string.Empty;
            List<MetaDataParam> paras = new MetaDataParamBLL().GetList(data.ID);
            if (paras == null || paras.Count == 0) return retVal;
            switch (data.DataSourceType)
            {
                case DataSourceType.Table:
                    foreach (MetaDataParam para in paras)
                    {
                        retVal += string.Format(" and {0}={1}", para.ParamName, para.ParamValue);
                    }
                    break;
                case DataSourceType.Func:
                    foreach (MetaDataParam para in paras)
                    {
                        retVal += (!string.IsNullOrEmpty(retVal) ? "," : "") + para.ParamValue;
                    };
                    break;
                case DataSourceType.StoreProcess:
                    retVal = "";//Todo
                    break;
                default:
                    break;
            }
            //替换参数
            for (int i = 0; i < paramNames.Count; i++)
            {
                retVal = retVal.Replace(string.Format("@{0}@", paramNames[i]), paramValues[i]);
            }
            return retVal;
        }


        private ctms_sys_metadata ModelToEntity(MetaData model)
        {
            if (model == null) return null;
            return new ctms_sys_metadata()
            {
                Category = model.Category,
                DataSource = model.DataSource,
                DataSourceColumn = model.DataSourceColumn,
                DataSourceType = (int)model.DataSourceType,
                DataType = (int)model.DataType,
                DisplayName = model.DisplayName,

                CreateDateTime = model.CreateDateTime,
                CreateUserId = model.CreateUserID,
                CreateUserName = model.CreateUserName,
                EditDateTime = model.EditTime,
                EditUserId = model.EditUserID,
                EditUserName = model.EditUserName,
                OwnerId = model.OwnerID,
                OwnerName = model.OwnerName,
                IsDeleted = model.IsDeleted ? 1 : 0
            };
        }

        private MetaData EntityToModel(ctms_sys_metadata entity)
        {
            if (entity == null) return null;
            return new MetaData()
            {
                ID = entity.Id,
                Category = entity.Category,
                DataSource = entity.DataSource,
                DataSourceColumn = entity.DataSourceColumn,
                DataSourceType = (DataSourceType)entity.DataSourceType,
                DataType = (DataType)entity.DataType,
                DisplayName = entity.DisplayName,

                CreateDateTime = entity.CreateDateTime,
                CreateUserID = entity.CreateUserId,
                CreateUserName = entity.CreateUserName,
                EditTime = entity.EditDateTime,
                EditUserID = entity.EditUserId,
                EditUserName = entity.EditUserName,
                OwnerID = entity.OwnerId,
                OwnerName = entity.OwnerName,
                IsDeleted = entity.IsDeleted == 1 ? true : false
            };
        }
    }
}
