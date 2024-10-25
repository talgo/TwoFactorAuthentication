using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuthentication.Application.Models
{
    public class TokenModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public TimeSpan? ExpirationTime { get; set; } = TimeSpan.Zero;       
    }
}
