﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuthentication.Application.Constants
{
    public static class RedisKeys
    {
        public static string TwoFactorAuthByUserId(string userId) => 
            string.Format("{0}:{1}", "TwoFactorAuth", userId);
    }
}