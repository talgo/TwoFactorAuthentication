using Microsoft.AspNetCore.Mvc;
using TwoFactorAuthentication.Application.Interfaces;
using TwoFactorAuthentication.Application.Models.Requests;

namespace TwoFactorAuthentication.WebAPI.Controllers
{
    public class TwoFactorAuthenticationController : Controller
    {
        private readonly ITwoFactorAuthenticationEmail _twoFactorAuthenticationEmail;
        private readonly ITwoFactorAuthenticationSms _twoFactorAuthenticationSms;
        private readonly ITwoFactorAuthentication _twoFactorAuthentication;

        public TwoFactorAuthenticationController(ITwoFactorAuthenticationEmail twoFactorAuthenticationEmail, ITwoFactorAuthenticationSms twoFactorAuthenticationSms, ITwoFactorAuthentication twoFactorAuthentication)
        {
            _twoFactorAuthenticationEmail = twoFactorAuthenticationEmail;
            _twoFactorAuthenticationSms = twoFactorAuthenticationSms;
            _twoFactorAuthentication = twoFactorAuthentication;
        }

        [HttpPost("SendByEmail")]
        public async Task<IActionResult> SendByEmail(TwoFactorEmailModel twoFactorModel)
        {
            var response = await _twoFactorAuthenticationEmail.Send(new TwoFactorModel() { Email = twoFactorModel.Email, UserId = twoFactorModel.UserId });
            return new ObjectResult(response);
        }

        [HttpPost("SendBySms")]
        public async Task<IActionResult> SendBySms(TwoFactorSmsModel twoFactorModel)
        {
            var response = await _twoFactorAuthenticationSms.Send(new TwoFactorModel() { Phone = twoFactorModel.Phone, UserId = twoFactorModel.UserId });
            return new ObjectResult(response);
        }

        [HttpPost("ValidateToken")]
        public async Task<IActionResult> ValidateToken(TwoFactorValidateTokenRequestModel validateTokenRequestModel)
        {
            var response = await _twoFactorAuthentication.ValidateToken(validateTokenRequestModel);
            return new ObjectResult(response);
        }
    }
}
