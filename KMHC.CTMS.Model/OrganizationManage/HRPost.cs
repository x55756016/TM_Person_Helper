using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.OrganizationManage
{
    public class HRPost
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string PostID { get; set; }
        /// <summary>
        /// 岗位编号
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 岗位职责
        /// </summary>
        public string PostFunction { get; set; }
        /// <summary>
        /// 人员编制
        /// </summary>
        public Nullable<decimal> PostNumber { get; set; }
        /// <summary>
        /// 岗位级别
        /// </summary>
        public Nullable<int> PostLevel { get; set; }
        /// <summary>
        /// 岗位系数
        /// </summary>
        public string PostCoefficient { get; set; }
        /// <summary>
        /// 岗位目标
        /// </summary>
        public string PostGoal { get; set; }
        /// <summary>
        /// 直接上级
        /// </summary>
        public string FatherPostID { get; set; }
        /// <summary>
        /// 下属人数
        /// </summary>
        public Nullable<decimal> UnderNumber { get; set; }
        /// <summary>
        /// 晋升方向
        /// </summary>
        public string PromoteDirection { get; set; }
        /// <summary>
        /// 轮换岗位
        /// </summary>
        public string ChangePost { get; set; }
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
}
