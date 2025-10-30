using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message);

    }
}
