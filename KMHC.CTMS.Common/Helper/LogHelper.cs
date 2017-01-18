using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using log4net;
using log4net.Config;
using SMT.Foundation.Log;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Reflection;
using System.Configuration;
using System.Net;

namespace Project.Common.Helper
{
    public class LogHelper
    {
        //static Logger log = new Logger("CTMSLog");
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(string msg)
        {
            Tracer.Debug(msg);
            //log.ErrorFormat(msg);
        }

        /// <summary>
        /// 记录Debug信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteDebug(string msg)
        {
            Tracer.Debug(msg);
            //log.DebugFormat(msg);
        }
        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarn(string msg)
        {
            Tracer.Debug(msg);
            //log.WarnFormat(msg);
        }
        /// <summary>
        /// 记录普通信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteInfo(string msg)
        {
            Tracer.Debug(msg);
            //log.InfoFormat(msg);
        }
        /// <summary>
        /// 记录严重错误信息
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteFatal(string msg)
        {
            Tracer.Debug(msg);
            //log.FatalFormat(msg);
        }

        public static string CheckAgent()
        {
            //CallOperation这个头是用来区分ios或者安卓的
            //Version这个头是用来区分医生或者患者端的
            string agent = string.Empty;
            try
            {
                string[] values = HttpContext.Current.Request.Headers.GetValues("CallOperation");
                if (values == null)
                {
                    return "web";
                }
                if (values.Length <= 0)
                {
                    return "web";
                }
                agent = values[0].ToLower();
            }
            catch (Exception ex)
            {
                return "web";
            }

            if (string.IsNullOrEmpty(agent))
            {
                return "web";
            }
            if (agent.Contains("Android".ToLower()))
            {
                string platform = HttpContext.Current.Request.Headers.GetValues("Version")[0].ToLower();
                if (platform.Contains("patient"))
                {
                    return "安卓患者端";
                }
                else if (platform.Contains("doctor"))
                {
                    return "安卓医生端";
                }
                else
                {
                    return "安卓";
                }
                //安卓
            }
            else if (agent.Contains("iOS".ToLower()))
            {
                string platform = HttpContext.Current.Request.Headers.GetValues("Version")[0].ToLower();
                if (platform.Contains("patient"))
                {
                    return "IOS患者端";
                }
                else if (platform.Contains("doctor"))
                {
                    return "IOS医生端";
                }
                else
                {
                    return "IOS";
                }
                //IOS
            }
            return "web";
        }
    }

    public class V_ctms_sys_errorcount2
    {
        public string ErrorId { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string LocalPath { get; set; }
        public string InnerException { get; set; }
        public Nullable<int> Port { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> InputTime { get; set; }
        public string Platform { get; set; }
        public Nullable<int> Type { get; set; }
        public string ErrorName { get; set; }

        public NameValueCollection GetParameters()
        {
            NameValueCollection list = new NameValueCollection();
            foreach (PropertyInfo pi in this.GetType().GetProperties())
            {
                object obj = pi.GetValue(this);
                list.Add("Data[" + pi.Name + "]", obj == null ? string.Empty : obj.ToString());
            }
            return list;
        }
    }

    //internal class Logger
    //{
    //    private ILog Log = null;

    //    /// <summary>
    //    /// Holds the type of formatting for Log4Net
    //    /// </summary>
    //    private enum formatType
    //    {
    //        Debug, Warn, Error, Fatal, Info
    //    }
    //    public Logger(string name, FileInfo configFile)
    //    {
    //        Log = LogManager.GetLogger(name);
    //        if (configFile != null && configFile.Exists)
    //            XmlConfigurator.Configure(configFile);
    //        else
    //            XmlConfigurator.Configure();
    //    }

    //    public Logger(string name)
    //    {
    //        Log = LogManager.GetLogger(name);
    //        XmlConfigurator.Configure();
    //    }

    //    #region DebugFormat
    //    /// <summary>
    //    /// Writes to both console and log4net. Log4net uses Log.Debug
    //    /// </summary>
    //    /// <param name="text"></param>
    //    public void DebugFormat(string text)
    //    {
    //        write(text, formatType.Debug);
    //    }
    //    #endregion

    //    #region WarnFormat
    //    /// <summary>
    //    /// Writes to both console and log4net. Log4net uses Log.Error.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    public void WarnFormat(string text)
    //    {
    //        write(text, formatType.Warn);
    //    }
    //    #endregion

    //    #region ErrorFormat
    //    /// <summary>
    //    /// Writes to both console and log4net. Log4net uses Log.Error.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    public void ErrorFormat(string text)
    //    {
    //        write(text, formatType.Error);
    //    }
    //    #endregion

    //    #region FatalFormat
    //    /// <summary>
    //    /// Writes to both console and log4net. Log4net uses Log.Fatal.
    //    /// </summary>
    //    /// <param name="text"></param>
    //    public void FatalFormat(string text)
    //    {
    //        write(text, formatType.Fatal);
    //    }
    //    #endregion

    //    #region InfoFormat
    //    /// <summary>
    //    /// Writes to both console and log4net. Log4net uses Log.Info
    //    /// </summary>
    //    /// <param name="text"></param>
    //    public void InfoFormat(string text)
    //    {
    //        write(text, formatType.Info);
    //    }

    //    public void InfoFormat(string text, object arg0)
    //    {
    //        string formattedString = string.Format(text, arg0);
    //        write(formattedString, formatType.Info);
    //    }

    //    public void InfoFormat(string text, object arg0, object arg1)
    //    {
    //        string formattedString = string.Format(text, arg0, arg1);
    //        write(formattedString, formatType.Info);
    //    }

    //    public void InfoFormat(string text, object arg0, object arg1, object arg2)
    //    {
    //        string formattedString = string.Format(text, arg0, arg1, arg2);
    //        write(formattedString, formatType.Info);
    //    }

    //    public void InfoFormat(string text, List<object> args)
    //    {
    //        string formattedString = string.Format(text, args);
    //        write(formattedString, formatType.Info);
    //    }
    //    #endregion

    //    #region Write
    //    /// <summary>
    //    /// Writes to both console and log4net. Uses type to determine which type of logging to use.
    //    /// </summary>
    //    /// <param name="text">Text to write to console and log4net.</param>
    //    /// <param name="incomingType">Type to use for log4net.</param>
    //    private void write(string text, formatType incomingType)
    //    {
    //        string now = DateTime.Now.ToString();
    //        //Add time stamp to the console.
    //        Console.WriteLine(String.Format("{0} [{1}] {2}", now, incomingType.ToString(), text));

    //        //Log out to log4net if configuration was present
    //        switch (incomingType)
    //        {
    //            case formatType.Debug:
    //                System.Diagnostics.Trace.WriteLine(String.Format("[{0}]{1}", now, text));
    //                Log.Debug(text);
    //                break;

    //            case formatType.Warn:
    //                System.Diagnostics.Trace.TraceWarning(String.Format("[{0}]{1}", now, text));
    //                Log.Warn(text);
    //                break;

    //            case formatType.Error:
    //                System.Diagnostics.Trace.TraceError(String.Format("[{0}]{1}", now, text));
    //                Log.Error(text);
    //                break;

    //            case formatType.Fatal:
    //                System.Diagnostics.Trace.TraceError(String.Format("[{0}]{1}", now, text));
    //                Log.Fatal(text);
    //                break;

    //            case formatType.Info:
    //                System.Diagnostics.Trace.TraceInformation(String.Format("[{0}]{1}", now, text));
    //                Log.Info(text);
    //                break;
    //        }
    //    }
    //    #endregion
    //}
}