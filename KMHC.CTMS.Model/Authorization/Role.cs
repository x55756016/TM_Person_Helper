using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Authorization
{
    public class Role : BaseModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 子系统
        /// </summary>
        public Nullable<int> SystemCategory { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public bool IsSelected { get; set; }

        /// <summary>
        /// 子系统名称
        /// </summary>
        public string CategoryValue { get; set; }
    }

    /// <summary>
    /// 角色扩展模型
    /// </summary>
    public class ExtRole
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 子系统
        /// </summary>
        public Nullable<int> SystemCategory { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 功能列表
        /// </summary>
        public IList<ExtFun> ExtFuns { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public IList<RoleFunction> RoleFuns { get; set; }
    }
}
