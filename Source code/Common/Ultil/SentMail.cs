using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Common.Ultil
{
    public class SentMail
    {
        public static void Sent(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(ToEmail);
                mail.From = new MailAddress(ToEmail);
                mail.Subject = Title;
                mail.Body = Content;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                (FromEmail, PassWord);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch
            {
            }

        }
    }
}