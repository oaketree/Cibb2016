using System;
using System.Net.Mail;
using System.Web;

namespace Cibb.Core.Utility
{

    public class Email
    {
        private string messageSubject;
        private string messageBody;
        private string emailTo;
        private string emailFrom;
        private HttpPostedFile emailAttachment;
        public Email(string messageSubject, string messageBody, string emailFrom,string emailTo, HttpPostedFile emailAttachment=null)
        {
            this.messageSubject = messageSubject;
            this.messageBody = messageBody;
            this.emailTo = emailTo;
            this.emailFrom = emailFrom;
            this.emailAttachment = emailAttachment;
        }

        private MailMessage MakeEmail()
        {
            //MailMessage message = new MailMessage("shgygl@126.com", emailTo);
            //MailMessage message = new MailMessage("glxh09@163.com", emailTo);
            MailMessage message = new MailMessage(emailFrom, emailTo);
            message.IsBodyHtml = true;
            message.Subject = this.messageSubject;
            message.Body = this.messageBody;
            if (emailAttachment!=null)
            {
                Attachment attachment = new Attachment(emailAttachment.InputStream, emailAttachment.FileName);
                message.Attachments.Add(attachment);
            }
            return message;
        }

        public string send(string smtp,string username,string password)
        {
            //SmtpClient Client = new SmtpClient("smtp.126.com");
            SmtpClient Client = new SmtpClient(smtp);
            Client.UseDefaultCredentials = false;
            //Client.Credentials = new System.Net.NetworkCredential("shgygl", "bjb142900");
            Client.Credentials = new System.Net.NetworkCredential(username, password);
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                Client.Send(MakeEmail());
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
