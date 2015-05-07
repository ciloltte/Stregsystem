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
        List<string> ReadLatestTransactions(int numTransactionsToRaed);
        List<string> ReadLatestTransactionsByUserId(User user, int numTransactionsToRaed);
        int GetNextTransactionID();
    }
}
