using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class BaseModel
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public virtual string CreateUserID { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public virtual string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public virtual string EditUserID { get; set; }


        /// <summary>
        /// 修改人姓名
        /// </summary>
        public virtual string EditUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? EditTime { get; set; }

        /// <summary>
        /// 所属人ID
        /// </summary>
        public virtual string OwnerID { get; set; }

        /// <summary>
        /// 所属人姓名
        /// </summary>
        public virtual string OwnerName { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 所属公司id
        /// </summary>
        public virtual string OwnerCompanyId { get; set; }

        /// <summary>
        /// 所属部门id
        /// </summary>
        public virtual string OwnerDeptId { get; set; }

        /// <summary>
        /// 所属岗位id
        /// </summary>
        public virtual string OwnerPostId { get; set; }
    }
}
