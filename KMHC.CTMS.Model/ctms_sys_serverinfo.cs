using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class V_ctms_sys_serverinfo : BaseMonitor
    {
        public string ServerInfoId { get; set; }
        public string IPAddress { get; set; }
        public Nullable<float> CPUValue { get; set; }
        public Nullable<float> MemoryValue { get; set; }
        public Nullable<float> DiskValue { get; set; }
        public Nullable<System.DateTime> InputTime { get; set; }
        public string SystemId { get; set; }
    }

    public class C_ctms_sys_serverinfo
    {
        public Nullable<float> CPUValue { get; set; }
        public Nullable<float> MemoryValue { get; set; }
        public Nullable<float> DiskValue { get; set; }
        public string InputTime { get; set; }
    }
}
