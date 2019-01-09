﻿using JW.DataRelay.Framework.Web;
using log4net;
using System;
using System.Text;
using System.Web;

namespace JW.DataRelay.Framework.Logging
{
    public partial class Log4netHelper
    {
        private static object _lock = new object();

        private static ILog _infoLogger = null;
        private static ILog infoLogger
        {
            get
            {
                if (_infoLogger == null)
                {
                    lock (_lock)
                    {
                        if (_infoLogger == null)
                        {
                            _infoLogger = LogManager.GetLogger("InfoLogger");
                        }
                    }
                }
                return _infoLogger;
            }
        }

        private static ILog _fatalLogger = null;
        private static ILog fatalLogger
        {
            get
            {
                if (_fatalLogger == null)
                {
                    lock (_lock)
                    {
                        if (_fatalLogger == null)
                        {
                            _fatalLogger = LogManager.GetLogger("FatalLogger");
                        }
                    }
                }
                return _fatalLogger;
            }
        }

        private static ILog _debugLogger = null;
        private static ILog debugLogger
        {
            get
            {
                if (_debugLogger == null)
                {
                    lock (_lock)
                    {
                        if (_debugLogger == null)
                        {
                            _debugLogger = LogManager.GetLogger("DebugLogger");
                        }
                    }
                }
                return _debugLogger;
            }
        }

        private static string Ip
        {
            get
            {
                try
                {
                    return HttpHelper.IP;
                }
                catch (Exception ex)
                {
                    return "0.0.0.0";
                }
            }
        }

        private static string RequestType
        {
            get
            {
                try
                {
                    return HttpContext.Current.Request.RequestType;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        private static string Url
        {
            get
            {
                try
                {
                    return HttpContext.Current.Request.Url.AbsolutePath;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        private static string UserAgent
        {
            get
            {
                try
                {
                    return HttpContext.Current.Request.UserAgent;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 获取日志记录的完整前缀信息
        /// </summary>cache_item
        /// <param name="msg"></param>
        /// <returns></returns>
        private static string GetLogCompleteHeaders(string msg)
        {
            return string.Format("[{0}] \"{1} {2}\" {3} \"{4}\"", Ip, RequestType, Url, msg, UserAgent);
        }

        /// <summary>
        /// 获取日志记录的基础前缀信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static string GetLogBasicHeaders(string msg)
        {
            return string.Format("[{0}] \"{1} {2}\" {3}", Ip, RequestType, Url, msg);
        }

        /// <summary>
        /// 程序运行时执行
        /// </summary>
        /// <param name="log4netConfigPath">log4net.config文件的绝对路径</param>
        public static void Application_Start(string log4netConfigPath)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(log4netConfigPath));
        }

        #region Info 记录信息

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
#if DEBUG
            Console.WriteLine(msg);
#endif
            infoLogger.Info(msg);
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Info(string msg, Exception ex)
        {
#if DEBUG
            Console.WriteLine(msg);
            Console.WriteLine(ex);
#endif
            infoLogger.Info(msg, ex);
        }

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="method">方法</param>
        /// <param name="param">相关参数以及说明</param>
        public static void Info(string msg, string method = null, object param = null)
        {
            Info(string.Format("{1}{0}方法：{2}{0}参数：{3}", Environment.NewLine, msg, method, param.ToJson()));
        }

        #endregion

        #region Fatal 致命错误

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            fatalLogger.Fatal(GetLogCompleteHeaders(msg));
        }

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="ex"></param>
        public static void Fatal(Exception ex)
        {
            Exception original_ex = ex;
            while (original_ex.InnerException != null)
            {
                fatalLogger.Fatal(original_ex);
                original_ex = original_ex.InnerException;
            }
            fatalLogger.Fatal(ex);
        }

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(string msg, Exception ex)
        {
            Exception original_ex = ex;
            while (original_ex.InnerException != null)
            {
                fatalLogger.Fatal(original_ex);
                original_ex = original_ex.InnerException;
            }
            fatalLogger.Fatal(GetLogCompleteHeaders(msg), ex);
        }
        #endregion

        #region Debug 调试 记录程序运行和终止运行情况

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
#if DEBUG
            Console.WriteLine(msg);
#endif
            debugLogger.Debug(msg);
        }

        public static void Debug(params object[] args)
        {
            var msg = new StringBuilder();
            foreach (var arg in args)
            {
                msg.Append(arg.ToJson());
            }
            Debug(msg.ToString());
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(string msg, Exception ex)
        {
#if DEBUG
            Console.WriteLine(msg);
            Console.WriteLine(ex);
#endif
            debugLogger.Debug(GetLogCompleteHeaders(msg), ex);
        }
        #endregion

        /// <summary>
        /// 序列化ex.Data
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string SerializeData(Exception ex)
        {
            if (ex.Data != null && ex.Data.Count > 0)
            {
                return string.Format("[{0}]", Newtonsoft.Json.JsonConvert.SerializeObject(ex.Data));
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取真实异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Exception GetRealException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return GetRealException(ex.InnerException);
            }
            else
            {
                return ex;
            }
        }
    }
}
