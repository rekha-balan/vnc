using System;

namespace VNCDxWPFWindowAppBase.Data
{
    partial class ApplicationDataSet
    {
        public static void IndicateApplicationUsage(string application, DateTime eventDate, string user, string message)
        {
            var dataRow = Common.ApplicationDataSet.ApplicationUsage.NewApplicationUsageRow();

            dataRow.Application = application;
            dataRow.EventDate = eventDate;
            dataRow.User = user;
            dataRow.EventMessage = message;

            Common.ApplicationDataSet.ApplicationUsage.AddApplicationUsageRow(dataRow);
            // HACK(crhodes)
            // Skip writing to database for now
            Common.ApplicationDataSet.ApplicationUsageTA.Update(Common.ApplicationDataSet.ApplicationUsage);
        }
    }
}
