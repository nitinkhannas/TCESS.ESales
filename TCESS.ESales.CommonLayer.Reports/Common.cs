using Microsoft.Reporting.WebForms;

namespace TCESS.ESales.CommonLayer.Reports
{
    public static class Common
    {
        /// <summary>
        /// Sets the embedded resource for a report
        /// </summary>
        public static void SetReportEmbeddedResource(ReportViewer viewer, string reportName)
        {
            viewer.LocalReport.ReportEmbeddedResource = reportName;
        }
    }
}