#region Using directives

using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

#endregion Namespaces

namespace TCESS.ESales.Logging
{
    public class CustomLogger
    {
        #region Private Static variables

        private static ILogFormatter formatter = null;
        private static FlatFileTraceListener flatFileTraceListener = null;
        private static LogSource logSource = null;
        public static LogWriter LogWriter { get { return Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Writer; } }
        public static LoggingSettings logsetting = new LoggingSettings();

        #endregion

        #region .ctor

        /// <summary>
        /// This constructor is initializing the text template for the logging file
        /// </summary>
        public CustomLogger()
        {
            formatter = new TextFormatter()
             {
                 Template = "Timestamp: {newline}{newline}{timestamp}{newline}Message: {message}{newline}Category: {category}{newline}Priority: {priority}{newline}Title:{title}{newline}Machine: {localMachine}{newline}App Domain: {localAppDomain}{newline}Process Name: {localProcessName}{newline}Thread Name: {threadName}{newline})}",
             };
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// This method has been used to initialize the objects of the Log Manager class
        /// </summary>
        /// <param name="traceFilePath">full path of trace file</param>
        public static void InitializeLogManager(string traceFilePath)
        {
            if (formatter == null)
            {
                formatter = new TextFormatter()
                {
                    Template = " ------------------------------------------------------{newline}Timestamp:{timestamp}{newline}Message: {message}{newline}Category: {category}{newline}Priority: {priority}{newline}Title:{title}{newline}Machine: {localMachine}{newline}App Domain: {localAppDomain}{newline}Process Name: {localProcessName}{newline}Thread Name: {threadName}{newline}-----------------------------------------)}",
                };
            }

            if (flatFileTraceListener == null)
            {
                flatFileTraceListener = new FlatFileTraceListener(traceFilePath, formatter) { };
            }

            if (logSource == null)
            {
                logSource = new LogSource("Logging", new List<TraceListener>() { flatFileTraceListener }, SourceLevels.Information);
            }
        }

        public static void InitializeLogManager()
        {
            if (formatter == null)
            {
                formatter = new TextFormatter()
                {
                    Template = " ------------------------------------------------------{newline}Timestamp:{timestamp}{newline}Message: {message}{newline}Category: {category}{newline}Priority: {priority}{newline}Title:{title}{newline}Machine: {localMachine}{newline}App Domain: {localAppDomain}{newline}Process Name: {localProcessName}{newline}Thread Name: {threadName}{newline}-----------------------------------------)}",
                };
            }

            if (flatFileTraceListener == null)
            {
                flatFileTraceListener = new FlatFileTraceListener(formatter) { };
            }

            if (logSource == null)
            {
                logSource = new LogSource("Logging", new List<TraceListener>() { flatFileTraceListener }, SourceLevels.Information);
            }
        }

        /// <summary>
        /// This method has been used to log the message into trace file
        /// </summary>
        /// <param name="message">message to log</param>
        public static void WriteLog(string message)
        {
            logSource.TraceData(TraceEventType.Information, 1022, new LogEntry(message, "General", 1, 999,
                TraceEventType.Information, "Logs", null));
        }

        #endregion
    }
}