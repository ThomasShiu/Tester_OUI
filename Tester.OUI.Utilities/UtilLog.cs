using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;
using System.Diagnostics;
using System.Reflection;

namespace Tester.OUI.Utilities
{
    public sealed class UtilLog
    {
        private static readonly ILog logger = LogManager.GetLogger("tracelog");
        private static readonly ILog secsLogger = LogManager.GetLogger("secslog");

        static UtilLog()
        {
            XmlConfigurator.Configure();
        }

        private UtilLog()
        {

        }

        public static void Debug(string msg)
        {
            logger.Debug(msg);
        }

        public static void Debug(string msg, Exception ex)
        {
            logger.Debug(msg, ex);
        }

        public static void DebugFormat(string format, params string[] args)
        {
            logger.DebugFormat(format, args);
        }

        public static void Info(string msg)
        {
            logger.Info(msg);
        }

        public static void SecsInfo(string msg)
        {
            secsLogger.Info(msg);
        }

        public static void Info(string msg, Exception ex)
        {
            logger.Info(msg, ex);
        }

        public static void InfoFormat(string format, params string[] args)
        {
            logger.InfoFormat(format, args);
        }

        public static void Warn(string msg)
        {
            logger.Warn(msg);
        }

        public static void Warn(string msg, Exception ex)
        {
            logger.Warn(msg, ex);
        }

        public static void WarnFormat(string format, params string[] args)
        {
            logger.WarnFormat(format, args);
        }

        public static void Error(string msg)
        {
            logger.Error(msg);
        }

        public static void Error(string msg, Exception ex)
        {
            logger.Error(msg, ex);
        }

        public static void ErrorFormat(string format, params string[] args)
        {
            logger.ErrorFormat(format, args);
        }

        public static void Error(Exception ex)
        {
            if (ex != null)
            {
                StackTrace st = new StackTrace(ex);

                StringBuilder sb = new StringBuilder();

                int frameCount = st.FrameCount;

                for (int frameIndex = frameCount - 1; frameIndex >= 0; frameIndex--)
                {
                    StackFrame sf = st.GetFrame(frameIndex);

                    MethodBase mb = (MethodBase)sf.GetMethod();

                    if (frameIndex == frameCount - 1)
                    {
                        sb.AppendFormat("[{0}.{1}]", mb.DeclaringType, mb.Name);
                    }
                    else
                    {
                        sb.AppendFormat(" --> [{0}.{1}]", mb.DeclaringType, mb.Name);
                    }
                }

                sb.AppendFormat(" [{0}] : {1}", ex.Source, ex.Message);

                logger.Error(sb.ToString().Trim());
            }
        }

        public static void Fatal(string msg)
        {
            logger.Fatal(msg);
        }

        public static void Fatal(string msg, Exception ex)
        {
            logger.Fatal(msg, ex);
        }

        public static void FatalFormat(string format, params string[] args)
        {
            logger.FatalFormat(format, args);
        }
    }
}
