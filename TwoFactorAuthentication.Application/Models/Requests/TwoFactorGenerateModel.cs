﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuthentication.Application.Models.Requests
{
    public class TwoFactorModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
