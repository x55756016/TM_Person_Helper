using Project.Common.Helper;
using Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Project.ServerClientDOS
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer t = new Timer();
            t.Interval = 1000 * 60;
            t.Elapsed += t_Elapsed;
            t.Start();
        }

        private static void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                SystemInfo sys = new SystemInfo();

                double cpuValue = Math.Round(sys.CpuLoad, 2);
                double memoryValue = Math.Round((Convert.ToDouble(sys.PhysicalMemory - sys.MemoryAvailable) / sys.PhysicalMemory) * 100, 2);

                double diskValue = 0;
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (var item in drives)
                {
                    if (item.DriveType != DriveType.Fixed)
                        continue;
                    double persent = Math.Round((Convert.ToDouble(item.TotalSize - item.AvailableFreeSpace) / item.TotalSize) * 100, 2);
                    if (persent > diskValue)
                        diskValue = persent;
                }

                V_ctms_sys_serverinfo model = new V_ctms_sys_serverinfo()
                {
                    IPAddress = GetAddressIP(),
                    CPUValue = (float)cpuValue,
                    MemoryValue = (float)memoryValue,
                    DiskValue = (float)diskValue,
                    InputTime = DateTime.Now,
                    SystemId = ConfigurationManager.AppSettings["SYSTEM_ID"]
                };

                WebClient client = new WebClient();
                byte[] b = client.UploadValues(ConfigurationManager.AppSettings["MONITOR_URL"] + "api/ctms_sys_serverinfo", model.GetParameters());
                LogHelper.WriteInfo(string.Format("{0}上传数据成功", DateTime.Now));
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                LogHelper.WriteInfo(string.Format("{0}上传数据失败--->{1}", DateTime.Now, ex.ToString() + ex.InnerException));
            }
        }

        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public static string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }

        protected override void OnStop()
        {
        }
    }
}
