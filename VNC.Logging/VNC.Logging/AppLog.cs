//'------------------------------------------------------------------------------------------------------------------------
//'
//' TODO: Determine how to use the TraceEventType enumeration.  Vista seems to have more levels.  Windows 2003 fewer.  
//'       Should probably plan for future. Review the mapping levels
//'       Not sure how to get Critical messages to appear in eventlog.  Go look in EntLib code to see if they do mapping.
//'
//'       TraceEventType  Description                         Value   Vista Log   2003 Log    2008 Log    VNC.Log Method	Priority Filter
//'       --------------  ---------------------------------   -----   ---------   --------    --------    ------------		---------------
//'       Critical        Fatal error or application crash    1       Error       ?           ?           Failure			VNC.Log.Failure		(-10)
//'       Error           Recoverable error                   2       Error                               Error				VNC.Log.Error		(-1)
//'       Warning         Non-Critical Problem                4       Warning                             Warning			VNC.Log.Warning		(1)       
//'       Information     Informational message               8       Information Information ?           Info				VNC.Log.Info		(100)
//'																										  Info1				VNC.Log.Info1		(101)
//'																										  Info2				VNC.Log.Info2		(102)
//'																										  Info3				VNC.Log.Info3		(103)
//'																										  Info4				VNC.Log.Info4		(104)
//'																										  Info5				VNC.Log.Info5		(105)
//'       Verbose         Debugging trace                     16                                          Debug				VNC.Log.Debug		(1000)
//'																										  Debug1			VNC.Log.Debug1		(101)
//'																										  Debug2			VNC.Log.Debug2		(102)
//'																										  Debug3			VNC.Log.Debug3		(103)
//'																										  Debug4			VNC.Log.Debug4		(104)
//'																										  Debug5			VNC.Log.Debug5		(105)
//'                                                                                                       Trace				VNC.Log.Trace     (10000)
//'                                                                                                       Trace1			VNC.Log.Trace1		(10001)
//'                                                                                                       Trace2			VNC.Log.Trace2		(10002)
//'                                                                                                       Trace3			VNC.Log.Trace3		(10003)
//'                                                                                                       Trace4			VNC.Log.Trace4		(10004)
//'                                                                                                       Trace5			VNC.Log.Trace5		(10005)
//'                                                                                                       Trace6			VNC.Log.Trace6		(10006)
//'                                                                                                       Trace7			VNC.Log.Trace7		(10007)
//'                                                                                                       Trace8			VNC.Log.Trace8		(10008)
//'                                                                                                       Trace9			VNC.Log.Trace9		(10009)
//'                                                                                                       Trace10			VNC.Log.Trace10		(100010)
//'                                                                                                       Trace11			VNC.Log.Trace11		(100011)
//'                                                                                                       Trace12			VNC.Log.Trace12		(100012)
//'                                                                                                       Trace13			VNC.Log.Trace13		(100013)
//'                                                                                                       Trace14			VNC.Log.Trace14		(100014)
//'                                                                                                       Trace15			VNC.Log.Trace15		(100015)
//'                                                                                                       Trace16			VNC.Log.Trace16		(100016)
//'                                                                                                       Trace17			VNC.Log.Trace17		(100017)
//'                                                                                                       Trace18			VNC.Log.Trace18		(100018)
//'                                                                                                       Trace19			VNC.Log.Trace19		(100019)
//'                                                                                                       Trace20			VNC.Log.Trace20		(100020)
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

/// <summary>
/// Low level routines used by client and web applications
/// 
/// </summary>
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
            Failure		= -10,
            Error		= -1,
            Warning		= 1,
            Info		= 100,
			Info1		= 101,
			Info2		= 102,
			Info3		= 103,
			Info4		= 104,
			Info5		= 105,
            Debug		= 1000,
			Debug1		= 1001,
			Debug2		= 1002,
			Debug3		= 1003,
			Debug4		= 1004,
			Debug5		= 1005,
            Trace		= 10000,
			Trace1		= 10001,
			Trace2		= 10002,
			Trace3		= 10003,
			Trace4		= 10004,
			Trace5		= 10005,
			Trace6		= 10006,
			Trace7		= 10007,
			Trace8		= 10008,
			Trace9		= 10009,
			Trace10		= 10010,
			Trace11		= 10011,
			Trace12		= 10012,
			Trace13		= 10013,
			Trace14		= 10014,
			Trace15		= 10015,
			Trace16		= 10016,
			Trace17		= 10017,
			Trace18		= 10018,
			Trace19		= 10019,
			Trace20		= 10020,
			Max			= 10020
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

		#region Info

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
		#region Info1

        [DebuggerStepThrough]
        public static long Info1(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info1(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info1(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info1(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info1(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info1(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info1(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info1(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
		#region Info2

        [DebuggerStepThrough]
        public static long Info2(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info2(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info2(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info2(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info2(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info2(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info2(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info2(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
		#region Info3

        [DebuggerStepThrough]
        public static long Info3(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info3(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info3(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info3(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info3(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info3(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info3(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info3(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
		#region Info4

        [DebuggerStepThrough]
        public static long Info4(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info4(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info4(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info4(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info4(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info4(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info4(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info4(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
		#region Info5

        [DebuggerStepThrough]
        public static long Info5(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info5(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info5(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info5(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info5(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info5(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info5(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Info5(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Information, applicationCategory, LoggingPriority.Info5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
	#endregion

    #region Log Debug Methods

	
		#region Debug

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
	
		#region Debug1

        [DebuggerStepThrough]
        public static long Debug1(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug1(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug1(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug1(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug1(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug1(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug1(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug1(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug1, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
	
		#region Debug2

        [DebuggerStepThrough]
        public static long Debug2(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug2(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug2(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug2(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug2(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug2(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug2(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug2(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug2, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
	
		#region Debug3

        [DebuggerStepThrough]
        public static long Debug3(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug3(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug3(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug3(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug3(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug3(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug3(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug3(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug3, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
	
		#region Debug4

        [DebuggerStepThrough]
        public static long Debug4(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug4(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug4(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug4(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug4(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug4(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug4(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug4(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug4, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion
	
		#region Debug5

        [DebuggerStepThrough]
        public static long Debug5(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug5(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug5(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug5(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug5(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug5(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug5(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Debug5(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Debug5, 
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

		#endregion

#endregion

	#region Trace Log Methods

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

		#region Trace10

        [DebuggerStepThrough]
        public static long Trace10(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace10,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace10(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace10,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace10(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace10,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace10(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace10,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace10(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace10,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace10(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace10,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace10(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace10,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace10(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace10,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace11

        [DebuggerStepThrough]
        public static long Trace11(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace11,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace11(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace11,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace11(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace11,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace11(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace11,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace11(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace11,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace11(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace11,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace11(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace11,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace11(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace11,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace12

        [DebuggerStepThrough]
        public static long Trace12(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace12,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace12(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace12,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace12(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace12,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace12(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace12,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace12(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace12,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace12(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace12,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace12(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace12,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace12(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace12,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace13

        [DebuggerStepThrough]
        public static long Trace13(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace13,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace13(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace13,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace13(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace13,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace13(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace13,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace13(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace13,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace13(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace13,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace13(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace13,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace13(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace13,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace14

        [DebuggerStepThrough]
        public static long Trace14(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace14,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace14(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace14,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace14(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace14,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace14(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace14,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace14(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace14,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace14(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace14,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace14(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace14,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace14(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace14,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace15

        [DebuggerStepThrough]
        public static long Trace15(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace15,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace15(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace15,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace15(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace15,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace15(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace15,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace15(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace15,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace15(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace15,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace15(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace15,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace15(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace15,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace16

        [DebuggerStepThrough]
        public static long Trace16(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace16,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace16(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace16,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace16(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace16,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace16(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace16,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace16(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace16,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace16(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace16,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace16(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace16,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace16(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace16,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace17

        [DebuggerStepThrough]
        public static long Trace17(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace17,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace17(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace17,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace17(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace17,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace17(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace17,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace17(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace17,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace17(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace17,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace17(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace17,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace17(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace17,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace18

        [DebuggerStepThrough]
        public static long Trace18(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace18,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace18(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace18,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace18(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace18,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace18(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace18,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace18(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace18,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace18(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace18,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace18(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace18,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace18(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace18,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace19

        [DebuggerStepThrough]
        public static long Trace19(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace19,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace19(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace19,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace19(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace19,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace19(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace19,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace19(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace19,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace19(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace19,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace19(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace19,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace19(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace19,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion

		#region Trace20

        [DebuggerStepThrough]
        public static long Trace20(string message, string applicationCategory)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace20,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace20(string message, string applicationCategory, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace20,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, false, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace20(string message, string applicationCategory, int EventId)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace20,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace20(string message, string applicationCategory, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace20,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace20(string message, string applicationCategory, int EventId, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace20,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace20(string message, string applicationCategory, int EventId, long startTicks)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace20,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace20(string message, string applicationCategory, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace20,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        [DebuggerStepThrough]
        public static long Trace20(string message, string applicationCategory, int EventId, long startTicks, Dictionary<string, string> props)
        {
            StackTrace trace = new StackTrace();
            MethodBase method = trace.GetFrame(1).GetMethod();
            InternalWrite(message, TraceEventType.Verbose, applicationCategory, LoggingPriority.Trace20,
                method.ReflectedType.Name, method.Name, Assembly.GetCallingAssembly().GetName().Name, Convert.ToBoolean(0), EventId, startTicks, props);
            return Stopwatch.GetTimestamp();
        }

        #endregion


	#endregion

    }
}