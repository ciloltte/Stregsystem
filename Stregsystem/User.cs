using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem
{
    class User :IComparable
    {
        public int UserID { get { return userID; } }
        public string Firstname { get { return firstname; } }
        public string Lastname { get { return lastname; } }
        public string Username { get { return username; } }
        public string Email { get { return email; } }
        public int Balance { get { return balance; } set { balance = value; } }

        private int userID;
        private string firstname;
        private string lastname;
        private string username;
        private string email;
        private int balance;

        public User(string firstname, string lastname, string username, string email, int nextUserID)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.username = username;
            this.email = email;

            userID = nextUserID;
            balance = 0;
        }

        public override string ToString()
        {
            return "Firstname: " + Firstname + "\nEmail: " + Email;
        }

        public override bool Equals(object obj)
        {
            User other = obj as User;
            if (other == null) return false;
            return UserID.Equals(other.UserID);
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            User other = obj as User;
            return userID - other.userID;
        }
    }
}
