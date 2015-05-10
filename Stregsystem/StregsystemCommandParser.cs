using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class StregsystemCommandParser
    {
        private Stregsystem stregsystem;
        private IStregsystemUI ui;
        private Dictionary<string, Action<string>> adminFunctions; 

        public StregsystemCommandParser(Stregsystem stregsystem, IStregsystemUI ui)
        {
            this.stregsystem = stregsystem;
            this.ui = ui;
            adminFunctions = new Dictionary<string, Action<string>>();

            adminFunctions.Add(":quit", str => ui.Close());
            adminFunctions.Add(":activate", productID => stregsystem.GetProduct(Convert.ToInt32(productID)).Active = true);
            adminFunctions.Add(":deactivate", productID => stregsystem.GetProduct(Convert.ToInt32(productID)).Active = false);
            adminFunctions.Add(":crediton", productID => stregsystem.GetProduct(Convert.ToInt32(productID)).CanBeBoughtOnCredit = true);
            adminFunctions.Add(":creditoff", productID => stregsystem.GetProduct(Convert.ToInt32(productID)).CanBeBoughtOnCredit = false);
            adminFunctions.Add(":addcredits", usernameAndAmount => stregsystem.AddCreditsToAccount(Convert.ToInt32(usernameAndAmount.Split()[1]), usernameAndAmount.Split()[0]));
            adminFunctions.Add(":help", str => adminFunctions.Keys.ToList().ForEach(key => ui.DisplayMessage(key)));

            stregsystem.AddCreditsToAccount(10000, "siraggi");
        }

        public void ParseCommand(string command)
        {
            string[] commandParts = command.Split(' ');

            if (command[0] == ':')
            {
                ParseAdminCommand(commandParts);
            }
            else
            {
                try
                {
                    if (commandParts.Count() == 3)
                    {
                        ui.DisplayUserBuysProduct(stregsystem.BuyProduct(Convert.ToInt32(commandParts[2]), Convert.ToInt32(commandParts[1]), commandParts[0]));
                    }
                    else if (commandParts.Count() == 2)
                    {
                        ui.DisplayUserBuysProduct(stregsystem.BuyProduct(Convert.ToInt32(commandParts[1]), commandParts[0]));
                    }
                    else if (commandParts.Count() == 1)
                    {
                        ui.DisplayUserInfo(stregsystem.GetUser(commandParts[0]));
                    }
                    else
                    {
                        ui.DisplayTooManyArgumentsError();
                    }
                }
                catch(UserNotFoundException)
                {
                    ui.DisplayUserNotFound(commandParts[0]);
                }
            }
        }

        private bool ValidCommand(string[] commandParts)
        {
            int temp;

            if (commandParts.Count() > 3)
            {
                ui.DisplayTooManyArgumentsError();
            }
            else if (commandParts.Count() == 3)
            {
                if (Int32.TryParse(commandParts[1], out temp) && Int32.TryParse(commandParts[2], out temp))
                    return true;
            }
            else if (commandParts.Count() == 2)
            {
                if (Int32.TryParse(commandParts[1], out temp))
                    return true;
            }

            return false;
        }

        private void ParseAdminCommand(string[] commandParts)
        {
            string inputString = string.Empty;

            for(int i = 1; i < commandParts.Count(); i++)
                inputString += commandParts[i] + " ";

            if (adminFunctions.ContainsKey(commandParts[0]))
                try
                {
                    adminFunctions[commandParts[0]](inputString);
                }
                catch
                {
                    ui.DisplayError("Not a valid suffix for admin command " + commandParts[0]);
                }
            else
                ui.DisplayError("Not a valid admin command.\nWrite ':help' to get all admin commands.");
        }
    }
}