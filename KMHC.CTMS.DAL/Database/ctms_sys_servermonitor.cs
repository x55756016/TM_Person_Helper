//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.DAL.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ctms_sys_servermonitor
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
