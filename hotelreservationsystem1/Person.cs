using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace hotelreservationsystem1
{
    public abstract class Person
    {
        private int accountID;
        private Account account; // composition - Person cannot exist without Account
        private string name;

        public Person(int accountID, Account account, string name) 
        { 
            this.accountID = accountID;
            this.account = account ?? throw new ArgumentNullException(nameof(account), "Account cannot be null."); // this is to make sure the Person class cannot exist without an Account
            this.name = name;
        }

        public Person(string name)
        {
            this.name = name;
        }

        public int getAccountID()
        {
            return accountID;
        }

        public void setAccountID(int accountID)
        {
            this.accountID = accountID;
        }

        public Account getAccount() 
        { 
            return account;
        }

        public void RemovePerson()
        {
            account = null;  // Person is deleted when Account is removed
        }

        public void setAccount(Account account) 
        { 
            this.account = account;
        }

        public string getName() 
        { 
            return name;
        }

        public void setName(string name) 
        {
            this.name = name;
        }
    }
}
