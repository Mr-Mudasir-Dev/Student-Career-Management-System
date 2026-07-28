using Application.Common;
using Application.Interface.Service;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Domain.Exceptions;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailConfig> emailconfig, ILogger<EmailService> logger)
        {
            _emailConfig = emailconfig.Value;
            _logger = logger;
        }
        public async Task<bool> SendVerificationEmailAsync(string email, string verificationLink)
        {
            try {
                _logger.LogInformation($"Email Sender : Sending verification email to {email}...");
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));

                message.To.Add(new MailboxAddress("", email));

                message.Subject = "Email Verification";

                message.Body = new TextPart("html")
                {
                    Text = $"<p>Click the link below to verify your email:</p><a href='{verificationLink}'>Verify Email</a>"
                };

                _logger.LogInformation($"Email Sender : Connecting to SMTP server {_emailConfig.SmtpServer}:{_emailConfig.Port}...");

                using var client = new SmtpClient();

                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);


                _logger.LogInformation($"Email Sender : Verification email sent to {email} successfully.");

                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something Wen't Wrong : {ex.Message}");
                throw new AppException("Something Wen't Wrong");
 
            }
        }
    }
}
