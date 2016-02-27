using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using DomainModel.Interface;

namespace DomainModel.Mail
{
    
    public class MailMessageTo:IMailMessageTo
    {
        public string Mailto { get; set; }
        public string Caption { get; set; }
        public string Message { get; set; }
        public string AttachFile { get; set; }
    }
    public class MailSender:IMailSender
    {
        public string SMTPServer { get; set; }
        public string SMTPRequiresAuthentication { get; set; }
        public string SMTPUseSsl { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPassword { get; set; }
        public int SMTPTimeoutInMilliseconds { get; set; }
        public string SmtpPreferredEncoding { get; set; }
        public IMailMessageTo message { get; set; }
        public MailSender()
        {
            //message = m;
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            SMTPServer = appSettings["SMTPServer"];
            SMTPRequiresAuthentication = appSettings["SMTPRequiresAuthentication"];
            SMTPUseSsl = appSettings["SMTPUseSsl"];
            SMTPPort = Convert.ToInt32(appSettings["SMTPPort"]);
            SMTPUser = appSettings["SMTPUser"];
            SMTPPassword = appSettings["SMTPPassword"];
            SMTPTimeoutInMilliseconds = Convert.ToInt32(appSettings["SMTPTimeoutInMilliseconds"]);
            SmtpPreferredEncoding = appSettings["SmtpPreferredEncoding"];
            InitializeWebMail();
        }

        public void InitializeWebMail()
        {
            WebMail.SmtpServer = SMTPServer;
            WebMail.SmtpPort = SMTPPort;
            WebMail.From = SMTPUser;
            WebMail.UserName = SMTPUser;
            WebMail.Password = SMTPPassword;
            WebMail.EnableSsl = true;
        }

        public void SendByWebMail()
        {
            WebMail.Send(message.Mailto,message.Caption, message.Message);
            
        }
        public void SendMail()
        //string mailto, string caption, string message, string attachFile = null)
        {
            //InitCollectionSender();
            SmtpClient client = new SmtpClient();
            client.Host = SMTPServer;
            client.Port = SMTPPort;
            client.EnableSsl = Convert.ToBoolean(SMTPUseSsl);
            client.Credentials = new NetworkCredential(SMTPUser, SMTPPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SMTPUser);
            mail.To.Add(new MailAddress(message.Mailto));
            mail.Subject = message.Caption;
            mail.Body = message.Message;
            
            if (!string.IsNullOrEmpty(message.AttachFile))
                mail.Attachments.Add(new Attachment(message.AttachFile));
            client.Send(mail);

            mail.Dispose();
        }
    }

}