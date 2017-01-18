using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class V_ctms_sys_userregrecord
    {
        public string UserRegRecordId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public Nullable<int> Age { get; set; }
        public string LoginName { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string UserSource { get; set; }
        public Nullable<System.DateTime> InputTime { get; set; }
        public string SystemId { get; set; }
    }
}
