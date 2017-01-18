using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Examine
{
    public class SupDoctorGroup : BaseModel
    {
        public string ID { get; set; }
        public string GroupName { get; set; }
        public string GroupIcon { get; set; }
        public string GroupIconSource { get; set; }
        public string GroupIconFileName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupAdminID { get; set; }
        public string GroupAdminName { get; set; }
        public string OwnerPostID { get; set; }
        public string OwnerDepartID { get; set; }
        public string OwnerCompanyID { get; set; }
        public string DoctorIDs { get; set; }
        public string DoctorNames { get; set; }
        public string DoctorDatas { get; set; }
        /// <summary>
        /// 所属公司名称
        /// </summary>
        public string OwnerCompanyName { get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string OwnerDepartmentName { get; set; }
        /// <summary>
        /// 关注状态
        /// </summary>
        public int FollowStatus { get; set; }
    }
}
