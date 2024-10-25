using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuthentication.Application.Models.Requests
{
    public class SendEmailRequestModel
    {
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
