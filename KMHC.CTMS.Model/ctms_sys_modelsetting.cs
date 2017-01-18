using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class V_ctms_sys_modelsetting : BaseMonitor
    {
        public string ModelSettingId { get; set; }
        public string ModelName { get; set; }
        public string ModelCode { get; set; }
        public string Remark { get; set; }
        public int IsValid { get; set; }
        public int IsDeleted { get; set; }
        public string CreateUserAccount { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public string LastModifyUserAccount { get; set; }
        public System.DateTime LastModifyDateTime { get; set; }
        public Nullable<int> ModelSource { get; set; }
        public string SystemId { get; set; }
    }
}
