using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace balenciaga.Core.Sender
{
    public class SendEmail
    {
        public static void Send(string To, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("finerip763@gmail.com", "BornRich");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            Console.WriteLine("pk");
            SmtpServer.UseDefaultCredentials = false;
            Console.WriteLine("gk");

            SmtpServer.Credentials = new System.Net.NetworkCredential("finerip763@gmail.com", "09906557095n mn n tw");
            Console.WriteLine("kk");

            SmtpServer.EnableSsl = true;
            Console.WriteLine("nk");


            SmtpServer.Send(mail);
            Console.WriteLine("send");


        }
    }
}