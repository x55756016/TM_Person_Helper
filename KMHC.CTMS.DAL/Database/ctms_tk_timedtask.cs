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
    
    public partial class ctms_tk_timedtask
    {
        public string TIMEDTASKID { get; set; }
        public string TIMEDTASKDEFINEID { get; set; }
        public string TRIGGERTYPE { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public string STARTTIME { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
        public string ENDTIME { get; set; }
        public string RECEIVEUSER { get; set; }
        public string RECEIVEROLE { get; set; }
        public string MESSAGEBODY { get; set; }
        public string MODELCODE { get; set; }
        public string MSGLINKURL { get; set; }
        public string PROCESSWCFURL { get; set; }
        public string PROCESSFUNCNAME { get; set; }
        public string PROCESSFUNCPAMETER { get; set; }
        public string TASKSTATUS { get; set; }
        public Nullable<System.DateTime> EXECUTTIME { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string FUNCTIONMARK { get; set; }
        public string RECEIVEUSERNAME { get; set; }
        public string RECEIVEROLENAME { get; set; }
    }
}
