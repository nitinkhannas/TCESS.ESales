using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Net;
using System.Configuration;

namespace TCESS.ESales.Scheduler.DataSynchronizerService
{
	public partial class DataSyncService : ServiceBase
	{
		public DataSyncService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			EventLog.WriteEntry("DataSyncService Started at:" + DateTime.Now.ToShortDateString());
			DataSynchronizer a = new DataSynchronizer();
			a.InitializeTimerControl();
		}

		protected override void OnStop()
		{
			EventLog.WriteEntry("DataSyncService Stopped at:" + DateTime.Now.ToShortDateString());
			base.OnStop();
		}
	}
}