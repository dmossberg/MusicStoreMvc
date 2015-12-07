using Microsoft.ApplicationInsights;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using MvcMusicStore.Logging;
using MvcMusicStore.Proxy;
using StackExchange.Profiling;
using System;
using System.Diagnostics.Tracing;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ObservableEventListener listener;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            EnableLoggingListener();
        }

        private void EnableLoggingListener()
        {
            listener = new ObservableEventListener();
            listener.LogToWindowsAzureTable("CustomEvents", "DefaultEndpointsProtocol=https;AccountName=musiccloudstorage;AccountKey=biS7z9k5sN8th89411jiTIv3s000KcykkjNMT7a8tHtuYj8BQra21tJV/wxNhJSFEcW5c5ElQFs0W/Ao97UO+w==");
            //listener.EnableEvents(AuditEvent.Log, EventLevel.LogAlways, Keywords.All);
            listener.EnableEvents(ErrorEvent.Log, EventLevel.LogAlways, Keywords.All);
        }

        protected void Application_End()
        {
            DisableLoggingListener();
        }

        private void DisableLoggingListener()
        {
            //listener.DisableEvents(AuditEvent.Log);
            listener.Dispose();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            TelemetryClient telemetry = new TelemetryClient();

            var exception = Server.GetLastError();
            while (exception.InnerException != null)
                exception = exception.InnerException;

            string exceptionType = exception.GetType().ToString();
            string exceptionMessage = exception.Message;
            string stackTrace = exception.StackTrace;

            switch( exceptionType )
            {
                case "System.Data.SqlClient.SqlException" :
                    ErrorEvent.Log.DatabaseError( exceptionType, exceptionMessage, stackTrace);
                    break;
                case "MvcMusicStore.Proxy.ServiceCallException":
                    string serviceUrl = ((ServiceCallException)exception).ServiceUrl;
                    ErrorEvent.Log.ServiceCallError(exceptionType, exceptionMessage, stackTrace, serviceUrl);
                    break;
                default:
                    ErrorEvent.Log.ExcepcionNoManejada(exceptionType, exceptionMessage, stackTrace);
                    break;
            }
            //Server.ClearError();
        }

        //Enable MiniProfiler

        //protected void Application_BeginRequest()
        //{
        //    if (Request.IsLocal)
        //    {
        //        MiniProfiler.Start();
        //    }
        //}

        //protected void Application_EndRequest()
        //{
        //    MiniProfiler.Stop();
        //}
    }
}