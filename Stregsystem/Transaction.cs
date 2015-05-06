using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    abstract class Transaction
    {
        public int TransactionID { get { return transactionID; } }
        public User User { get { return user; } }
        public DateTime TimeOfTransaction { get { return timeOfTransaction; } }
        public int Amount { get { return amount; } }

        private int transactionID;
        private User user;
        private DateTime timeOfTransaction;
        private int amount;

        public Transaction(int transactionID, User user, int amount)
        {
            this.transactionID = transactionID;
            this.user = user;
            this.amount = amount;

            timeOfTransaction = DateTime.Now;
        }

        public virtual void Execute()
        {
            User.Balance += amount;
        }

        public override string ToString()
        {
            return " ID=" + TransactionID + " Username=" + User.Username + " Amount=" + Amount + " TimeOfTransaction=" + TimeOfTransaction;
        }


    }
}
