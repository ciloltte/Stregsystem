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
        private string fileDir;

        public Log()
        {
            fileDir = @".\TransactionLog.txt";

            if (!File.Exists(fileDir))
            {
                FileStream stream = File.Create(fileDir);
                stream.Close();
            }
        }

        public void SaveTransaction(Transaction transaction)
        {
            StreamWriter writer = File.AppendText(fileDir);

            writer.WriteLine(transaction.ToString());

            writer.Close();
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

            if (tempList.Count > 0)
                return Convert.ToInt32(tempList[tempList.Count - 1].Split(' ')[1].Split('=')[1]) + 1;
            else return 1;
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

            reader.Close();

            return results;
        }
    }
}