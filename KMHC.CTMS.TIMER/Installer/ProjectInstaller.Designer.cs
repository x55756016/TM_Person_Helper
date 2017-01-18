namespace KMHC.CTMS.TIMER
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.TestTimeTask = new System.ServiceProcess.ServiceInstaller();
            this.TaskActionService = new System.ServiceProcess.ServiceInstaller();
            this.OrderOverTimeService = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // TestTimeTask
            // 
            this.TestTimeTask.Description = "轮询定时设置表并生成对应任务";
            this.TestTimeTask.DisplayName = "CTMS_TimeDefineService";
            this.TestTimeTask.ServiceName = "TimeDefineService";
            // 
            // TaskActionService
            // 
            this.TaskActionService.Description = "执行定时任务";
            this.TaskActionService.DisplayName = "CTMS_TaskActionService";
            this.TaskActionService.ServiceName = "TaskActionService";
            // 
            // OrderOverTimeService
            // 
            this.OrderOverTimeService.Description = "定时轮询医生是否回答问题，否则提醒客服跟踪";
            this.OrderOverTimeService.DisplayName = "CTMS_OrderOverTimeService";
            this.OrderOverTimeService.ServiceName = "OrderOverTimeService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.TestTimeTask,
            this.TaskActionService,
            this.OrderOverTimeService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller TestTimeTask;
        private System.ServiceProcess.ServiceInstaller TaskActionService;
        private System.ServiceProcess.ServiceInstaller OrderOverTimeService;
    }
}