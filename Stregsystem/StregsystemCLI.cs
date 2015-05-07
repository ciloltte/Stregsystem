using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class StregsystemCLI : IStregsystemUI
    {
        private bool isRunning = true;
        private Stregsystem stregsystem;

        public StregsystemCLI(Stregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
        }

        private void CLILoop()
        {
            while (isRunning)
            {
                PrintActiveProducts();
                GetCommand();
            }
        }

        private void PrintActiveProducts()
        {
            List<Product> activeProducts = stregsystem.GetActiveProducts();

            //for (int i = 0; i < activeProducts.Count; i++)
            //{

            //}

            foreach (Product product in activeProducts)
            {
                Console.WriteLine(product.ToString());
            }
        }

        private void GetCommand()
        {
            Console.ReadLine();
        }

        public void Start()
        {
            CLILoop();
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine("No user with username: " + username + " found.");
        }

        public void DisplayProductNotFound(int productID)
        {
            Console.WriteLine("No product with ID: " + productID + " found.");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user.ToString());
        }

        public void DisplayTooManyArgumentsError()
        {
            Console.WriteLine("Not a valid command, to many arugments.");
        }

        public void DisplayAdminCommandNotFoundMessage()
        {
            Console.WriteLine("Not a valid admin command. Write '--help' to print all admin commands.");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            bool moreThanOne = transaction.AmountOfProduct > 1;
            Console.WriteLine("User " + transaction.User.Username + " bought " + 
                (moreThanOne ? transaction.AmountOfProduct + " x " + transaction.Product.Name : transaction.Product.Name) + " for " + (transaction.Amount / 100) + " kr.");
        }

        public void Close()
        {
            isRunning = false;
        }

        public void DisplayError(string errorString)
        {
            Console.WriteLine("ERROR: " + errorString);
        }
    }
}
