using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.OrganizationManage
{
    public class HRDepartment
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentID { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 部门级别
        /// </summary>
        public string DepartmentLevel { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 上级机构类型
        /// </summary>
        public string FatherType { get; set; }
        /// <summary>
        /// 上级机构ID
        /// </summary>
        public string FatherID { get; set; }
        /// <summary>
        /// 部门负责人ID
        /// </summary>
        public string DepartmentBossHead { get; set; }
        /// <summary>
        /// 部门负责人姓名
        /// </summary>
        public string DepartmentHeadName { get; set; }
        /// <summary>
        /// 部门职责
        /// </summary>
        public string DepartmentFunction { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string CheckState { get; set; }
        /// <summary>
        /// 编辑状态
        /// </summary>
        public string EditState { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUserID { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public Nullable<System.DateTime> UpdateDate { get; set; }
        /// <summary>
        /// 所属员工ID
        /// </summary>
        public string OwnerID { get; set; }
        /// <summary>
        /// 所属岗位ID
        /// </summary>
        public string OwnerPostID { get; set; }
        /// <summary>
        /// 所属部门ID
        /// </summary>
        public string OwnerDepartmentID { get; set; }
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public string OwnerCompanyID { get; set; }
    }

    public class HRDepartmentDTO
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentID { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 部门级别
        /// </summary>
        public string DepartmentLevel { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
}
