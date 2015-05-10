using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class NotAValidEmailException : Exception
    {
        public string Email { get { return email; } }

        private string email;

        public NotAValidEmailException(string email)
        {
            this.email = email;
        }
    }
}
