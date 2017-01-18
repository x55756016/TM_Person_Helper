/*
 * 描述:
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 *    		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.TIMER.Model
{
    public class TimedTaskDto
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
        public string CREATETIME { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string UPDATETIME { get; set; }
        public string FUNCTIONMARK { get; set; }
    }
}
