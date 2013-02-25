#region Using directives

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.Unity;
using TCESS.ESales.CommonLayer.Unity;

#endregion

namespace TCESS.ESales.CommonLayer.Exception
{
    public class ExceptionHandler
    {
        private static ExceptionManager AppErrorManager;

        /// <summary>
        /// This property has been used to get the instance of Unity Exception Manager
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public static ExceptionManager AppExceptionManager
        {
            get
            {
                return AppErrorManager;
            }            
        }

        /// <summary>
        /// This method has been used to initialize the exception manager for the complete application
        /// </summary>
        /// <remarks></remarks>
        /// </summary>
        public static void InitializeExceptionManager()
        {
            ESalesUnityContainer.Container.AddNewExtension<EnterpriseLibraryCoreExtension>();
            AppErrorManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();
        }
    }
}