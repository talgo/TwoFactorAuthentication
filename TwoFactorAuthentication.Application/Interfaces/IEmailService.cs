using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoFactorAuthentication.Application.Models.Requests;
using TwoFactorAuthentication.Application.Models.Responses;

namespace TwoFactorAuthentication.Application.Interfaces
{
    public interface IEmailService
    {
        SendEmailResponseModel SendEmail(SendEmailRequestModel sendEmailRequest);
    }
}
