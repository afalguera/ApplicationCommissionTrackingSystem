using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Primesoft.Common.Exceptions;
using Primesoft.Common.Utilities;
using Primesoft.Common.Web.Configurations;
using Primesoft.Common.Web.Contracts;

namespace Primesoft.Common.Web.Services
{
    public class SMTPService : IEmailService
    {
        #region IEmailService Members

        public void SendMail(EmailServerConfiguration configuration, string from, string to, string subject, string body, bool isBodyHtml, bool isAsync, Attachment[] attachments)
        {
            if (WebValidationUtility.ValidateEmailAddress(from) && WebValidationUtility.ValidateEmailAddress(to) && !String.IsNullOrEmpty(body))
            {
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.IsBodyHtml = isBodyHtml;
                message.Body = body;
                message.From = new MailAddress(from);

                if (attachments != null && attachments.Length > 0)
                {
                    for (Int32 index = 0; index < attachments.Length; index++)
                    {
                        message.Attachments.Add(attachments[index]);
                    }
                }

                try
                {
                    if (isAsync)
                    {
                        using (new SynchronizationUtility())
                        {
                            SmtpClient smtpClient = new SmtpClient(configuration.Server);
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Port = configuration.Port;
                            if (configuration.RequiresAuthentication)
                            {
                                smtpClient.Credentials = new NetworkCredential(configuration.UserName, configuration.Password);
                            }
                            smtpClient.SendAsync(message, null);
                        }
                    }
                    else
                    {
                        SmtpClient smtpClient = new SmtpClient(configuration.Server);
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Port = configuration.Port;
                        smtpClient.EnableSsl = configuration.UseSsl;
                        if (configuration.RequiresAuthentication)
                        {
                            smtpClient.Credentials = new NetworkCredential(configuration.UserName, configuration.Password);
                        }
                        smtpClient.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    throw new EmailSendingException("Invalid email configuration");
                }
            }
        }

        public void SendMail(EmailServerConfiguration configuration, string from, string to, string subject, string body, bool isBodyHtml, bool isAsync)
        {
            SendMail(configuration, from, to, subject, body, isBodyHtml, isAsync, null);
        }

        public void SendMail(EmailServerConfiguration configuration, string from, string to, string subject, string body, bool isBodyHtml)
        {
            SendMail(configuration, from, to, subject, body, isBodyHtml, false, null);
        }

        public void SendMail(EmailServerConfiguration configuration, string from, string to, string subject, string body)
        {
            SendMail(configuration, from, to, subject, body, false, false, null);
        }

        #endregion
    }
}
