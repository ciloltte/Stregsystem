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

            MakeTempUsers();

            adminFunctions = new Dictionary<string, Action<string>>();

            adminFunctions.Add(":q", str => ui.Close());
            adminFunctions.Add(":quit", str => ui.Close());
            adminFunctions.Add(":activate", productID => stregsystem.GetProduct(Convert.ToInt32(productID)).Active = true);
            adminFunctions.Add(":deactivate", productID => stregsystem.GetProduct(Convert.ToInt32(productID)).Active = false);
            adminFunctions.Add(":crediton", productID => stregsystem.GetProduct(Convert.ToInt32(productID)).CanBeBoughtOnCredit = true);
            adminFunctions.Add(":creditoff", productID => stregsystem.GetProduct(Convert.ToInt32(productID)).CanBeBoughtOnCredit = false);
            adminFunctions.Add(":addcredits", usernameAndAmount => stregsystem.AddCreditsToAccount(Convert.ToInt32(usernameAndAmount.Split()[1]), usernameAndAmount.Split()[0]));
            adminFunctions.Add(":makeuser", userDetails => MakeUser(userDetails));
            adminFunctions.Add(":help", str => adminFunctions.Keys.ToList().ForEach(key => ui.DisplayMessage(key)));
        }

        public void ParseCommand(string command)
        {
            string[] commandParts = command.Split(' ');

            if (command != string.Empty)
                if (command[0] == ':')
                {
                    ExecuteAdminCommand(commandParts);
                }
                else if (ValidCommand(commandParts))
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
                    catch (Exception e)
                    {
                        if (e is UserNotFoundException)
                            ui.DisplayUserNotFound(commandParts[0]);
                        else if (e is InsufficientCreditsException)
                            ui.DisplayError("Insufficient funds.");
                    }
                }
                else
                    ui.DisplayError("Not a valid command");
            else
                ui.DisplayError("You must type a command.");
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
            else if (commandParts.Count() == 1)
                return true;

            return false;
        }

        private void ExecuteAdminCommand(string[] commandParts)
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

        // Makes temporary Users to test the program
        private void MakeTempUsers()
        {
            MakeUser("August Korvell siraggi aug.ust@aggisoft.dk");
            MakeUser("August1 Korvell1 siraggi1 august1@aggisoft.dk");
        }

        private void MakeUser(string userDetails)
        {
            string[] parts = userDetails.Split(' ');

            try
            {
                stregsystem.userHandler.MakeNewUser(parts[0], parts[1], parts[2], parts[3]);
            }
            catch (Exception e)
            {
                if (e is NotAValidEmailException)
                    ui.DisplayError(parts[3] + " is not a Command");
                else if (e is NotAValidUsernameException)
                    ui.DisplayError(parts[2] + " is not a valid username");
                else if (e is UsernameTakenException)
                    ui.DisplayError("Username " + parts[2] + " is taken");
            }
        }
    }
}