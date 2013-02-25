﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace TCESS.ESales.Scheduler.DataSynchronizerService
{
	[RunInstaller(true)]
	public partial class ProjectInstaller : System.Configuration.Install.Installer
	{
		public ProjectInstaller()
		{
			InitializeComponent();
		}

		private void serviceProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
		{
			using (ServiceController controller = new ServiceController(serviceInstaller.ServiceName))
			{
				controller.Start();
			}
		}		
	}
}