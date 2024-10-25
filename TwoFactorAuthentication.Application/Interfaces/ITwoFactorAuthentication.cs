using TwoFactorAuthentication.Application.Models;
using TwoFactorAuthentication.Application.Models.Requests;
using TwoFactorAuthentication.Application.Models.Responses;

namespace TwoFactorAuthentication.Application.Interfaces
{
    public interface ITwoFactorAuthentication
    {
        Task<TokenModel> GetToken(string userId);
        Task<TwoFactorValidateTokenResponseModel> ValidateToken(TwoFactorValidateTokenRequestModel validateTokenRequestModel);
    }
}