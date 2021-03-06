﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class UsernameTakenException : Exception
    {
        public string Username { get { return username; } }

        private string username;

        public UsernameTakenException(string username)
        {
            this.username = username;
        }
    }
}
