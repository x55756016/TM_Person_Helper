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
    
    public partial class ctms_hr_department
    {
        public string DEPARTMENTID { get; set; }
        public string DEPARTMENTCODE { get; set; }
        public string DEPARTMENTNAME { get; set; }
        public string DEPARTMENTLEVEL { get; set; }
        public string COMPANYID { get; set; }
        public string FATHERTYPE { get; set; }
        public string FATHERID { get; set; }
        public string DEPARTMENTBOSSHEAD { get; set; }
        public string DEPARTMENTHEADNAME { get; set; }
        public string DEPARTMENTFUNCTION { get; set; }
        public string CHECKSTATE { get; set; }
        public string EDITSTATE { get; set; }
        public string CREATEUSERID { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public string UPDATEUSERID { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string OWNERID { get; set; }
        public string OWNERPOSTID { get; set; }
        public string OWNERDEPARTMENTID { get; set; }
        public string OWNERCOMPANYID { get; set; }
    }
}