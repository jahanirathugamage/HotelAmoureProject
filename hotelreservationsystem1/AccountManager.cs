using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace hotelreservationsystem1
{
    internal class AccountManager
    {
        // connect to the db
        private static string connectionstring = "server=localhost; database=hotel_amoure; user=root; password=;";

        // testing the connection to the database
        public static void TestDBConnection() 
        {
  
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Connection Successful");
                }
                catch (Exception ex) 
                { 
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        // search account username
        public static bool searchAccountDBUsernames(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // Query to check if the username exists
                    string searchQuery = "SELECT * FROM `accountcredentials` WHERE `username` = @username";

                    MySqlCommand mysqlCommand = new MySqlCommand(searchQuery, conn);
                    mysqlCommand.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        // If found, return true
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return false; // Return false if not found in db
        }

        // search account password
        public static bool searchAccountDBPasswords(string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // Query to check if the password exists
                    string searchQuery = "SELECT * FROM `accountcredentials` WHERE `password` = @password";

                    MySqlCommand mysqlCommand = new MySqlCommand(searchQuery, conn);
                    mysqlCommand.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        // If found, return true
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return false; // Return false if not found in db
        }

        // validate User Login
        public static bool ValidateUserLogin(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // Query to check if the username and password are correct and correspond, and accountType is 'user'
                    string query = "SELECT * FROM `accountcredentials` WHERE `username` = @username AND `password` = @password AND `accountType` = 'user'";

                    MySqlCommand mysqlCommand = new MySqlCommand(query, conn);
                    mysqlCommand.Parameters.AddWithValue("@username", username);
                    mysqlCommand.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        // If found, return true
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return false; // Return false if not found in db
        }

        // Validate Admin Login
        public static bool ValidateAdminLogin(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // Query to check if the username and password are correct and correspond, and accountType is 'admin'
                    string getCredentialsquery = "SELECT * FROM `accountcredentials` WHERE `username` = @username AND `password` = @password AND `accountType` = 'admin'";

                    MySqlCommand mysqlCommand = new MySqlCommand(getCredentialsquery, conn);
                    mysqlCommand.Parameters.AddWithValue("@username", username);
                    mysqlCommand.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        // If found, return true
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return false; // Return false if not found
        }

        // add user to the db
        public static void AddNewUser(Account newUser) 
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // check if the username and password are correct and correspond, and accountType is 'admin'
                    string insertUserquery = "INSERT INTO accountcredentials (accountType, username, password) VALUES (@accountType, @username, @password)";

                    // get values
                    string accountType = newUser.getAccountType();
                    string username = newUser.getUsername();
                    string password = newUser.getPassword();

                    // insert the values
                    MySqlCommand mysqlCommand = new MySqlCommand(insertUserquery, conn);
                    mysqlCommand.Parameters.AddWithValue("@accountType", accountType);
                    mysqlCommand.Parameters.AddWithValue("@username", username);
                    mysqlCommand.Parameters.AddWithValue("@password", password);

                    // run query
                    mysqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
