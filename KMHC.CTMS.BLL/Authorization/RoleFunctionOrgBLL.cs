using Project.Common.Helper;
using Project.DAL.Authorization;
using Project.DAL.Database;
using Project.Model.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Authorization
{
    public class RoleFunctionOrgBLL
    {
        private readonly string logTitle = "访问RoleFunctionOrgBLL类";
        /// <summary>
        /// 新增角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(RoleFunctionOrg model)
        {
            if (model == null) return string.Empty;
            using (RoleFunctionOrgDAL dal = new RoleFunctionOrgDAL())
            {
                if (string.IsNullOrEmpty(model.RoleFunctionOrgID)) model.RoleFunctionOrgID = Guid.NewGuid().ToString();
                AutoMapper.Mapper.CreateMap<RoleFunctionOrg, ctms_sys_rolefunctionorg>();
                ctms_sys_rolefunctionorg entity = AutoMapper.Mapper.Map<ctms_sys_rolefunctionorg>(model);
                dal.Insert(entity);

                return model.RoleFunctionOrgID;
            }
        }

        /// <summary>
        /// 修改角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(RoleFunctionOrg model)
        {
            if (string.IsNullOrEmpty(model.RoleFunctionOrgID))
            {
                LogHelper.WriteInfo("试图修改为空的RoleFunctionOrg实体！");
                throw new KeyNotFoundException();
            }
            using (RoleFunctionOrgDAL dal = new RoleFunctionOrgDAL())
            {
                AutoMapper.Mapper.CreateMap<RoleFunctionOrg, ctms_sys_rolefunctionorg>();
                ctms_sys_rolefunctionorg entity = AutoMapper.Mapper.Map<ctms_sys_rolefunctionorg>(model);

                return dal.Update(entity);
            }
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<RoleFunctionOrg> GetList(Expression<Func<ctms_sys_rolefunctionorg, bool>> predicate = null)
        {
            using (RoleFunctionOrgDAL dal = new RoleFunctionOrgDAL())
            {
                List<ctms_sys_rolefunctionorg> entity = dal.Get(predicate).ToList();

                AutoMapper.Mapper.CreateMap<ctms_sys_rolefunctionorg, RoleFunctionOrg>();
                return AutoMapper.Mapper.Map<List<RoleFunctionOrg>>(entity);
            }
        }
    }
}
