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
    
    public partial class ctms_sys_errorcount
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
        public string SystemId { get; set; }
    }
}
