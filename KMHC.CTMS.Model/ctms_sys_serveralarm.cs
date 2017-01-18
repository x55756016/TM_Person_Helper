using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class V_ctms_sys_serveralarm
    {
        public string ServerAlarmId { get; set; }
        public Nullable<System.DateTime> InputTime { get; set; }
        public string IPAddress { get; set; }
        public string Message { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Type { get; set; }
        public string SystemId { get; set; }
    }
}
