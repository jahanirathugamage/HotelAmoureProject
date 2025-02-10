using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace hotelreservationsystem1
{
    public class Account
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

        // login process
        public static void processLogin(string username, string password, Form Form1)
        {
            try
            {
                // validate username and password 
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Please enter username.");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter password.");
                    return;
                }

                // validate user login
                bool isUserValid = AccountManager.ValidateUserLogin(username, password);
                bool isAdminValid = AccountManager.ValidateAdminLogin(username, password);

                if (isUserValid)
                {
                    // open the user dashboard
                    Form1.Hide();

                    Form GuestDashboard = new GuestDashboard();
                    GuestDashboard.ShowDialog();

                }
                else if (isAdminValid)
                {
                    // open the admin dashboard
                    Form1.Hide();

                    Form AdminDashboard = new AdminDashboard();
                    AdminDashboard.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Try Again! Invalid username or password.");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // register process
        public static void processRegistration(string name, string email, string contactNumber, string username, string password, string confirmPass, string accountType) 
        {
            try
            {
                // check if anything is null
                if (string.IsNullOrEmpty(name)) // the name can have white spaces
                {
                    MessageBox.Show("Please enter name.");
                }
                if (string.IsNullOrWhiteSpace(email)) 
                {
                    MessageBox.Show("Please enter email.");
                }
                if (string.IsNullOrWhiteSpace(contactNumber)) 
                { 
                    MessageBox.Show("Please enter contactNumber.");
                }
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Please enter username.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter password.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(confirmPass))
                {
                    MessageBox.Show("Please enter confirm password");
                    return;
                }
                // check if the email has "@gmail.com"
                if (!email.Contains("@gmail.com"))
                {
                    MessageBox.Show("Please enter a valid email with '@gmail.com'.");
                    return;
                }
                // check for whitespaces in username or password
                if (username.Contains(" ") || password.Contains(" "))
                {
                    MessageBox.Show("Username and password cannot contain spaces.");
                    return;
                }
                // check if password is at least 8 characters long
                else if (password.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters long.");
                    return;
                }

                // check if password matches confirmation password
                else if (password != confirmPass)
                {
                    MessageBox.Show("Password and confirmation password do not match.");
                    return;
                }

                // check if the username and password already exist 
                else if (AccountManager.searchAccountDBUsernames(username) || AccountManager.searchAccountDBPasswords(password))
                {
                    MessageBox.Show("Unavailable credentials. Please choose a different username and password.");
                    return;
                }
                else
                {
                    // create account
                    Account newAccount = new Account(accountType, username, password);

                    // add account and guest
                    AccountManager.AddNewGuest(newAccount, name, email, contactNumber);

                    // Show success message and redirect to login form
                    MessageBox.Show("Registration successful!");


                    // redirect to login by hiding register page
                    Form RegisterForm = new RegistrationForm();
                    RegisterForm.Hide();

                    Form Form1 = new Form1();
                    Form1.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
