using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoFactorAuthentication.Application.Interfaces;
using TwoFactorAuthentication.Application.Models.Requests;
using TwoFactorAuthentication.Application.Models.Responses;

namespace TwoFactorAuthentication.Application.Services
{
    public class EmailService : IEmailService
    {
        public SendEmailResponseModel SendEmail(SendEmailRequestModel sendEmailRequest)
        {
            return new SendEmailResponseModel() { ErrorMessage = "", Success = true };
        }
    }
}
