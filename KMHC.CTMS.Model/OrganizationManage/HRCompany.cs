using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.OrganizationManage
{
    public class HRCompany
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 企业类型:1地产企业,2物流企业,3生产企业,4软件企业,零售企业
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanryCode { get; set; }
        /// <summary>
        /// 公司英文名称
        /// </summary>
        public string EName { get; set; }
        /// <summary>
        /// 公司中文名称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompanyCategory { get; set; }
        /// <summary>
        /// 所在地城市，系统字典中定义
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 国别 0 中国大陆1 中国香港 系统字典中定义
        /// </summary>
        public string CountyType { get; set; }
        /// <summary>
        ///公司级别
        /// </summary>
        public string CompanyLevel { get; set; }
        /// <summary>
        /// 父公司ID
        /// </summary>
        public string FatherCompanyID { get; set; }
        /// <summary>
        ///  上级机构类型
        /// </summary>
        public string FatherType { get; set; }
        /// <summary>
        ///公司地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string LegalPerson { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LinkMan { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string TelNumber { get; set; }
        /// <summary>
        /// 法人身份证号
        /// </summary>
        public string LegalPersonID { get; set; }
        /// <summary>
        /// 营业执照号
        /// </summary>
        public string BussinessLicenceNo { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string BussinessArea { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string AccountCode { get; set; }
        /// <summary>
        /// 开户银行代码
        /// </summary>
        public string BankID { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string FaxNumber { get; set; }
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

    public class OrganizationTree
    {
        public string text { get; set; }

        public string value { get; set; }

        public string tags { get; set; }

        public IEnumerable<OrganizationTree> nodes { get; set; }
    }
}
