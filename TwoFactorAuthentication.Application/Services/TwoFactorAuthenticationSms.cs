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
    public class TwoFactorAuthenticationSms : TwoFactorAuthenticationBase, ITwoFactorAuthenticationSms
    {
        private readonly ISmsService _smsService;
        public TwoFactorAuthenticationSms(IRedisService redisService, ISmsService smsService) : base(redisService)
        {
            _smsService = smsService;
        }

        protected override TwoFactorGenerateResponseModel SendCode(TwoFactorAuthModel twoFactor)
        {
            var sendSmsResponse = SendSms(new SendSmsRequestModel()
            {
                Message = twoFactor.Token,
                PhoneNumber = twoFactor.Phone
            });

            return new TwoFactorGenerateResponseModel()
            {
                IsTokenSent = sendSmsResponse.Success,
                Success = sendSmsResponse.Success
            };
        }

        private SendSmsResponseModel SendSms(SendSmsRequestModel sendSmsRequest)
        {
            var sendSmsResponse = new SendSmsResponseModel();

            sendSmsResponse.Success = _smsService.SendSms(sendSmsRequest).Success;

            return sendSmsResponse;
        }
    }
}
