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
    public class V_ctms_pm_task
    {      
    	    			         	/// <summary>
		        /// TASKID
		        /// </summary>
		     		        		public string TASKID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// PROJECTID
		        /// </summary>
		     		        		public string PROJECTID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// PRETASKID
		        /// </summary>
		     		        		public string PRETASKID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// FATHERTASKID
		        /// </summary>
		     		        		public string FATHERTASKID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// ORDERNUMBER
		        /// </summary>
		     		        		public Nullable<int> ORDERNUMBER  { get; set; }
	        		        	
         		         	/// <summary>
		        /// TASKNAME
		        /// </summary>
		     		        		public string TASKNAME  { get; set; }
	        		        	
         		         	/// <summary>
		        /// COSTTIME
		        /// </summary>
		     		        		public string COSTTIME  { get; set; }
	        		        	
         		         	/// <summary>
		        /// STARTDATE
		        /// </summary>
		     		        		public Nullable<DateTime> STARTDATE  { get; set; }
	        		        	
         		         	/// <summary>
		        /// ENDDATE
		        /// </summary>
		     		        		public Nullable<DateTime> ENDDATE  { get; set; }
	        		        	
         		         	/// <summary>
		        /// USERNAME
		        /// </summary>
		     		        		public string USERNAME  { get; set; }
	        		        	
         		         	/// <summary>
		        /// PROGRESS
		        /// </summary>
		     		        		public string PROGRESS  { get; set; }
	        		        	
         		         	/// <summary>
		        /// TASKREMARK
		        /// </summary>
		     		        		public string TASKREMARK  { get; set; }
	        		        	
         		         	/// <summary>
		        /// OWENRID
		        /// </summary>
		     		        		public string OWENRID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// OWNERPOSTID
		        /// </summary>
		     		        		public string OWNERPOSTID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// OWNERDEPARTMENTID
		        /// </summary>
		     		        		public string OWNERDEPARTMENTID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// OWNERCOMPANYID
		        /// </summary>
		     		        		public string OWNERCOMPANYID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// CREATETIME
		        /// </summary>
		     		        		public Nullable<DateTime> CREATETIME  { get; set; }
	        		        	
         		         	/// <summary>
		        /// CREATEUSER
		        /// </summary>
		     		        		public string CREATEUSER  { get; set; }
	        		        	
         		         	/// <summary>
		        /// EDITTIME
		        /// </summary>
		     		        		public Nullable<DateTime> EDITTIME  { get; set; }
	        		        	
         		         	/// <summary>
		        /// EDITUSER
		        /// </summary>
		     		        		public string EDITUSER  { get; set; }
	        		        	
         		         	/// <summary>
		        /// PMANAGER
		        /// </summary>
		     		        		public string PMANAGER  { get; set; }
	        		        	
         		         	/// <summary>
		        /// ISCOMFIRM
		        /// </summary>
		     		        		public string ISCOMFIRM  { get; set; }
	        		        	
         		         	/// <summary>
		        /// REALFINISHDATE
		        /// </summary>
		     		        		public Nullable<DateTime> REALFINISHDATE  { get; set; }
	        		        	
         	    	    }
}