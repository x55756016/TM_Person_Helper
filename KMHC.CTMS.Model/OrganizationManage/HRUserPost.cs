using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.OrganizationManage
{
    [Serializable]
    [DataContract]
    public class HRUserPost
    {
        /// <summary>
        /// 员工岗位id
        /// </summary>
        [DataMember]
        public string EmployeepostID { get; set; }
        /// <summary>
        /// 是否代理岗位
        /// </summary>
        [DataMember]
        public string IsAgency { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        [DataMember]
        public string UserID { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [DataMember]
        public string DepartmentID { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        [DataMember]
        public string CompanyID { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        [DataMember]
        public string PostName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public string CName { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        [DataMember]
        public string PostID { get; set; }
        /// <summary>
        /// 岗位级别
        /// </summary>
        [DataMember]
        public Nullable<int> PostLevel { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        [DataMember]
        public string CheckState { get; set; }
        /// <summary>
        /// 编辑状态
        /// </summary>
        [DataMember]
        public string EditState { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreateUserID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public Nullable<System.DateTime> CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember]
        public string UpdateUserID { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }


    public class HRUserPostDTO
    {
        /// <summary>
        /// 员工岗位id
        /// </summary>
        public string EmployeepostID { get; set; }
        /// <summary>
        /// 是否代理岗位
        /// </summary>
        public string IsAgency { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public string PostID { get; set; }
        /// <summary>
        /// 岗位级别
        /// </summary>
        public Nullable<int> PostLevel { get; set; }
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
        /// 公司ID
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 公司中文名称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 用户登录帐号
        /// </summary>
        public string UserLoginName { get; set; }
    }


    public class HROrganUserDTO
    {
        public string level { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string data { get; set; }
        public bool open { get; set; }
        public bool nocheck { get; set; }
        public List<HROrganUserDTO> children { get; set; }

    }



    public class HROrganUser
    {
        public string CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string PositionID { get; set; }

        public string PositionName { get; set; }

        public string UserID { get; set; }

        public string LoginName { get; set; }

        public string UserName { get; set; }
    }
}
