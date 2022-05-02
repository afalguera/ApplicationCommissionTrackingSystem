using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Primesoft.Common.Factories;
//using Primesoft.Common.Web.Configurations;
//using Primesoft.Common.Web.Contracts;

namespace Primesoft.Common.Web.Services
{
    //public class TextLoggingEmailService: IEmailService
    //{
    //    #region IEmailService Members

    //    public void SendMail(EmailServerConfiguration configuration, string from, string to, string subject, string body, bool isBodyHtml, bool isAsync, System.Net.Mail.Attachment[] attachments)
    //    {
    //        StringBuilder email = new StringBuilder();
    //        email.AppendLine(String.Format("To: {0}", to));
    //        email.AppendLine(String.Format("From: {0}", from));
    //        email.AppendLine(String.Format("Subject: {0}", subject));
    //        email.AppendLine(String.Format("Body: {0}", body));

    //        LoggingFactory.GetLogger().Log(email.ToString());
    //    }

    //    public void SendMail(EmailServerConfiguration configuration, string from, string to, string subject, string body, bool isBodyHtml, bool isAsync)
    //    {
    //        SendMail(configuration, from, to, subject, body, isBodyHtml, isAsync, null);
    //    }

    //    public void SendMail(EmailServerConfiguration configuration, string from, string to, string subject, string body, bool isBodyHtml)
    //    {
    //        SendMail(configuration, from, to, subject, body, isBodyHtml, false, null);
    //    }

    //    public void SendMail(EmailServerConfiguration configuration, string from, string to, string subject, string body)
    //    {
    //        SendMail(configuration, from, to, subject, body, false, false, null);
    //    }

    //    #endregion
    //}
}
