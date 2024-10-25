using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuthentication.Application.Models.Requests
{
    public class SendSmsRequestModel
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
