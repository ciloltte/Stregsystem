using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    interface ILog
    {
        void SaveTransaction(Transaction transaction);
        string[] ReadTransactions(int numTransactionsToRaed);
        string[] ReadTransactionsByUserId(int userId);
    }
}
