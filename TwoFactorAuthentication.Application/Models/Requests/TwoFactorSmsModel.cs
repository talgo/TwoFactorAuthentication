using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuthentication.Application.Models.Requests
{
    public class TwoFactorSmsModel
    {
        public string UserId { get; set; }
        public string Phone { get; set; }
    }
}
