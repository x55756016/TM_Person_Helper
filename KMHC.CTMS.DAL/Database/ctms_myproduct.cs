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
    
    public partial class ctms_myproduct
    {
        public string myproductid { get; set; }
        public string userid { get; set; }
        public string loginname { get; set; }
        public string orderid { get; set; }
        public string productcode { get; set; }
        public string productid { get; set; }
        public string productname { get; set; }
        public int productnum { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public int isused { get; set; }
        public Nullable<System.DateTime> useddate { get; set; }
        public string createuserid { get; set; }
        public string createusername { get; set; }
        public Nullable<System.DateTime> createdatetime { get; set; }
        public string edituserid { get; set; }
        public string editusername { get; set; }
        public Nullable<System.DateTime> editdatetime { get; set; }
        public string ownerid { get; set; }
        public string ownername { get; set; }
        public int isdeleted { get; set; }
    }
}
