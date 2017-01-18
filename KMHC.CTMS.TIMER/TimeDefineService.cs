using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.TIMER.BLL;

namespace KMHC.CTMS.TIMER
{
    public partial class TimeDefineService : ServiceBase
    {
        System.Timers.Timer timsStart;  //计时器

        public TimeDefineService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timsStart = new System.Timers.Timer();
            timsStart.Interval = 60*1000; //设置计时器事件间隔执行时间  一分钟
            timsStart.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            timsStart.Enabled = true;
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour == 3 && DateTime.Now.Minute==0)//每天凌晨 2 点整执行
            {
                PushTimeTaskDefineBLL.RollTimeDefine();
            }
            //反射去执行方法
            //string bllName = "TimedTaskBLL";
            //string funName = "CheckTimeTask";

            ////Assembly dll = Assembly.LoadFile(Environment.CurrentDirectory + "\\KMHC.CTMS.BLL.dll");
            //Assembly dll = Assembly.LoadFile(@"D:\SvnCode\CTMS\KMHC.CTMS.TIMER\bin\Debug\KMHC.CTMS.BLL.dll");
            //Type function = dll.GetType("KMHC.CTMS.BLL.TimedTask.TimedTaskBLL", true);
            //MethodInfo method = function.GetMethod("CheckTimeTask");
            //var obj = dll.CreateInstance("KMHC.CTMS.BLL.TimedTask.TimedTaskBLL");
            //method.Invoke(obj, new object[] { new HashSet<string>() { "1", "2" } });

            //执行SQL语句或其他操作
            //TimedTaskBLL tb = new TimedTaskBLL();
            //tb.CheckTimeTask();
        }

        protected override void OnStop()
        {
        }
    }
}
