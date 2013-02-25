#region Using directives

using System;
using System.IO;
using System.Linq;
using TCESS.ESales.Logging;
using System.Net;
using System.Collections;
using System.Configuration;
using System.Threading;
using System.Security.AccessControl;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.BusinessLayer.Services;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using System.Collections.Generic;
using TCESS.ESales.CommonLayer.Mapper;
using TCESS.ESales.CommonLayer.UnityExtension;

#endregion

namespace TCESS.ESales.Scheduler.DataSynchronizer
{
	public class DailyDataSynchronizer
	{
		#region Private Member Variables

		private TimerCallback _timerDelegate;
		private Timer _stateTimer;

		// Singleton instance
		private static volatile DailyDataSynchronizer uniqueInstance;

		#endregion

		#region Private Constructor

		private DailyDataSynchronizer()
		{
			try
			{
				// Initialize timer control to connect to FTP when time is elapsed
				InitializeTimerControl();
			}
			catch (Exception exception)
			{
				// Logs the error
                CustomLogger.WriteLog(exception.Message);
			}
		}

		#endregion

		/// <summary>
		/// GetInstance Constructor Will Check For Nullability of existing Instance, If Exists,
		/// Instantiate it with New Or Else Return the existing Instance
		/// </summary>
		public static DailyDataSynchronizer GetInstance()
		{
			try
			{
				if (uniqueInstance == null)
				{
					uniqueInstance = new DailyDataSynchronizer();
				}
			}
			catch (Exception ex)
			{
				//Logs the error
                CustomLogger.WriteLog(ex.Message);
			}
			//return the value
			return uniqueInstance;
		}

		private void InitializeTimerControl()
		{
			try
			{
				string scheduledTime = ConfigurationManager.AppSettings["ScheduledTime"];
				TimeSpan scheduledTimeSpan = new TimeSpan(Convert.ToDateTime(scheduledTime).Hour,
					Convert.ToDateTime(scheduledTime).Minute, 0);

				// Get today's date at scheduled time
				DateTime scheduledRunTime = DateTime.Today.Add(scheduledTimeSpan);

				// If scheduled time has passed, get tomorrow's schedule time
				string dataTranferWaitPeriod = ConfigurationManager.AppSettings["DataTranferWaitPeriod"];
				if (DateTime.Now > scheduledRunTime)
				{
					scheduledRunTime = DateTime.Now.AddDays(1);
				}
				// Calculate milliseconds until the next scheduled time.
				int timeToFirstExecution = (int)scheduledRunTime.Subtract(DateTime.Now).TotalMilliseconds;

				// Calculate the number of milliseconds in 24 hours.
				int timeBetweenCalls = (int)new TimeSpan(24, 0, 0).TotalMilliseconds;

				// Set the method to execute when the timer executes.
                _timerDelegate = DataSynchronization;

				// Start the timer. The timer will execute "UploadFilesToFTPServer" when the number of 
				// seconds between now and the next scheduled time elapse. 
				// After that, it will execute every 24 hours.
				_stateTimer = new Timer(_timerDelegate, null, timeToFirstExecution, timeBetweenCalls);
			}
			catch (Exception ex)
			{
				//Logs the error
                CustomLogger.WriteLog(ex.Message);
			}
		}

        private void UpdateDCADailyPercentage()
        {
            IList<DcaMaterialAllocationDTO> lstMaterialAllocations = new List<DcaMaterialAllocationDTO>();

            IList<MaterialTypeDTO> lstMaterialType = ESalesUnityContainer.Container.Resolve<IMaterialTypeService>()
                .GetMaterialTypeList(true);

            foreach (MaterialTypeDTO material in lstMaterialType)
            {
                IList<AgentDTO> lstAgent = ESalesUnityContainer.Container.Resolve<IAgentService>().GetAgentList();
                if (lstAgent.Count > 0)
                {
                    foreach (AgentDTO item in lstAgent)
                    {
                        DcaMaterialAllocationDTO DCAMaterialAllocation = new DcaMaterialAllocationDTO();

                        DCAMaterialAllocation.DCAMA_Date = System.DateTime.Now.Date.AddDays(0);
                        DCAMaterialAllocation.DCAMA_Agent_Id = item.Agent_Id;
                        DCAMaterialAllocation.DCAMA_MaterialType_Id = material.MaterialType_Id;

                        IList<AgentMaterialPercentageDTO> lstAgentMaterialPercentageDTO = ESalesUnityContainer.Container
                          .Resolve<IAgentMaterialPercentageService>().GetAgentMaterialPercentByAgentId(item.Agent_Id);

                        IList<DcaMaterialAllocationDTO> listMaterial = ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                        .GetMaterialAllocationDetails(material.MaterialType_Id, DateTime.Now.Date.AddDays(-1));
                        DCAMaterialAllocation.DCAMA_TodayPercentage = (from F in lstAgentMaterialPercentageDTO
                                                                        where F.AMP_MaterialType_Id == material.MaterialType_Id
                                                                        select F.AMP_Percentage).FirstOrDefault()
                                                                        +
                                                                        (from F in listMaterial
                                                                         where F.DCAMA_Agent_Id == item.Agent_Id
                                                                         select F.DCAMA_CurrentVariance).FirstOrDefault()
                                                                        ;
                        lstMaterialAllocations.Add(DCAMaterialAllocation);
                    }
                }
            }

            ESalesUnityContainer.Container.Resolve<IDcaMaterialAllocationService>()
                                .SaveAndUpdateDCAMaterialDetails(lstMaterialAllocations);
        }

        private void DeactivateTransporterTrucksNotReportedForOneYear()
        {

        }

        private void DataSynchronization(object stateObject)
		{
            try
            {
				ESalesUnityContainerExtension.InitializeContainer();
				ESalesUnityContainer.Container.Resolve<IMapObject>().CreateMap();
				//Update Daily percentage of DCA for carry forward
                UpdateDCADailyPercentage();

                //De-activate transporter trucks not reported for one year
                DeactivateTransporterTrucksNotReportedForOneYear();
            }
            catch (Exception ex)
            {
            }
		}
	}
}