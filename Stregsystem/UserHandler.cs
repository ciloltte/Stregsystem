using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stregsystem
{
    class UserHandler
    {
        public List<User> Userlist { get { return userList; } }

        private List<User> userList;
        bool invalid = false;

        public UserHandler()
        {
            userList = new List<User>();

            // Temp userlist
            MakeNewUser("August", "Korvell", "siraggi", "august@aggisoft.dk", GetNextUserID());
            MakeNewUser("August1", "Korvell1", "siraggi1", "august1@aggisoft.dk", GetNextUserID());
        }

        private int GetNextUserID()
        {
            return userList.Count + 1;
        }

        public void MakeNewUser(string firstname, string lastname, string username, string email, int userID)
        {
            if (!IsValidEmail(email))
                throw new NotAValidEmailException(email);
            else if (!isValidUsername(username))
                throw new NotAValidUsernameException(username);
            else if (!UsernameIsAvailable(username))
                throw new UsernameTakenException(username);
            else
                userList.Add(new User(firstname, lastname, username, email, GetNextUserID()));
        }

        private bool UsernameIsAvailable(string username)
        {
            foreach (User user in userList)
            {
                if (user.Username == username)
                    return false;
            }

            return true;
        }

        private bool isValidUsername(string username)
        {
            Regex regexItem = new Regex("^[a-z0-9 ]*$");

            if (regexItem.IsMatch(username))
                return true;
            else
                return false;
        }

        #region IsValidEmail

        //Code from https://msdn.microsoft.com/en-us/library/vstudio/01escwtf%28v=vs.100%29.aspx with minor adjustments.
        private bool IsValidEmail(string email)
        {
            
            if (String.IsNullOrEmpty(email))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            email = Regex.Replace(email, @"(@)(.+)$", this.DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(email,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-_\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
        #endregion
    }
}
