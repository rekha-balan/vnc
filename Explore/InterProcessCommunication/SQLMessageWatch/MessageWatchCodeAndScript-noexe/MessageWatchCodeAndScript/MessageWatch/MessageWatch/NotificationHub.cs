using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MessageWatch
{
    public class NotificationHub : Hub
    {
        //public void Send(string Name, string EventID, string LogLevelName, string OperationCodeName, string ServerName, string ComponentName, string SubComponentName)
        //{
        //    // This will brodcast message to all the clients
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //    context.Clients.All.broadcastMessage(Name, EventID, LogLevelName, OperationCodeName, ServerName, ComponentName, SubComponentName);

        //}

        /// <summary>
        /// This function will be called from client to get all the messages.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MessageLog> GetAllMessagesLog()
        {
            string conStr = "Data Source=HP;Initial Catalog=SignalRDatabase;Trusted_Connection=true;";
            SqlConnection connection = new SqlConnection(conStr);

            string query = "SELECT Message,EventID,LL.Name as LogLevelID,OC.Name as OperationCodeID,ML.ServerName,ML.ComponentName,ML.SubComponentName FROM [dbo].[MessageLog] ML inner join [dbo].[LogLevel] LL on ML.LogLevelID = LL.ID inner join [dbo].[OperationCode] OC on ML.OperationCodeID = OC.ID";
            SqlDependency.Start(conStr);
            SqlCommand command = new SqlCommand(query, connection);
            SqlDependency dependency = new SqlDependency(command);

            //If Something will change in database and it will call dependency_OnChange method.
            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<MessageLog> messageList = new List<MessageLog>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MessageLog ml = new MessageLog();
                ml.Name = dt.Rows[i]["Message"].ToString();
                ml.EventID = Convert.ToInt32(dt.Rows[i]["EventID"].ToString());
                ml.LogLevelName = dt.Rows[i]["LogLevelID"].ToString();
                ml.OperationCodeName = dt.Rows[i]["OperationCodeID"].ToString();
                ml.ServerName = dt.Rows[i]["ServerName"].ToString();
                ml.ComponentName = dt.Rows[i]["ComponentName"].ToString();
                ml.SubComponentName = dt.Rows[i]["SubComponentName"].ToString();
                messageList.Add(ml);
            }
            return messageList;

        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {

                SendNotifications();
            }
        }
        private void SendNotifications()
        {
            IEnumerable<MessageLog> messageList = GetAllMessagesLog();

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.broadcastMessage(messageList);//Will update all the clients with message log.

        }
    }
}