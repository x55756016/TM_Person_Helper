//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace KMHC.CTMS.TIMER.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTMS_OD_ORDERS
    {
        public string ORDERID { get; set; }
        public string ORDERCODE { get; set; }
        public string USERID { get; set; }
        public string ORDERTYPE { get; set; }
        public string PRODUCTCODE { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string CHANNELTYPE { get; set; }
        public string CHANNELORDERID { get; set; }
        public Nullable<System.DateTime> PAYTIME { get; set; }
        public string ORDERSTATUS { get; set; }
        public string EXPRESSID { get; set; }
        public string RECEIVEADDR { get; set; }
        public string CREATEUSERID { get; set; }
        public string CREATEUSERNAME { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public string EDITUSERNAME { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public string OWNERID { get; set; }
        public string OWNERNAME { get; set; }
        public bool ISDELETED { get; set; }
        public string USERNAME { get; set; }
        public string MOBILENUMBER { get; set; }
        public string KMORDERNUMBER { get; set; }
        public Nullable<bool> NEEDDAIJIAN { get; set; }
        public Nullable<bool> NEEDINVOICE { get; set; }
        public string INVOICEREMARK { get; set; }
        public string OWNERDOCID { get; set; }
        public string BALANCESTATUS { get; set; }
        public Nullable<System.DateTime> ORDERENDDATE { get; set; }
        public string PRODUCTINSTANCEID { get; set; }
        public Nullable<decimal> TOTALFEE { get; set; }
        public Nullable<decimal> LOGISTICSFEE { get; set; }
        public Nullable<decimal> DAIJIANFEE { get; set; }
    }
}
