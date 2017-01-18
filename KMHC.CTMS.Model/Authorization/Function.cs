using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Authorization
{
    /// <summary>
    /// 功能实体
    /// </summary>
    public class Function : BaseModel
    {
        /// <summary>
        /// 功能ID
        /// </summary>
        public string FunctionID { get; set; }

        /// <summary>
        /// 功能编码
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public FunctionStatus Status { get; set; }

        /// <summary>
        /// 子系统(从字典项SubSystem中查询)
        /// </summary>
        public int SystemCategory { get; set; }

        /// <summary>
        /// 父功能ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 是否为菜单
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 菜单实体
        /// </summary>
        public MenuInfo Menu { get; set; }

        /// <summary>
        /// 帮助说明
        /// </summary>
        public string HelperTitle { get; set; }

        /// <summary>
        /// 帮助链接
        /// </summary>
        public string HelperUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否公开(无权限管控)
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 默认是否展开菜单
        /// </summary>
        public bool IsExpand { get; set; }
    }

    /// <summary>
    /// 功能扩展实体
    /// </summary>
    public class ExtFun
    {
        /// <summary>
        /// 功能ID
        /// </summary>
        public string FunctionID { get; set; }

        /// <summary>
        /// 功能编码
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 是否为菜单
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public IList<Permission> Permissions { get; set; }
    }
}
