/*
 * 描述:
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2016-11-16                                         创建 
 *  
 */

using System;

namespace Project.Model
{
    public class V_ctms_sys_sysmonitor
    {
        /// <summary>
        /// OPERATEID
        /// </summary>
        public string OPERATEID { get; set; }

        /// <summary>
        /// USERID
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// USERNAME
        /// </summary>
        public string USERNAME { get; set; }

        /// <summary>
        /// OPRATESOURCE
        /// </summary>
        public string OPRATESOURCE { get; set; }

        /// <summary>
        /// MODELCODE
        /// </summary>
        public string MODELCODE { get; set; }

        /// <summary>
        /// MODELNAME
        /// </summary>
        public string MODELNAME { get; set; }

        /// <summary>
        /// OPERATETYPE
        /// </summary>
        public string OPERATETYPE { get; set; }

        /// <summary>
        /// OPERATENAME
        /// </summary>
        public string OPERATENAME { get; set; }

        /// <summary>
        /// OPERATETIME
        /// </summary>
        public Nullable<DateTime> OPERATETIME { get; set; }

        /// <summary>
        /// REMARK
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// OWNERID
        /// </summary>
        public string OWNERID { get; set; }

        /// <summary>
        /// OWNERPOSTID
        /// </summary>
        public string OWNERPOSTID { get; set; }

        /// <summary>
        /// OWNERDEPARTMENTID
        /// </summary>
        public string OWNERDEPARTMENTID { get; set; }

        /// <summary>
        /// OWNERCOMPANYID
        /// </summary>
        public string OWNERCOMPANYID { get; set; }
        public string SystemId { get; set; }

    }
}