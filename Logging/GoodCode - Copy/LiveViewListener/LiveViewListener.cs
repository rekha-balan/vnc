using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

using VNC.Logging.CustomTraceListeners.ServiceReference1;

namespace VNC.Logging.CustomTraceListeners
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class LiveViewListener : Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners.CustomTraceListener
    {
        private string sLoggingDbConString = string.Empty;
        private int iSQLCommandTimeoutInSecs = 300;
        private string sEntryID = string.Empty;
        private string sMachineName = string.Empty;
        private string sUserName = string.Empty;
        private string sApplicationName = string.Empty;
        private string sCategoryName = string.Empty;
        private string sSeverity_TC = string.Empty;
        private string sMessage_Text = string.Empty;
        private string sSessionID = string.Empty;
        private string sThreadID = string.Empty;
        private string sExecutableName = string.Empty;
        private string sCallstack = string.Empty;
        private double dPerformance = 0.00;
        private int iStepInt = 1;
        private int iEventID = 0;

        private LiveViewClient client = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public LiveViewListener()
        {
            client = new LiveViewClient();
        }
        /// <summary>
        /// List of Attributes allowed
        /// </summary>
        /// <returns></returns>
        protected override string[] GetSupportedAttributes()
        {
            return new string[] { "LoggingDbConString", "SQLCommandTimeout" };
            //return base.GetSupportedAttributes();
        }
        public override void Write(string message)
        {
            Debug.Write(message);
            client.DisplayLogEntry(message);
        }

        public override void WriteLine(string message)
        {
            client.DisplayLogEntry(message);

            Debug.WriteLine(message);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            //Dictionary<String, Object> dictionary = new Dictionary<String, Object>();

            ////Get Logging database connection string
            //if (Attributes.ContainsKey("LoggingDbConString") == true)
            //{
            //    sLoggingDbConString = Attributes["LoggingDbConString"];
            //}
            //else
            //{
            //    throw new LoggingException("Logging Database Connection String is missing");
            //}
            ////Get SQL Command Timeout in seconds
            //if (Attributes.ContainsKey("SQLCommandTimeout") == true)
            //{
            //    iSQLCommandTimeoutInSecs = Convert.ToInt32(Attributes["SQLCommandTimeout"]);
            //}
            //else
            //{
            //    //use default value
            //    iSQLCommandTimeoutInSecs = 300;
            //}
            if (data is LogEntry && this.Formatter != null)
            {
                try
                {
                    LogEntry e = (LogEntry)data;

                    this.sEntryID = System.Guid.NewGuid().ToString();
                    this.sMachineName = e.MachineName;
                    this.sApplicationName = e.CategoriesStrings[0];
                    this.iEventID = e.EventId;
                    this.sCategoryName = e.CategoriesStrings[0];
                    this.sSeverity_TC = e.Severity.ToString();
                    this.sMessage_Text = e.Message;
                    this.sThreadID = e.Win32ThreadId;

                    foreach (KeyValuePair<string, Object> kvp in e.ExtendedProperties)
                    {
                        switch (kvp.Key.ToString())
                        {
                            case "User Name":
                                {
                                    this.sUserName = kvp.Value.ToString();
                                    break;
                                }
                            case "Calling Assembly":
                                {

                                    this.sExecutableName = kvp.Value.ToString();
                                    break;
                                }
                            case "Stack":
                                {
                                    this.sCallstack = kvp.Value.ToString();
                                    break;
                                }
                            case "Duration":
                                {
                                    this.dPerformance = Convert.ToDouble(kvp.Value);
                                    break;
                                }
                            default:
                                {
                                    //dictionary.Add(kvp.Key.ToString(), kvp.Value);
                                    break;
                                }
                        }
                    }

                    this.WriteLine(this.Formatter.Format(data as LogEntry));
                    //this.WriteLine(this.Formatter.Format(data as LogEntry));
                    //Insert Log entry
                    //InsertLogEntryIntoDb();

                    //foreach (KeyValuePair<string, Object> kvp in dictionary)
                    //{
                    //    InsertExtraData(kvp.Key, kvp.Value.ToString());
                    //}
                }
                catch (Exception ex)
                {
                    throw new LoggingException(ex.Message);
                    //EventLog.WriteEntry("CustomDatabaseTraceListener", ex.Message, EventLogEntryType.Information);

                }
                finally
                {
                    //dictionary.Clear();
                    //dictionary = null;
                }
            }
            else
            {
                this.Write("not LogEntry");
                this.WriteLine(data.ToString());
                //Not a LogEntry. Ignore
            }
        }

        private bool InsertLogEntryIntoDb()
        {
            bool bRetVal = false;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                //Open SQL connection 
                conn = new SqlConnection(sLoggingDbConString);
                conn.Open();

                //Set command options
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = iSQLCommandTimeoutInSecs;

                //Set stored procedure
                cmd.CommandText = "sp_logging_add_log_entry";

                cmd.Parameters.Add(new SqlParameter("@entry_id", this.sEntryID));
                cmd.Parameters.Add(new SqlParameter("@writeDate", System.DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@machine_name", this.sMachineName));
                cmd.Parameters.Add(new SqlParameter("@user_name", this.sUserName));
                cmd.Parameters.Add(new SqlParameter("@application_name", this.sApplicationName));
                cmd.Parameters.Add(new SqlParameter("@category_name", this.sCategoryName));
                cmd.Parameters.Add(new SqlParameter("@severity_tc", this.sSeverity_TC.Substring(0, 1)));
                cmd.Parameters.Add(new SqlParameter("@error_number", 9999));
                cmd.Parameters.Add(new SqlParameter("@message_txt", this.sMessage_Text));
                cmd.Parameters.Add(new SqlParameter("@session_id", this.sSessionID));
                cmd.Parameters.Add(new SqlParameter("@thread_id", this.sThreadID));
                cmd.Parameters.Add(new SqlParameter("@executable_name", this.sExecutableName));
                cmd.Parameters.Add(new SqlParameter("@callstack", this.sCallstack));
                if (dPerformance != 0.00)
                {
                    cmd.Parameters.Add(new SqlParameter("@performance", dPerformance));
                }
                cmd.Parameters.Add(new SqlParameter("@step_int", 1));
                if (iEventID != 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@event_id", iEventID));
                }
                //Execute Query 
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                bRetVal = true;
            }
            catch (Exception ex)
            {
                throw new LoggingException(ex.Message);
                //EventLog.WriteEntry("CustomDatabaseTraceListener", ex.Message, EventLogEntryType.Error);
            }
            finally
            {

                cmd = null;

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }

            }
            return bRetVal;
        }

        private bool InsertExtraData(string sKey, string sValue)
        {
            bool bRetVal = false;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                //Open SQL connection 
                conn = new SqlConnection(sLoggingDbConString);
                conn.Open();

                //Set command options
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = iSQLCommandTimeoutInSecs;

                //Set stored procedure
                cmd.CommandText = "sp_logging_add_extra_data";
                cmd.Parameters.Add(new SqlParameter("@entry_id", this.sEntryID));
                cmd.Parameters.Add(new SqlParameter("@name", sKey));
                cmd.Parameters.Add(new SqlParameter("@value", sValue));

                //Execute Query 
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();

                bRetVal = true;
            }
            catch (Exception ex)
            {
                throw new LoggingException(ex.Message);
                //EventLog.WriteEntry("CustomDatabaseTraceListener", ex.Message, EventLogEntryType.Error);
            }
            finally
            {

                cmd = null;

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }

            }
            return bRetVal;

        }
    }
}
