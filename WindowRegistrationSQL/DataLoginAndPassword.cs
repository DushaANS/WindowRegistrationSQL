﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowRegistrationSQL
{
    public class DataLoginAndPassword
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public DataLoginAndPassword()
        {
            Login = "";
            Password = "";
        }
    }
}
