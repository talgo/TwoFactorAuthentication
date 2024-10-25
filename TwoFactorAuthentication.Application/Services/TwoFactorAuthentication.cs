using TwoFactorAuthentication.Application.Constants;
using TwoFactorAuthentication.Application.Interfaces;
using TwoFactorAuthentication.Application.Models.Requests;
using TwoFactorAuthentication.Application.Models.Responses;
using TwoFactorAuthentication.Application.Models;

namespace TwoFactorAuthentication.Application.Services
{
    public class TwoFactorAuthentication : ITwoFactorAuthentication
    {
        private readonly IRedisService _redisService;

        public TwoFactorAuthentication(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<TokenModel> GetToken(string userId)
        {
            string redisKey = RedisKeys.TwoFactorAuthByUserId(userId);
            var tokenRedis = await _redisService.GetAsync<TwoFactorAuthModel>(redisKey);

            if (tokenRedis == null)
                return null;

            return new TokenModel()
            {
                ExpirationTime = await _redisService.GetExpirationAsync(redisKey),
                Token = tokenRedis.Token,
                UserId = userId
            };
        }

        public async Task<TwoFactorValidateTokenResponseModel> ValidateToken(TwoFactorValidateTokenRequestModel validateTokenRequestModel)
        {
            if (validateTokenRequestModel == null || string.IsNullOrEmpty(validateTokenRequestModel.Token) || string.IsNullOrEmpty(validateTokenRequestModel.UserId))
                return new TwoFactorValidateTokenResponseModel() { Success = false };

            TokenModel tokenModel = await GetToken(validateTokenRequestModel.UserId);

            if (tokenModel == null)
                return new TwoFactorValidateTokenResponseModel { Success = false };

            if (string.Equals(tokenModel.Token, validateTokenRequestModel.Token))
            {
                string redisKey = RedisKeys.TwoFactorAuthByUserId(validateTokenRequestModel.UserId);
                await _redisService.RemoveAsync(redisKey);
            }
            else
            {
                return new TwoFactorValidateTokenResponseModel() { Success = false, ErrorMessage = "Token not found!" };
            }

            return new TwoFactorValidateTokenResponseModel() { Success = true };
        }
    }
}
