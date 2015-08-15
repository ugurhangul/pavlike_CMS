using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace pavlikeLibrary
{
    public class MailerDeamon
    {
       public static void Sender(string host,int smtpport,string fromMail,string fromPassword,string toMail, string toName, string body, string subject,string senderDisplayName="",bool enableSsl = false,SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network,bool useDefaultCredentials = false)
        {
            var fromAddress = new MailAddress(fromMail, senderDisplayName);
            var toAddress = new MailAddress(toMail, toName);
        
            var smtp = new SmtpClient
            {
                Host = host,
                Port = smtpport,
                EnableSsl = enableSsl,
                DeliveryMethod = deliveryMethod,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);
        }
    }
}
