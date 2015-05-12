using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class Stregsystem
    {
        public Log log;
        public UserHandler userHandler;

        private ProductHandler productHandler;
        private List<Product> productList;
        private List<User> userList;

        public Stregsystem()
        {
            log = new Log();
            productHandler = new ProductHandler();
            userHandler = new UserHandler();

            productList = productHandler.productList;
            userList = userHandler.Userlist;
        }

        public BuyTransaction BuyProduct(int productID, string username)
        {
            Transaction trans = new BuyTransaction(log.GetNextTransactionID(), GetUser(username), GetProduct(productID));

            ExecuteTransaction(trans);

            return trans as BuyTransaction;
        }

        public BuyTransaction BuyProduct(int productID, int amountOfProduct, string username)
        {
            Transaction trans = new BuyTransaction(log.GetNextTransactionID(), GetUser(username), GetProduct(productID), amountOfProduct);

            ExecuteTransaction(trans);

            return trans as BuyTransaction;
        }

        public void AddCreditsToAccount(int amount, string username)
        {
            Transaction trans = new InsertCashTransaction(log.GetNextTransactionID(), GetUser(username), amount);

            ExecuteTransaction(trans);
        }

        private void ExecuteTransaction(Transaction trans)
        {
            try
            {
                trans.Execute();
            }
            catch (InsufficientCreditsException exception)
            {
                throw exception;
            }

            log.SaveTransaction(trans);
        }

        public Product GetProduct(int productID)
        {
            List<Product> tempProductList = productList.Where(product => product.ProductID == productID).ToList<Product>();

            if(tempProductList.Count > 0 && tempProductList[0] != null)
                return tempProductList[0];

            return null;
        }

        public User GetUser(string username)
        {
            List<User> tempUserList = userList.Where(user => user.Username == username).ToList<User>();

            if (tempUserList.Count > 0 && tempUserList[0] != null)
                return tempUserList[0];

            throw new UserNotFoundException();
        }

        public List<string> GetTransactionList(int numOfTransactions)
        {
            return log.ReadLatestTransactions(numOfTransactions);
        }

        public List<string> GetTransactionList(string username, int numOfTransactions)
        {
            return log.ReadLatestTransactionsByUserId(GetUser(username), numOfTransactions);
        }

        public List<Product> GetActiveProducts()
        {
            return productList.Where(product => product.Active).ToList<Product>();
        }
    }
}