using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Primesoft.Common.Web.Configurations;

namespace Primesoft.Common.Web.Contracts
{
    public interface IEmailService
    {
        void SendMail(EmailServerConfiguration configuration, String from, String to, String subject, String body, Boolean isBodyHtml, Boolean isAsync, Attachment[] attachments);
        void SendMail(EmailServerConfiguration configuration, String from, String to, String subject, String body, Boolean isBodyHtml, Boolean isAsync);
        void SendMail(EmailServerConfiguration configuration, String from, String to, String subject, String body, Boolean isBodyHtml);
        void SendMail(EmailServerConfiguration configuration, String from, String to, String subject, String body);
    }
}
