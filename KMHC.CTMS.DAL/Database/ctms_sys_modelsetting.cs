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
    
    public partial class ctms_sys_modelsetting
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