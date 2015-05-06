using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class Log : ILog
    {
        private string fileDir = @"\TransactionLog.txt";

        public void SaveTransaction(Transaction transaction)
        {
            string fileDir = @"\TransactionLog.txt";

            if (!File.Exists(fileDir))
            {
                File.Create(fileDir);
            }

            StreamWriter writer = File.AppendText(fileDir);

            writer.WriteLine(transaction.ToString());
        }

        public List<string> ReadLatestTransactions(int numTransactionsToRaed)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadLatestTransactionsByUserId(User user, int numTransactionsToRaed)
        {
            throw new NotImplementedException();
        }

        public int GetNextTransactionID()
        {
            List<string> tempList = GetAllTransactions();

            return Convert.ToInt32(tempList[tempList.Count - 1].Split(' ')[1].Split('=')[1]);
        }

        private List<string> GetAllTransactions()
        {
            List<string> transactionStrings = new List<string>();
            StreamReader reader = new StreamReader(fileDir);
            string str;

            while ((str = reader.ReadLine()) != null)
            {
                transactionStrings.Add(str);
            }

            return transactionStrings;
        }
    }
}