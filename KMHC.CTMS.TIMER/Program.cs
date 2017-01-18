using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.TIMER
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new TimeDefineService(),
                new TaskActionService(),
                new OrderOverTimeService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
