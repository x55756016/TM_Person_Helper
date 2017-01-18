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
    public class V_ctms_pm_project
    {      
    	    			         	/// <summary>
		        /// PROJECTID
		        /// </summary>
		     		        		public string PROJECTID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// ORDERNUMBER
		        /// </summary>
		     		        		public Nullable<int> ORDERNUMBER  { get; set; }
	        		        	
         		         	/// <summary>
		        /// PROJECTNAME
		        /// </summary>
		     		        		public string PROJECTNAME  { get; set; }
	        		        	
         		         	/// <summary>
		        /// FATHERPROJECTID
		        /// </summary>
		     		        		public string FATHERPROJECTID  { get; set; }
	        		        	
         		         	/// <summary>
		        /// COSTTIME
		        /// </summary>
		     		        		public string COSTTIME  { get; set; }
	        		        	
         		         	/// <summary>
		        /// STARTDATE
		        /// </summary>
		     		        		public string STARTDATE  { get; set; }
	        		        	
         		         	/// <summary>
		        /// ENDDATE
		        /// </summary>
		     		        		public string ENDDATE  { get; set; }
	        		        	
         		         	/// <summary>
		        /// PROGRESS
		        /// </summary>
		     		        		public string PROGRESS  { get; set; }
	        		        	
         		         	/// <summary>
		        /// PROJECTREMARK
		        /// </summary>
		     		        		public string PROJECTREMARK  { get; set; }
	        		        	
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
	        		        	
         		         	/// <summary>
		        /// RESULTSCORE
		        /// </summary>
		     		        		public string RESULTSCORE  { get; set; }
	        		        	
         		         	/// <summary>
		        /// SUGGESTIONS
		        /// </summary>
		     		        		public string SUGGESTIONS  { get; set; }
	        		        	
         		         	/// <summary>
		        /// BUDGET
		        /// </summary>
		     		        		public Nullable<decimal> BUDGET  { get; set; }
	        		        	
         		         	/// <summary>
		        /// REALCOST
		        /// </summary>
		     		        		public Nullable<decimal> REALCOST  { get; set; }
	        		        	
         	    	    }
}