using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KMHC.CTMS.TIMER.BLL;
using Timer = System.Timers.Timer;
using Project.Common.Helper;

namespace KMHC.CTMS.TIMER
{
    partial class TaskActionService : ServiceBase
    {
        private bool flag;
        private System.Timers.Timer timeStart;// 计时器

        public TaskActionService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            flag = true;
            //Thread.Sleep(4000);

            //PushTimeTaskDefineBLL.PollingTimeTask();



            //TODO: Add code here to start your service.
            timeStart = new Timer();
            timeStart.Interval = 1000 *10;//10s执行一次 //1000毫秒  =  1秒
            timeStart.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            timeStart.Enabled = true;


            //StringBuilder sb = new StringBuilder();

            //  // 获取程序的基目录，结尾包含\
            //  var a = AppDomain.CurrentDomain.BaseDirectory;
            //  Console.WriteLine(a);
            //  sb.Append("获取程序的基目录，结尾包含"+a.ToString());

            //  // 获取和设置包括该应用程序的目录的名称，与上一个一样
            //  var b = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //  Console.WriteLine(b);
            //  sb.Append("获取和设置包括该应用程序的目录的名称，与上一个一样" + b.ToString());

  
            //  // 获取启动了应用程序的可执行文件的路径及文件名
            //  //var c = MediaTypeNames.Application.StartupPath;
            // //Console.WriteLine(c);
            // // 获取模块的完整路径，与上一个一样
            // var d = Process.GetCurrentProcess().MainModule.FileName;
            // Console.WriteLine(d);

            // sb.Append("获取模块的完整路径，与上一个一样" + d.ToString());

 
            // // 获取和设置当前目录(该进程从中启动的目录)的完全限定目录
            // var e = Environment.CurrentDirectory;
            // Console.WriteLine(e);


            // sb.Append("获取和设置当前目录(该进程从中启动的目录)的完全限定目录" + e.ToString());


            // // 获取应用程序的当前工作目录
            // var f = Directory.GetCurrentDirectory();
            // Console.WriteLine(f);

            // sb.Append("获取应用程序的当前工作目录" + f.ToString());


            // // 获取启动了应用程序的可执行文件的路径
            // //var g = MediaTypeNames.Application.StartupPath;
            // //Console.WriteLine(g);

            //LogHelper.WriteInfo(sb.ToString());
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if ((DateTime.Now.Minute == 30 || DateTime.Now.Minute == 0) && flag)
            {
                LogHelper.WriteInfo("执行定时任务");
                flag = false;
                try
                {
                    PushTimeTaskDefineBLL.PollingTimeTask();
                }
                catch (Exception ex)
                {
                    LogHelper.WriteInfo(ex.ToString());
                }
                finally
                {
                    flag = true;
                }
            }
        }
    }
}
