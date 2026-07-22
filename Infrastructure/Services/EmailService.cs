using Application.Interface.Service;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {

        public EmailService() { }
        public Task SendVerificationEmailAsync(string email, string verificationToken)
        {
            throw new NotImplementedException();
        }
    }
}
