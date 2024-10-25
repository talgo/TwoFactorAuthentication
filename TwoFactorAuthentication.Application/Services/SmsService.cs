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
    public class SmsService : ISmsService
    {
        public SendSmsResponseModel SendSms(SendSmsRequestModel request)
        {
            return new SendSmsResponseModel() { ErrorMessage = "", Success = true };
        }
    }
}
