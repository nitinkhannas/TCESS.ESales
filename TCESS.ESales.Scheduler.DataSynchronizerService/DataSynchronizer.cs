using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Net;

namespace TCESS.ESales.Scheduler.DataSynchronizerService
{
    public class DataSynchronizer
    {
        #region Private Member Variables

        private TimerCallback _timerDelegate;
        private Timer _stateTimer;

        #endregion
				
        public void InitializeTimerControl()
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
                //CustomLogger.WriteLog(ex.Message);
            }
        }

        private void DataSynchronization(object stateObject)
        {
			try
			{
				System.Diagnostics.EventLog.WriteEntry("Execution Started", "Ok");
				UpdationService.SMSServiceClient serviceClient = new UpdationService.SMSServiceClient();
				string result= serviceClient.UpdateDCAPercentage();
				System.Diagnostics.EventLog.WriteEntry(result, "Ok");
				//WebRequest wr = WebRequest.Create("http://127.0.0.1/ESalesApplication/DataUpdation.aspx");
			}
			catch (Exception ex)
			{
			}
        }
    }
}