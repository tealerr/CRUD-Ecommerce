using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class MailHelpers
    { 
        public async Task<HttpStatusCode> SendMailAsync(List<EmailAddress> emails, string TemplateId, object MailData)
        {
            Console.WriteLine(MailData);
            var client = new SendGridClient(ConnectionStrings.SendgridApikey);
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(ConnectionStrings.SendgridMailSender, "Club Beaute"));
            msg.AddTos(emails);
            msg.SetTemplateId(TemplateId);
            //msg.AddAttachments(attachments);
              
            msg.SetTemplateData(MailData);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(MailData);
            var response = await client.SendEmailAsync(msg);
            return response.StatusCode;
        }
    }
}
