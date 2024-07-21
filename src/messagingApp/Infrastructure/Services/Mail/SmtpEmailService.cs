using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Mail;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Mail;

public class SmtpEmailService : IMailService
{
    private readonly SmtpConfiguration _smtpConfiguration;

    public SmtpEmailService(IOptions<SmtpConfiguration> smtpConfiguration)
    {
        _smtpConfiguration = smtpConfiguration.Value;
    }
    public async Task SendEmailAsync(string to, string subject, string htmlMessage)
    {
        try
        {
            var message = new MailMessage
            {
                From = new MailAddress(_smtpConfiguration.Username),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(to);

            var smtpClient = new SmtpClient(_smtpConfiguration.Server)
            {
                Port = _smtpConfiguration.Port,
                Credentials = new NetworkCredential(_smtpConfiguration.Username, _smtpConfiguration.Password),
                EnableSsl = _smtpConfiguration.EnableSsl
            };

            await smtpClient.SendMailAsync(message);
        }
        catch (Exception e)
        {
            throw new Exception("Error sending email");
        }
    }
}