using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primesoft.Common;
using Primesoft.Common.Web.Contracts;
using Primesoft.Common.Web.Services;

namespace Primesoft.Common.Web.Factories
{
    public class EmailServiceFactory
    {
        private IEmailService _emailService;

        public EmailServiceFactory(CommonWebEnumeration.EmailServiceType serviceType)
        {
            //switch (serviceType)
            //{
            //    case CommonWebEnumeration.EmailServiceType.SMTP:
            //    default:
            //        _emailService = new SMTPService();
            //        break;
            //    case CommonWebEnumeration.EmailServiceType.Text:
            //        _emailService = new TextLoggingEmailService();
            //        break;
            //}
        }

        public IEmailService EmailService { get { return _emailService; } }
    }
}
