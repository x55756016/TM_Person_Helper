using Project.DAL.Database;
/*
 * 描述:保持线程内dbcontext的唯一
 *  
 * 修订历史: 
 * 日期                修改人              Email                  内容
 * 20160331   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace KMHC.CTMS.TIMER.DAL
{
   public  class DbSessionFactory
    {
       public static DbContext GetCurrentDbContext()
       {
           //callContext 存在于线程中的独立数据槽，该位置的变量由当前线程中共享,当该线程销毁的时候，该变量也销毁
           //https://msdn.microsoft.com/zh-cn/library/system.runtime.remoting.messaging.callcontext(VS.80).aspx
           var dbContext = CallContext.GetData("DbContext") as DbContext;
           if (dbContext == null)
           {
               dbContext = new tmpmEntities2();
               CallContext.SetData("DbContext", dbContext);
           }
           return dbContext;
       }
    }
}
