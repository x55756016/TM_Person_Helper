using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Authorization
{
    public class RoleFunctionOrg
    {
        /// <summary>
        /// ID
        /// </summary>
        public string RoleFunctionOrgID { get; set; }
        /// <summary>
        /// 角色设置ID
        /// </summary>
        public string RoleFunctionID { get; set; }
        /// <summary>
        /// 类型（0：医院 1：部门 2：岗位）
        /// </summary>
        public string OrgType { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrgID { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}
