using Domain.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace Domain.Service
{
    public class EmailService : IEMailService
    {
        private readonly MailSettings mailSettings;
        private readonly IConfiguration configuration;
        public EmailService(IOptions<MailSettings> mailsettings, IConfiguration configuration)
        {
            this.mailSettings = mailsettings.Value;
            this.configuration = configuration;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var FromMail = configuration.GetSection("MailSettings")["FromMail"];
                var DisplayName = configuration.GetSection("MailSettings")["DisplayName"];
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(DisplayName, FromMail));
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(mailSettings.Host, mailSettings.Port, mailSettings.UseSSL);
                await smtp.AuthenticateAsync(mailSettings.UserMail, mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
