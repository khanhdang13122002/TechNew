using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Tech_News.Helper
{
    public class SendMail
    {
        public static async Task<bool> SendMailAsync(string to,string subject,string body,string attchFile)
        {
            try
            {
                MailMessage msg = new MailMessage(ContrainsHelper.mailSender, to, subject, body);
                if (!string.IsNullOrEmpty(attchFile))
                {
                    Attachment att = new Attachment(attchFile);
                    msg.Attachments.Add(att);
                }
                using (var client = new SmtpClient(ContrainsHelper.hostMail, ContrainsHelper.portMail))
                {
                    client.EnableSsl = true;
                    NetworkCredential credential = new NetworkCredential(ContrainsHelper.mailSender,ContrainsHelper.mailPassword);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credential;
                    await  client.SendMailAsync(msg);
                }

            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}