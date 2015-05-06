using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class InsertCashTransaction : Transaction
    {

        public InsertCashTransaction(int transactionID, User user, int amount)
            : base(transactionID, user, amount)
        {
        }

        public override string ToString()
        {
            return "Insert cash transaction=" + base.ToString();
        }
    }
}
