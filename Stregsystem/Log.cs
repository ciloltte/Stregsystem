using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class Log : ILog
    {
        public void SaveTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public string[] ReadTransactions(int numTransactionsToRaed)
        {
            throw new NotImplementedException();
        }

        public string[] ReadTransactionsByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
