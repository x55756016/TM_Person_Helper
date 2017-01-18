using KMHC.CTMS.TIMER.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace KMHC.CTMS.TIMER
{
    partial class OrderOverTimeService : ServiceBase
    {
        private System.Timers.Timer timeStart;// 计时器
        public OrderOverTimeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO:  在此处添加代码以启动服务。
            timeStart = new Timer();
            timeStart.Interval = 1000 * 60 * 30;//半个小时一次 //1000毫秒  =  1秒
            timeStart.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            timeStart.Enabled = true;
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            OrderOverTimeBLL.DoWork();
        }
    }
}
