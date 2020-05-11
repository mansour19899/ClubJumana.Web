using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ClubJumana.Core.Senders
{
    public class SendEmail
    {
        public static void Send(string to,string subject,string body)
        {
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("clubjummana.com");
            //mail.From = new MailAddress("smm.2020@clubjummana.com", "تاپ لرن");
            //mail.To.Add(to);
            //mail.Subject = subject;
            //mail.Body = body;
            //mail.IsBodyHtml = true;



            //SmtpServer.Port = 465;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("smm.2020@clubjummana.com", "man1989sour");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential smtpCredentials = new NetworkCredential("smm.2020@clubjummana.com", "man1989sour");

            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("smm.2020@clubjummana.com");
            MailAddress BccAddress = new MailAddress("smm.2020@clubjummana.com");
            MailAddress toAddress = new MailAddress(to);

            smtpClient.Host = "mail.clubjummana.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = smtpCredentials;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            message.From = fromAddress;
            message.Bcc.Add(BccAddress); 
            message.To.Add(toAddress);
            message.IsBodyHtml = false;
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            message.SubjectEncoding = System.Text.Encoding.Default;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            smtpClient.Send(message);

        }
    }
}