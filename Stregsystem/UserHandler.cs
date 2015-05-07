using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class UserHandler
    {
        public List<User> Userlist { get { return userList; } }

        private List<User> userList;

        public UserHandler()
        {
            userList = new List<User>();

            // Temp userlist
            userList.Add(new User("August", "Korvell", "siraggi", "august@aggisoft.dk", GetNextUserID()));
            userList.Add(new User("August1", "Korvell1", "siraggi1", "august1@aggisoft.dk", GetNextUserID()));
            userList.Add(new User("August2", "Korvell2", "siraggi2", "august2@aggisoft.dk", GetNextUserID()));
            userList.Add(new User("August3", "Korvell3", "siraggi3", "august3@aggisoft.dk", GetNextUserID()));
        }

        private int GetNextUserID()
        {
            return userList.Count + 1;
        }
    }
}
