using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class V_ctms_sys_servermonitor : BaseMonitor
    {
        public string ServerMonitorId { get; set; }
        public string IPAddress { get; set; }
        public string ServerName { get; set; }
        public Nullable<int> CPUMaxValue { get; set; }
        public Nullable<int> MemoryMaxValue { get; set; }
        public Nullable<int> DiskMaxValue { get; set; }
        public string ContactUserName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public Nullable<int> IsValid { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> InputTime { get; set; }
        public string CreateUserLoginName { get; set; }
        public string CreateUserName { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserLoginName { get; set; }
        public string ModifyUserName { get; set; }
        public string SystemId { get; set; }
    }
}
