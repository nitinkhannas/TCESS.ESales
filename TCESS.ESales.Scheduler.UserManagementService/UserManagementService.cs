#region Using directives

using System;
using System.Diagnostics;
using System.ServiceProcess;
//using TCESS.ESales.Scheduler.UserManagement;

#endregion

namespace TCESS.ESales.Scheduler.UserManagementService
{
    public partial class UserManagementService : ServiceBase
    {
        public UserManagementService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OnStart(): Put startup code here
        /// - Start threads, get inital data, etc.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("UserManagementService Started at:" + DateTime.Now.ToShortDateString());
            //UserManagement.UserManagement userMgmtScheduler = UserManagement.UserManagement.GetInstance();
        }

        /// <summary>
        /// OnStop(): Put your stop code here
        /// - Stop threads, set final data, etc.
        /// </summary>
        protected override void OnStop()
        {
            EventLog.WriteEntry("UserManagementService Stopped at:" + DateTime.Now.ToShortDateString());
            base.OnStop();
        }
    }
}