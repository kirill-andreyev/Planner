﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class LoginResult
    {
        public string Name { get; set; }
        public string JwtToken { get; set; }
    }
}
