using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(User user, Product product)
            : base("Transaction of " + product.Name + " failed for " + user.Username + ", due to insufficient funds." )
        {

        }
    }
}
