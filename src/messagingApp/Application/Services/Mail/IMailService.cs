using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Mail;

public interface IMailService
{
    Task SendEmailAsync(string to, string subject, string htmlMessage);
}
