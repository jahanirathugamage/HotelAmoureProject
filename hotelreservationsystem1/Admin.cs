using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using System.Xml.Linq;

namespace hotelreservationsystem1
{
    public class Admin : Person
    {
        // attributes 
        private int adminID;
        private string role;

        // constructor
        public Admin(int accountID, Account account, string name, int adminID, string role) : base(accountID, account, name)
        {
            this.adminID = adminID;
            this.role = role;
        }

        // Getters and setters
        public int getAdminID() 
        { 
            return adminID; 
        }
        public string getRole() 
        {
            return role;
        }

        public void setAdminID(int adminID) 
        { 
            this.adminID = adminID; 
        }

        public void setRole(string role) 
        { 
            this.role = role;
        }

        // display admin data on a table
        public static void addAdminInfoToTable(Admin admin, DataGridView profileTable)
        {
            string adminID = admin.getAdminID().ToString();
            string name = admin.getName();
            string role = admin.getRole();

            // add data to the row
            profileTable.Rows.Add(adminID, name, role);
        }

        public static void displayAdmin(string adminName, DataGridView profileTable, TextBox viewAdminID) 
        {
            try
            {
                // get the admin
                Admin admin = AdminManager.GetAdminInfoByName(adminName);

                // check if admin exists before adding to table
                if (admin != null)
                {
                    // populate the table row
                    Admin.addAdminInfoToTable(admin, profileTable);
                }
                else
                {
                    MessageBox.Show("Invalid Admin Name, try again.");
                }

                viewAdminID.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // create admin account
        public static void createAdminAccount(string name, string role, string username, string password, string confirmPass, string accountType)
        {
            try
            {
                // check if name was entered
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter a name.");
                    return;
                }
                // check if role was entered
                else if (string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Please enter a role.");
                    return;
                }
                // check if username or password is null
                else if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Please enter username.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter password.");
                    return;
                }

                // check if confirm password is null
                if (string.IsNullOrWhiteSpace(confirmPass))
                {
                    MessageBox.Show("Please enter confirm password");
                    return;
                }

                // check if the username and password are already chosen
                if (AccountManager.searchAccountDBUsernames(username) || AccountManager.searchAccountDBPasswords(password))
                {
                    MessageBox.Show("Unavailable credentials. Please choose a different username and password.");
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

                    // add account
                    AccountManager.AddNewAdmin(newAccount, name, role);

                    MessageBox.Show("Registration successful!");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
