using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoFactorAuthentication.Application.Constants;
using TwoFactorAuthentication.Application.Interfaces;
using TwoFactorAuthentication.Application.Models;
using TwoFactorAuthentication.Application.Models.Requests;
using TwoFactorAuthentication.Application.Models.Responses;

namespace TwoFactorAuthentication.Application.Services
{
    public abstract class TwoFactorAuthenticationBase : ITwoFactorAuthenticationBase
    {
        private readonly IRedisService _redisService;

        protected TwoFactorAuthenticationBase(IRedisService redisService)
        {
            _redisService = redisService;
        }

        protected abstract TwoFactorGenerateResponseModel SendCode(TwoFactorAuthModel twoFactorGenerate);

        public async Task<TwoFactorGenerateResponseModel> Send(TwoFactorModel twoFactorModel)
        {
            var response = new TwoFactorGenerateResponseModel();

            string redisKey = RedisKeys.TwoFactorAuthByUserId(twoFactorModel.UserId);

            var cacheTwoFactorAuth = await _redisService.GetAsync<TwoFactorAuthModel>(redisKey);
            if (cacheTwoFactorAuth == null)
            {
                string twoFactorCode = GenerateCode();
                TwoFactorAuthModel twoFactorAuthModel = new TwoFactorAuthModel()
                {
                    Email = twoFactorModel.Email,
                    Phone = twoFactorModel.Phone,
                    Token = twoFactorCode
                };
                var sendCodeResponse = SendCode(twoFactorAuthModel);
                if (sendCodeResponse != null && sendCodeResponse.Success)
                {
                    _redisService.AddOrUpdateAsync(redisKey, twoFactorAuthModel);

                    response.IsTokenSent = sendCodeResponse.Success;
                }
            }
            else
            {
                response.IsTokenSent = true;
            }

            if (response.IsTokenSent)
                response.ExprirationTime = await _redisService.GetExpirationAsync(redisKey);

            return response;
        }

        private string GenerateCode()
        {
            var random = new Random();
            var twoFactorCode = random.Next(0, 100000).ToString("D6");

            return twoFactorCode;
        }
    }
}