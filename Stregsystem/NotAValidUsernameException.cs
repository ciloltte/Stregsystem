using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class NotAValidUsernameException : Exception
    {
        public string Username { get { return username; } }

        private string username;

        public NotAValidUsernameException(string username)
        {
            this.username = username;
        }
    }
}
