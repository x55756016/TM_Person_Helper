using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class V_ctms_sys_errorcount
    {
        public string ErrorId { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string LocalPath { get; set; }
        public string InnerException { get; set; }
        public Nullable<int> Port { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> InputTime { get; set; }
        public string Platform { get; set; }
        public Nullable<int> Type { get; set; }
        public string ErrorName { get; set; }
        public string SystemId { get; set; }
    }
}
