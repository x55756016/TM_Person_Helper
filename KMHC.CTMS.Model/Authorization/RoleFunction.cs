using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Authorization
{
    public class RoleFunction : BaseModel
    {
        /// <summary>
        /// 角色功能ID
        /// </summary>
        public string RoleFunctionID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 功能ID
        /// </summary>
        public string FunctionID { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public int PermissionValue { get; set; }

        /// <summary>
        /// 取值范围
        /// </summary>
        public string DataRange { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否设置组织权限
        /// </summary>
        public string IsSetOrg { get; set; }

        /// <summary>
        /// 组织架构范围
        /// </summary>
        public List<RoleFunctionOrg> RoleFunOrgs { get; set; }
    }
}
