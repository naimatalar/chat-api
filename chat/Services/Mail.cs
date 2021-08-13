using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace chat.Services
{
    public class Mail
    {
        public static bool SendMail(string mail,string content)
        {
            try
            {
           var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ultum.chat@gmail.com"));
            email.To.Add(MailboxAddress.Parse(mail));
            email.Subject = "Ultum dan bir iletişim talebi geldi";
            email.Body = new TextPart(TextFormat.Html) { Text = content };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ultum.chat@gmail.com", "text1.text");
            smtp.Send(email);
            smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                 
                throw ex;
            }
         
            return true;
        }
    }
}
