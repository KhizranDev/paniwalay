using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;

namespace WebTech.Common
{
    public static class Email
    {
        public static bool SendMail(string from, string to, string subject, string body, bool IsBodyHtml)
        {
            try
            {
                MailAddress fromAddress = new MailAddress(from);
                MailAddress toAddress = new MailAddress(to);

                return SendMail(fromAddress, toAddress, subject, body, IsBodyHtml);
            }
            catch (Exception ae)
            {
                Logs.WriteError("WebTech.Common.Email", "SendMail(" + from + "," + to + "," + subject + "," + body + ")", "General", ae.Message);
                return false;
            }
        }

        public static bool SendMail(MailAddress from, MailAddress to, string subject, string body, bool IsBodyHtml)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
                MailMessage message = new MailMessage();

                smtpClient.Port = 587;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new System.Net.NetworkCredential("admin@paniwalay.com", "PjH2@bCHqQ");
                smtpClient.EnableSsl = true;

                message.From = from; //here you can set address
                message.To.Add(to); //here you can add multiple to
                message.Subject = subject; //subject of email
                message.IsBodyHtml = IsBodyHtml; //To determine email body is html or not
                // ->> Define Email Message With Format<--	
                message.Body = body;
                // -->> End Define Email Message With Format<--
                smtpClient.Send(message);

                return true;
            }
            catch (Exception ae)
            {
                Logs.WriteError("WebTech.Common.Email", "SendMail(" + from.Address + "," + to.Address + "," + subject + "," + body + ")", "General", ae.Message);
                return false;
            }
        }

        public static bool SendMail2(MailAddress from, MailAddress to, string subject, string body, bool IsBodyHtml)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();

                message.From = from; //here you can set address
                message.To.Add(to); //here you can add multiple to
                message.Subject = subject; //subject of email
                message.IsBodyHtml = IsBodyHtml; //To determine email body is html or not
                // ->> Define Email Message With Format<--	
                message.Body = body;
                // -->> End Define Email Message With Format<--
                smtpClient.Send(message);

                return true;
            }
            catch (Exception ae)
            {
                Logs.WriteError("WebTech.Common.Email", "SendMail(" + from.Address + "," + to.Address + "," + subject + "," + body + ")", "General", ae.Message);
                return false;
            }
        }

        public static bool SaveEmail(string ToEmail, string Subject, string Message)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@ToEmail", Value=ToEmail },
                                           new SqlParameter() { ParameterName="@Subject", Value=Subject },
                                           new SqlParameter() { ParameterName="@Message", Value=Message },
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_SaveEmail", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Email.cs", "SaveEmail('" + ToEmail + "', " + Message + ")", "General", ae.Message);
                return false;
            }
        }
    }
}
