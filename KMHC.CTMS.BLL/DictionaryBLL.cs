using Project.Common.Helper;
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
    public class DictionaryBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(HrDictionary model)
        {
            if (model == null)
                return string.Empty;

            using (DictionaryDAL dal = new DictionaryDAL())
            {
                hr_dictionary entity = ModelToEntity(model);
                entity.dictionaryid = string.IsNullOrEmpty(model.DictionaryId) ? Guid.NewGuid().ToString() : model.DictionaryId;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public HrDictionary GetOne(Expression<Func<hr_dictionary, bool>> predicate = null)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
            {
                return EntityToModel(dal.GetOne(predicate));
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<HrDictionary> GetList(PageInfo page, Expression<Func<hr_dictionary, bool>> predicate = null)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
            {
                var list = dal.Get(predicate);

                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        public void DeleteDictionary(string dictionaryId)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
            {
                HrDictionary dic = GetOne(p => p.dictionaryid == dictionaryId);
                List<string> list = new List<string>();
                list.Add(dic.DictionaryId);
                Foo(list, dic);
                foreach (string item in list)
                    dal.DeleteById(item);
            }
        }

        private void Foo(List<string> nodeIds, HrDictionary dic)
        {
            foreach (var item in dic.Nodes)
            {
                if (item.Nodes.Count > 0)
                    Foo(nodeIds, item);
                nodeIds.Add(item.DictionaryId);
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<HrDictionary> GetList(Expression<Func<hr_dictionary, bool>> predicate = null)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
            {
                var list = dal.Get(predicate);

                return list.Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DictItem> GetItemList(Expression<Func<hr_dictionary, bool>> predicate = null)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
            {
                var list = dal.Get(predicate);
                AutoMapper.Mapper.CreateMap<hr_dictionary, DictItem>();
                IEnumerable<DictItem> items = AutoMapper.Mapper.Map<IEnumerable<DictItem>>(list);
                return items;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(HrDictionary model)
        {
            if (model == null) return false;
            using (DictionaryDAL dal = new DictionaryDAL())
            {
                hr_dictionary entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        public List<HrDictionary> GetDictionaryByCategory(string category)
        {
            using (DictionaryDAL dal = new DictionaryDAL())
            {
                List<hr_dictionary> list = dal.FindAll(p => p.dictioncategory == category && p.isdeleted == 0).ToList();

                List<HrDictionary> list3 = new List<HrDictionary>();
                list.ForEach((p) => { list3.Add(EntityToModel(p)); });

                var list2 = list3.FindAll(p => p.FatherId == "0" || string.IsNullOrEmpty(p.FatherId));

                List<HrDictionary> listResult = new List<HrDictionary>();

                foreach (var item in list2)
                {
                    listResult.Add(Foo(list3, item));
                }

                return listResult;
            }
        }

        private HrDictionary Foo(List<HrDictionary> list, HrDictionary dic)
        {
            var listSearch = list.FindAll(p => p.FatherId == dic.DictionaryId);
            if (dic.Nodes == null)
                dic.Nodes = new List<HrDictionary>();
            foreach (var item in listSearch)
            {
                dic.Nodes.Add(Foo(list, item));
            }
            return dic;
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private hr_dictionary ModelToEntity(HrDictionary model)
        {
            if (model != null)
            {
                var entity = new hr_dictionary()
                {
                    dictionaryid = model.DictionaryId,
                    fatherid = model.FatherId,
                    dictioncategory = model.DictionCategory,
                    dictionaryname = model.DictionaryName,
                    dictionaryvalue = model.DictionaryValue,
                    createuser = model.CreateUser,
                    createdate = model.CreateDate,
                    updateuser = model.UpdateUser,
                    updatedate = model.UpdateDate,
                    dictioncategoryname = model.DictionCategoryName,
                    remark = model.Remark,
                    systemname = model.SystemName,
                    systemcode = model.SystemCode,
                    ordernumber = (int)model.OrderNumber,
                    systemneed = model.SystemNeed,
                    isdeleted = model.IsDeleted.Value ? 1 : 0
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
        private HrDictionary EntityToModel(hr_dictionary entity)
        {
            if (entity != null)
            {
                var model = new HrDictionary()
                {
                    DictionaryId = entity.dictionaryid,
                    FatherId = entity.fatherid,
                    DictionCategory = entity.dictioncategory,
                    DictionaryName = entity.dictionaryname,
                    DictionaryValue = entity.dictionaryvalue,
                    CreateUser = entity.createuser,
                    CreateDate = entity.createdate,
                    UpdateUser = entity.updateuser,
                    UpdateDate = entity.updatedate,
                    DictionCategoryName = entity.dictioncategoryname,
                    Remark = entity.remark,
                    SystemName = entity.systemname,
                    SystemCode = entity.systemcode,
                    OrderNumber = entity.ordernumber,
                    SystemNeed = entity.systemneed,
                    IsDeleted = entity.isdeleted == 1 ? true : false
                };

                return model;
            }
            return null;
        }
    }
}
