﻿using System;
using System.Collections.Generic;
using System.Data;
//using System.DirectoryServices;
using System.Net.Mail;
using System.Text;

using VNC;
using VNCWPFUserControls;
using VNCWPFUserControls.Data;

namespace VNCWPFUserControls.Helpers
{
    public class Email
    {
        private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.HELPERS_EMAIL;

        public static void SendEmail(string sTo, string sFrom, string sSubject, string sBody, string sCC)
        {
            using (MailMessage mailMessage = new MailMessage(sFrom, sTo, sSubject, sBody))
            {
                // TODO(crhodes): Figure out how to do format types.
                //message.BodyFormat = MailFormat.Text;
                if (sCC != "")
                {
                    mailMessage.CC.Add(sCC);
                }

                SendEmail(mailMessage);
            }
        }

        public static void SendEmail(string sTo, string sFrom, string sSubject, string sBody, string sCC, List<string> attachments)
        {
            using (MailMessage mailMessage = new MailMessage(sFrom, sTo, sSubject, sBody))
            {
                //message.BodyFormat = MailFormat.Text;
                if (sCC != "")
                {
                    mailMessage.CC.Add(sCC);
                }

                foreach (string attachment in attachments)
                {
                    mailMessage.Attachments.Add(new Attachment(attachment));
                }

                SendEmail(mailMessage);
            }
        }

        public static void SendEmail(MailMessage message)
        {
            if (Config.SMTP_Server != "")
            {
                SmtpClient smtp = new SmtpClient(Config.SMTP_Server);

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    VNC.AppLog.Error(ex, Common.LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 8);
                    throw new ApplicationException("Cannot Send Email");
                }
            }
            else
            {
                // TODO(crhodes): Something about no SMTP
                throw new ApplicationException("SMTP Server not configured");
            }
        }

    }
}
