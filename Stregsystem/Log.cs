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

        public List<string> ReadLatestTransactions(int numTransactionsToRead)
        {
            List<string> tempList = GetAllTransactions();
            List<string> results = new List<string>();

            for (int i = tempList.Count - 1; i > (tempList.Count > numTransactionsToRead ? tempList.Count - numTransactionsToRead : tempList.Count); i--)
            {
                results.Add(tempList[i]);
            }

            return results;
        }

        public List<string> ReadLatestTransactionsByUserId(User user, int numTransactionsToRaed)
        {
            List<string> tempList = GetAllTransactions();
            List<string> results = new List<string>();
            int count = tempList.Count - 1;

            while (results.Count < numTransactionsToRaed && count >= 0)
            {
                if (tempList[count].Split(' ')[2].Split('=')[1] == user.Username)
                {
                    results.Add(tempList[count]);
                }

                count--;
            }

            return results;
        }

        public int GetNextTransactionID()
        {
            List<string> tempList = GetAllTransactions();

            return Convert.ToInt32(tempList[tempList.Count - 1].Split(' ')[1].Split('=')[1]);
        }

        private List<string> GetAllTransactions()
        {
            List<string> results = new List<string>();
            StreamReader reader = new StreamReader(fileDir);
            string str;

            while ((str = reader.ReadLine()) != null)
            {
                results.Add(str);
            }

            return results;
        }
    }
}