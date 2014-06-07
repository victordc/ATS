using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;

namespace ATS.Framework
{
    public class EmailManager
    {
        public int SendMail(string fromEmail, string toEmail, string subject, string body, string email)
        {
            try
            {
                WebMail.SmtpServer = WebConfigurationManager.AppSettings["SMTPServer"].ToString();
                WebMail.From = fromEmail;
                WebMail.Send(
                        toEmail,
                        subject,
                        body,
                        email
                    );

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static void sendReminder(string from, string password, string to, string subject, string message)
        {
            var loginInfo = new NetworkCredential(from, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(from);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
    }
}
