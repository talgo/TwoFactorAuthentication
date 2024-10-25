using TwoFactorAuthentication.Application.Interfaces;
using TwoFactorAuthentication.Application.Models.Requests;
using TwoFactorAuthentication.Application.Models.Responses;

namespace TwoFactorAuthentication.Application.Services
{
    public class TwoFactorAuthenticationEmail : TwoFactorAuthenticationBase, ITwoFactorAuthenticationEmail
    {
        private readonly IEmailService _emailService;
        public TwoFactorAuthenticationEmail(IRedisService redisService, IEmailService emailService) : base(redisService)
        {
            _emailService = emailService;
        }

        protected override TwoFactorGenerateResponseModel SendCode(TwoFactorAuthModel twoFactorGenerate)
        {
            var sendEmailResponse = SendEmail(new SendEmailRequestModel()
            {
                Content = twoFactorGenerate.Token,
                Email = twoFactorGenerate.Email
            });

            return new TwoFactorGenerateResponseModel()
            {
                IsTokenSent = sendEmailResponse.Success,
                Success = sendEmailResponse.Success
            };
        }

        private SendEmailResponseModel SendEmail(SendEmailRequestModel sendEmailRequest)
        {
            var sendEmailResponse = new SendEmailResponseModel();

            sendEmailResponse = _emailService.SendEmail(sendEmailRequest);

            return sendEmailResponse;
        }
    }
}
