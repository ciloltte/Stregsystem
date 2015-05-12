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
        private StregsystemCommandParser parser;

        public StregsystemCLI(Stregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
            parser = new StregsystemCommandParser(stregsystem, this);
        }

        private void CLILoop()
        {
            while (isRunning)
            {
                Console.Clear();
                PrintActiveProducts();
                GetCommand();
                WaitForKey();
            }
        }

        private void PrintActiveProducts()
        {
            List<Product> activeProducts = stregsystem.GetActiveProducts();

            foreach (Product product in activeProducts)
            {
                Console.WriteLine(product.ToString());
            }
        }

        private void GetCommand()
        {
            Console.Write("\nWrite a command: ");
            string command = Console.ReadLine();
            Console.Clear();

            parser.ParseCommand(command);
        }

        private void WaitForKey()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        public void Start()
        {
            CLILoop();
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine("No user with username '" + username + "' found.");
        }

        public void DisplayProductNotFound(int productID)
        {
            Console.WriteLine("No product with ID: " + productID + " found.");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user.ToString() + "\n");
            stregsystem.log.ReadLatestTransactionsByUserId(user, 10).ForEach(Console.WriteLine);
        }

        public void DisplayTooManyArgumentsError()
        {
            Console.WriteLine("Not a valid command, too many arugments.");
        }

        public void DisplayAdminCommandNotFoundMessage()
        {
            Console.WriteLine("Not a valid admin command. Write '--help' to print all admin commands.");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            bool moreThanOne = transaction.AmountOfProduct > 1;
            Console.WriteLine("User " + transaction.User.Username + " bought " + 
                (moreThanOne ? transaction.AmountOfProduct + " x " + transaction.Product.Name : transaction.Product.Name) + " for " + (-(transaction.Amount / 100)) + " kr.");

            if(transaction.User.Balance < 5000)
                Console.WriteLine("Your balance is running low.");
        }

        public void Close()
        {
            isRunning = false;
        }

        public void DisplayError(string errorString)
        {
            Console.WriteLine("ERROR: " + errorString);
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
