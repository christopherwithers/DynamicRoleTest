using System.IO;

namespace DynamicRoleTest.Common
{
    using System;
    using System.Diagnostics;
    using System.Web;
    //todo: error with this as every file is having the timing elemnts pasted to the end, so causing parse rrors for .js fles etc. Need to feed out only for .chtml files?
    public class TimingModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
            context.PreSendRequestContent += OnEndRequest;
        }

        static void OnBeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsLocal
                && HttpContext.Current.IsDebuggingEnabled)
            {
                string fileExtension = Path.GetExtension(HttpContext.Current.Request.PhysicalPath);

                if (string.IsNullOrEmpty(fileExtension))
                {
                    var stopwatch = new Stopwatch();
                    HttpContext.Current.Items["Stopwatch"] = stopwatch;
                    stopwatch.Start();
                }
            }
        }

        static void OnEndRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsLocal
                && HttpContext.Current.IsDebuggingEnabled/* && !(new HttpRequestWrapper(HttpContext.Current.Request).IsAjaxRequest())*/)
            {
                string fileExtension = Path.GetExtension(HttpContext.Current.Request.PhysicalPath);

                if (string.IsNullOrEmpty(fileExtension))
                {
                    var stopwatch =
                      (Stopwatch)HttpContext.Current.Items["Stopwatch"];
                    stopwatch.Stop();

                    var ts = stopwatch.Elapsed;
                    var elapsedTime = String.Format("{0}ms", ts.TotalMilliseconds);

                    HttpContext.Current.Response.Write("<p>" + elapsedTime + "</p>");
                }
            }
        }
    }
}