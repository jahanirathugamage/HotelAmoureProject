using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace hotelreservationsystem1
{
    internal class Account
    {
        // attributes
        private int accountID;
        private string accountType;
        private string username;
        private string password;

        // constructor
        public Account(string accountType, string username, string password) 
        {
            this.accountType = accountType;
            this.username = username;
            this.password = password;
        }


        // getters & setters
        public int getAccountID() 
        { 
            return accountID;
        }

        public void setAccountID(int accountID) 
        { 
            this.accountID = accountID;
        }

        public string getAccountType()
        {
            return accountType;
        }

        public void setAccountType(string accountType)
        {
            this.accountType = accountType;
        }

        public string getUsername()
        {
            return username;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public string getPassword()
        {
            return password;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }
    }
}
