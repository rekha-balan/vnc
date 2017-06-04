﻿//'------------------------------------------------------------------------------------------------------------------------
//'
//' TODO: Determine how to use the TraceEventType ennumeration.  Vista seems to have more levels.  Windows 2003 fewer.  
//'       Should probably plan for future. Review the mapping levels
//'       Not sure how to get Critical messages to appear in eventlog.  Go look in EntLib code to see if they do mapping.
//'
//'       TraceEventType  Description                         Value   Vista Log   2003 Log    2008 Log    VNC.Log Method    Priority Filter
//'       --------------  ---------------------------------   -----   ---------   --------    --------    ------------    ---------------
//'       Critical        Fatal error or application crash    1       Error       ?           ?           Failure         VNC.Log.Failure   (-10)
//'       Error           Recoverable error                   2       Error                               Error           VNC.Log.Error     (-1)
//'       Warning         Non-Critical Problem                4       Warning                             Warning         VNC.Log.Warning   (1)       
//'       Information     Informational message               8       Information Information ?           Info            VNC.Log.Info      (100)
//'       Verbose         Debugging trace                     16                                          Debug           VNC.Log.Debug     (1000)

//'                                                                                                       Trace           VNC.Log.Trace     (10000)
//'                                                                                                       Trace1          VNC.Log.Trace1    (10001)
//'                                                                                                       Trace2          VNC.Log.Trace2    (10002)
//'                                                                                                       Trace3          VNC.Log.Trace3    (10003)
//'                                                                                                       Trace4          VNC.Log.Trace4    (10004)
//'                                                                                                       Trace5          VNC.Log.Trace5    (10005)
//'
//'       Start          Starting of logical operation        256
//'       Stop           Stopping of logical operation        512
//'       Suspend        Suspension of logical operation      1024
//'       Resume         Resumption of logical operation      2048
//'       Transfer       Changing of correlation identity     4096
//'
//' TO DECIDE:
//'   I don't think we need the methods that don't pass a applicationCategory.  I also think we might want to skip the Info, Debug, and Trace
//'   versions that take a stacktrace flag.
//'   That would leave two versions for Failure and Error and one each for Warning, Info, Debug, and Trace.
//'
//' NOTES:
//'   The methods use the
//'           <System.Diagnostics.DebuggerStepThrough()> _
//'   attribute to suppress tracing through the logging code.  You can still set breakpoints in the method if you want.
//'
//'------------------------------------------------------------------------------------------------------------------------

//''' <summary>
//''' 
//''' </summary>
//''' <remarks>Wrapper around EnterpriseLibrary.Logging.Logger.Write  Simplifies calling by setting values for Priority and Severity.  Passes several extended properties.</remarks>

namespace VNC
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    using Microsoft.Practices.EnterpriseLibrary.Logging;
    //using Microsoft.VisualBasic.CompilerServices;

    [Serializable]
    public class AppLog
    {
        private const long NoElapsedTime = 0L;
        public enum LoggingPriority : int
        {
            Failure = -10,
            Error = -1,
            Warning = 1,
            Info   = 100,
            Debug  = 1000,
            Trace  = 10000,
            Trace1 = 10001,
            Trace2 = 10002,
            Trace3 = 10003,
            Trace4 = 10004,
            Trace5 = 10005,
            Trace6 = 10006,
            Trace7 = 10007,
            Trace8 = 10008,
            Trace9 = 10009
        }

        public enum ShowStack
        {
            No,
            Yes
        }

    #region Public Write Methods

            [DebuggerStepThrough]
        public static void Write(Exception ex, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, bool showStack)
        {
            string name = Assembly.GetCallingAssembly().GetName().Name;
            if (category == "")
            {
                category = "General";
            }
            InternalWrite(ex.Message + ex.StackTrace, severity, category, priority, className, methodName, name, showStack);
        }

        [DebuggerStepThrough]
            public static void Write(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, bool showStack)
        {
            string name = Assembly.GetCallingAssembly().GetName().Name;
            if (category == "")
            {
                category = "General";
            }
            InternalWrite(message, severity, category, priority, className, methodName, name, showStack);
        }

        [DebuggerStepThrough]
        public static void Write(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, bool showStack, long startTicks)
        {
            string name = Assembly.GetCallingAssembly().GetName().Name;
            if (category == "")
            {
                category = "General";
            }
            InternalWrite(message, severity, category, priority, className, methodName, name, showStack, startTicks);
        }

        [DebuggerStepThrough]
        public static void WriteLight(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, bool showStack, long startTicks)
        {
            if (category == "")
            {
                category = "General";
            }
            InternalWrite(message, severity, category, priority, className, methodName, "<unknown>", showStack, startTicks);
        }

    #endregion

    #region Private Write Methods

        [DebuggerStepThrough]
        private static void InternalWrite(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, string callingAssemblyName, bool showStack)
        {
            string str = "";
            if (showStack)
            {
                StackTrace trace = new StackTrace();
                string str2 = "";
                foreach (StackFrame frame in trace.GetFrames())
                {
                    MethodBase method = frame.GetMethod();
                    str2 = method.ReflectedType.Name + "." + method.Name + " > " + str2;
                }
                str = str2 + " " + str;
            }
            LogEntry log = new LogEntry();
            if (category == "")
            {
                category = "General";
            }
            log.Categories.Add(category);
            log.Severity = severity;
            log.Priority = (int)priority;
            log.ExtendedProperties.Add("Calling Assembly", callingAssemblyName);
            log.ExtendedProperties.Add("Class Name", className);
            log.ExtendedProperties.Add("Method Name", methodName);
            log.ExtendedProperties.Add("Stack", str);
            log.ExtendedProperties.Add("User Name", Environment.UserName);
            log.Message = message;
            try
            {
                Logger.Write(log);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }

        [DebuggerStepThrough]
        private static void InternalWrite(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, string callingAssemblyName, bool showStack, int EventId)
        {
            string str = "";
            if (showStack)
            {
                StackTrace trace = new StackTrace();
                string str2 = "";
                foreach (StackFrame frame in trace.GetFrames())
                {
                    MethodBase method = frame.GetMethod();
                    str2 = method.ReflectedType.Name + "." + method.Name + " > " + str2;
                }
                str = str2 + " " + str;
            }
            LogEntry log = new LogEntry();
            if (category == "")
            {
                category = "General";
            }
            log.Categories.Add(category);
            log.EventId = EventId;
            log.Severity = severity;
            log.Priority = (int)priority;
            log.ExtendedProperties.Add("Calling Assembly", callingAssemblyName);
            log.ExtendedProperties.Add("Class Name", className);
            log.ExtendedProperties.Add("Method Name", methodName);
            log.ExtendedProperties.Add("Stack", str);
            log.ExtendedProperties.Add("User Name", Environment.UserName);
            log.Message = message;
            try
            {
                Logger.Write(log);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }

        [DebuggerStepThrough]
        private static void InternalWrite(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, string callingAssemblyName, bool showStack, long startTicks)
        {
            string str = "";
            if (showStack)
            {
                StackTrace trace = new StackTrace();
                string str2 = "";
                foreach (StackFrame frame in trace.GetFrames())
                {
                    MethodBase method = frame.GetMethod();
                    str2 = method.ReflectedType.Name + "." + method.Name + " > " + str2;
                }
                str = str2 + " " + str;
            }
            LogEntry log = new LogEntry();
            if (category == "")
            {
                category = "General";
            }
            log.Categories.Add(category);
            log.Severity = severity;
            log.Priority = (int)priority;
            log.ExtendedProperties.Add("Calling Assembly", callingAssemblyName);
            log.ExtendedProperties.Add("Class Name", className);
            log.ExtendedProperties.Add("Method Name", methodName);
            log.ExtendedProperties.Add("Stack", str);
            log.ExtendedProperties.Add("User Name", Environment.UserName);
            log.ExtendedProperties.Add("Duration", ((double) (Stopwatch.GetTimestamp() - startTicks)) / ((double) Stopwatch.Frequency));
            log.Message = message;
            try
            {
                Logger.Write(log);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }

        [DebuggerStepThrough]
        private static void InternalWrite(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, string callingAssemblyName, bool showStack, Dictionary<string, string> extendedProp)
        {
            string str = "";
            if (showStack)
            {
                StackTrace trace = new StackTrace();
                string str2 = "";
                foreach (StackFrame frame in trace.GetFrames())
                {
                    MethodBase method = frame.GetMethod();
                    str2 = method.ReflectedType.Name + "." + method.Name + " > " + str2;
                }
                str = str2 + " " + str;
            }
            LogEntry log = new LogEntry();
            if (category == "")
            {
                category = "General";
            }
            log.Categories.Add(category);
            log.Severity = severity;
            log.Priority = (int)priority;
            log.ExtendedProperties.Add("Calling Assembly", callingAssemblyName);
            log.ExtendedProperties.Add("Class Name", className);
            log.ExtendedProperties.Add("Method Name", methodName);
            log.ExtendedProperties.Add("Stack", str);
            log.ExtendedProperties.Add("User Name", Environment.UserName);
            foreach (KeyValuePair<string, string> pair in extendedProp)
            {
                log.ExtendedProperties.Add(pair.Key, pair.Value);
            }
            log.Message = message;
            try
            {
                Logger.Write(log);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }

        [DebuggerStepThrough]
        private static void InternalWrite(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, string callingAssemblyName, bool showStack, int EventId, long startTicks)
        {
            string str = "";
            if (showStack)
            {
                StackTrace trace = new StackTrace();
                string str2 = "";
                foreach (StackFrame frame in trace.GetFrames())
                {
                    MethodBase method = frame.GetMethod();
                    str2 = method.ReflectedType.Name + "." + method.Name + " > " + str2;
                }
                str = str2 + " " + str;
            }
            LogEntry log = new LogEntry();
            if (category == "")
            {
                category = "General";
            }
            log.Categories.Add(category);
            log.EventId = EventId;
            log.Severity = severity;
            log.Priority = (int)priority;
            log.ExtendedProperties.Add("Calling Assembly", callingAssemblyName);
            log.ExtendedProperties.Add("Class Name", className);
            log.ExtendedProperties.Add("Method Name", methodName);
            log.ExtendedProperties.Add("Stack", str);
            log.ExtendedProperties.Add("User Name", Environment.UserName);
            log.ExtendedProperties.Add("Duration", ((double) (Stopwatch.GetTimestamp() - startTicks)) / ((double) Stopwatch.Frequency));
            log.Message = message;
            try
            {
                Logger.Write(log);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }

        [DebuggerStepThrough]
        private static void InternalWrite(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, string callingAssemblyName, bool showStack, int EventId, Dictionary<string, string> extendedProp)
        {
            string str = "";
            if (showStack)
            {
                StackTrace trace = new StackTrace();
                string str2 = "";
                foreach (StackFrame frame in trace.GetFrames())
                {
                    MethodBase method = frame.GetMethod();
                    str2 = method.ReflectedType.Name + "." + method.Name + " > " + str2;
                }
                str = str2 + " " + str;
            }
            LogEntry log = new LogEntry();
            if (category == "")
            {
                category = "General";
            }
            log.Categories.Add(category);
            log.EventId = EventId;
            log.Severity = severity;
            log.Priority = (int)priority;
            log.ExtendedProperties.Add("Calling Assembly", callingAssemblyName);
            log.ExtendedProperties.Add("Class Name", className);
            log.ExtendedProperties.Add("Method Name", methodName);
            log.ExtendedProperties.Add("Stack", str);
            log.ExtendedProperties.Add("User Name", Environment.UserName);
            foreach (KeyValuePair<string, string> pair in extendedProp)
            {
                log.ExtendedProperties.Add(pair.Key, pair.Value);
            }
            log.Message = message;
            try
            {
                Logger.Write(log);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }

        [DebuggerStepThrough]
        private static void InternalWrite(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, string callingAssemblyName, bool showStack, long startTicks, Dictionary<string, string> extendedProp)
        {
            string str = "";
            if (showStack)
            {
                StackTrace trace = new StackTrace();
                string str2 = "";
                foreach (StackFrame frame in trace.GetFrames())
                {
                    MethodBase method = frame.GetMethod();
                    str2 = method.ReflectedType.Name + "." + method.Name + " > " + str2;
                }
                str = str2 + " " + str;
            }
            LogEntry log = new LogEntry();
            if (category == "")
            {
                category = "General";
            }
            log.Categories.Add(category);
            log.Severity = severity;
            log.Priority = (int)priority;
            log.ExtendedProperties.Add("Calling Assembly", callingAssemblyName);
            log.ExtendedProperties.Add("Class Name", className);
            log.ExtendedProperties.Add("Method Name", methodName);
            log.ExtendedProperties.Add("Stack", str);
            log.ExtendedProperties.Add("User Name", Environment.UserName);
            log.ExtendedProperties.Add("Duration", ((double)(Stopwatch.GetTimestamp() - startTicks)) / ((double)Stopwatch.Frequency));
            foreach (KeyValuePair<string, string> pair in extendedProp)
            {
                log.ExtendedProperties.Add(pair.Key, pair.Value);
            }
            log.Message = message;
            try
            {
                Logger.Write(log);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }

        [DebuggerStepThrough]
        private static void InternalWrite(string message, TraceEventType severity, string category, LoggingPriority priority, string className, string methodName, string callingAssemblyName, bool showStack, int EventId, long startTicks, Dictionary<string, string> extendedProp)
        {
            string str = "";
            if (showStack)
            {
                StackTrace trace = new StackTrace();
                string str2 = "";
                foreach (StackFrame frame in trace.GetFrames())
                {
                    MethodBase method = frame.GetMethod();
                    str2 = method.ReflectedType.Name + "." + method.Name + " > " + str2;
                }
                str = str2 + " " + str;
            }
            LogEntry log = new LogEntry();
            if (category == "")
            {
                category = "General";
            }
            log.Categories.Add(category);
            log.EventId = EventId;
            log.Severity = severity;
            log.Priority = (int)priority;
            log.ExtendedProperties.Add("Calling Assembly", callingAssemblyName);
            log.ExtendedProperties.Add("Class Name", className);
            log.ExtendedProperties.Add("Method Name", methodName);
            log.ExtendedProperties.Add("Stack", str);
            log.ExtendedProperties.Add("User Name", Environment.UserName);
            log.ExtendedProperties.Add("Duration", ((double)(Stopwatch.GetTimestamp() - startTicks)) / ((double)Stopwatch.Frequency));
            foreach (KeyValuePair<string, string> pair in extendedProp)
            {
                log.ExtendedProperties.Add(pair.Key, pair.Value);
            }
            log.Message = message;
            try
            {
                Logger.Write(log);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }


    #endregion

    #region Public Methods

    #region Log Failure Methods

        [DebuggerStepThrough]
        public static void Failure(Exception ex, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, true);
        }

        [DebuggerStepThrough]
        public static void Failure(Exception ex, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, true, props);
        }

        [DebuggerStepThrough]
        public static void Failure(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
        }

        [DebuggerStepThrough]
        public static void Failure(Exception ex, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(1), EventId);
        }

        [DebuggerStepThrough]
        public static void Failure(string message, string applicationCategory, bool showStack)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, showStack);
        }

        [DebuggerStepThrough]
        public static void Failure(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
        }

        [DebuggerStepThrough]
        public static void Failure(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
        }

        [DebuggerStepThrough]
        public static void Failure(Exception ex, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(1), EventId, props);
        }

        [DebuggerStepThrough]
        public static void Failure(string message, string applicationCategory, bool showStack, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, showStack, props);
        }

        [DebuggerStepThrough]
        public static void Failure(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Critical, applicationCategory, LoggingPriority.Failure, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
        }

    #endregion

    #region Log Error Methods

        [DebuggerStepThrough]
        public static void Error(Exception ex, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, true);
        }

        [DebuggerStepThrough]
        public static void Error(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
        }

        [DebuggerStepThrough]
        public static void Error(Exception ex, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(1), EventId);
        }

        [DebuggerStepThrough]
        public static void Error(Exception ex, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, true, props);
        }

        [DebuggerStepThrough]
        public static void Error(string message, string applicationCategory, bool showStack)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Error, applicationCategory, LoggingPriority.Error,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, showStack);
        }

        [DebuggerStepThrough]
        public static void Error(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
        }

        [DebuggerStepThrough]
        public static void Error(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
        }

        [DebuggerStepThrough]
        public static void Error(Exception ex, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(1), EventId, props);
        }

        [DebuggerStepThrough]
        public static void Error(string message, string applicationCategory, bool showStack, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, showStack, props);
        }

        [DebuggerStepThrough]
        public static void Error(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Error, applicationCategory, LoggingPriority.Error, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
        }

    #endregion

    #region Log Warning Methods

        [DebuggerStepThrough]
        public static void Warning(Exception ex, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, true);
        }

        [DebuggerStepThrough]
        public static void Warning(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
        }

        [DebuggerStepThrough]
        public static void Warning(Exception ex, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(1), EventId);
        }

        [DebuggerStepThrough]
        public static void Warning(Exception ex, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, true, props);
        }

        [DebuggerStepThrough]
        public static void Warning(string message, string applicationCategory, bool showStack)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, showStack);
        }

        [DebuggerStepThrough]
        public static void Warning(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
        }

        [DebuggerStepThrough]
        public static void Warning(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
        }

        [DebuggerStepThrough]
        public static void Warning(Exception ex, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(ex.Message + ex.StackTrace, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(1), EventId, props);
        }

        [DebuggerStepThrough]
        public static void Warning(string message, string applicationCategory, bool showStack, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, showStack, props);
        }

        [DebuggerStepThrough]
        public static void Warning(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Warning, applicationCategory, LoggingPriority.Warning, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
        }

    #endregion

    #region Log Info Methods

        [DebuggerStepThrough]
        public static long Info(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

    #endregion

    #region Log Debug Methods

        [DebuggerStepThrough]
        public static long Debug(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

    #endregion
    
    #region Log Trace Methods

        #region Trace

        [DebuggerStepThrough]
        public static long Trace(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

        #region Trace1

        [DebuggerStepThrough]
        public static long Trace1(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace1,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace1(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace1,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace1(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace1,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace1(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace1,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace1(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace1,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace1(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace1,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace1(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace1,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace1(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace1,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

        #region Trace2

        [DebuggerStepThrough]
        public static long Trace2(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace2,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace2(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace2,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace2(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace2,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace2(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace2,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace2(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace2,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace2(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace2,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace2(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace2,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace2(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace2,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

    #endregion

        #region Trace3

        [DebuggerStepThrough]
        public static long Trace3(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace3,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace3(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace3,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace3(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace3,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace3(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace3,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace3(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace3,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace3(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace3,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace3(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace3,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace3(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace3,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

        #region Trace4

        [DebuggerStepThrough]
        public static long Trace4(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace4,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace4(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace4,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace4(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace4,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace4(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace4,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace4(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace4,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace4(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace4,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace4(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace4,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace4(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace4,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

        #region Trace5

        [DebuggerStepThrough]
        public static long Trace5(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace5,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace5(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace5,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace5(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace5,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace5(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace5,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace5(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace5,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace5(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace5,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace5(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace5,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace5(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace5,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

        #region Trace6

        [DebuggerStepThrough]
        public static long Trace6(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace6,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace6(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace6,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace6(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace6,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace6(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace6,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace6(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace6,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace6(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace6,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace6(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace6,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace6(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace6,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

        #region Trace7

        [DebuggerStepThrough]
        public static long Trace7(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace7,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace7(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace7,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace7(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace7,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace7(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace7,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace7(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace7,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace7(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace7,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace7(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace7,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace7(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace7,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

        #region Trace8

        [DebuggerStepThrough]
        public static long Trace8(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace8,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace8(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace8,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace8(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace8,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace8(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace8,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace8(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace8,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace8(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace8,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace8(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace8,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace8(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace8,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

        #region Trace9

        [DebuggerStepThrough]
        public static long Trace9(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace9,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace9(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace9,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace9(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace9,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace9(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace9,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace9(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace9,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace9(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace9,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace9(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace9,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace9(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace9,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

    #endregion

    #endregion

    }

}
