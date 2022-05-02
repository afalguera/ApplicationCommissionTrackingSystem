using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Configuration;


namespace CRS.Helper
{
    public static class Mailer
    {
        public static void Send(string sBody,string sSubject) 
        {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("mail.primesoft.com");
                var loginInfo = new NetworkCredential("mail.primesoft.com","qWERTY123");
                SmtpServer.Credentials = loginInfo;
                mail.From = new MailAddress("acts-admin@primesoft.com");
                //mail.To.Add("apalomares@primesoft.com");
                mail.To.Add("allan.palomares@gmail.com");
                mail.Subject = sSubject;
                mail.Body = sBody;

                SmtpServer.Send(mail);
        
        }

        public static void Send(string sBody, string sSubject, string sTo)
        {
            MailMessage mail = new MailMessage();
            Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            string networkHost = mailSettings != null ? mailSettings.Smtp.Network.Host.ToString() : string.Empty;
            int port = mailSettings != null ? mailSettings.Smtp.Network.Port : 0;
            string userName = mailSettings != null ? mailSettings.Smtp.Network.UserName : string.Empty;
            string userPswd = mailSettings != null ? mailSettings.Smtp.Network.Password : string.Empty;
            string sender = mailSettings != null ? mailSettings.Smtp.From : string.Empty;
            //SmtpClient SmtpServer = new SmtpClient("mail.primesoft.com");
            //var loginInfo = new NetworkCredential("mail.primesoft.com", "qWERTY123");
            SmtpClient SmtpServer = new SmtpClient(networkHost);
            var loginInfo = new NetworkCredential(userName, userPswd );
            SmtpServer.Credentials = loginInfo;
            mail.From = new MailAddress(sender);
            //mail.From = new MailAddress("acts-admin@primesoft.com");

            mail.To.Add(sTo);
            mail.Subject = sSubject;
            mail.Body = sBody;

            SmtpServer.Send(mail);

        }
    }
}