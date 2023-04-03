using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailsToSend = new MimeMessage();
            emailsToSend.From.Add(MailboxAddress.Parse("berekesakan@yandex.ru"));
            emailsToSend.To.Add(MailboxAddress.Parse(email));
            emailsToSend.Subject = subject;
            emailsToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html){ Text = htmlMessage };

            //send email
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.yandex.ru", 465, true);
                emailClient.Authenticate("berekesakan@yandex.ru", "Zt5RXSM39jJF.Zk");
                emailClient.Send(emailsToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;
        }
    }
}
