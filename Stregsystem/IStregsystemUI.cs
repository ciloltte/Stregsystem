using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    interface IStregsystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(int productID);
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError();
        void DisplayAdminCommandNotFoundMessage();
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void Close();
        void DisplayError(string errorString);
        void DisplayMessage(string message);
    }
}
