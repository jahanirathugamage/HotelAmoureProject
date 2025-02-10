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
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";

        // get all accounts
        public static List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    string getAllAccountsQuery = "SELECT * FROM `accountcredentials`";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(getAllAccountsQuery, conn))
                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int accountID = reader.GetInt32("accountID");
                            string username = reader.GetString("username");
                            string password = reader.GetString("password");
                            string accountType = reader.GetString("accountType");

                            // create an Account object and set accountID
                            Account account = new Account(accountType, username, password);
                            account.setAccountID(accountID);

                            // add to list
                            accounts.Add(account);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return accounts;
        }


        // get accounts by username
        public static Account GetAccountByUsername(string username)
        {
            Account account = null;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    string getAccountQuery = "SELECT accountID, username, password, accountType FROM accountcredentials WHERE username = @username";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(getAccountQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int accountID = reader.GetInt32("accountID");
                                string retrievedUsername = reader.GetString("username");
                                string password = reader.GetString("password");
                                string accountType = reader.GetString("accountType");

                                account = new Account(accountType, retrievedUsername, password);

                                account.setAccountID(accountID);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return account;
        }


        // search account username
        public static bool searchAccountDBUsernames(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // check if the username exists
                    string searchQuery = "SELECT * FROM `accountcredentials` WHERE `username` = @username";

                    MySqlCommand mysqlCommand = new MySqlCommand(searchQuery, conn);
                    mysqlCommand.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        // if found, return true
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return false; // return false if not found in db
        }

        // search account password
        public static bool searchAccountDBPasswords(string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // check if the password exists
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
                    Console.WriteLine(ex.ToString());
                }
            }
            return false; // return false if not found in db
        }

        // validate User Login
        public static bool ValidateUserLogin(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // check if the username and password are correct and correspond, and accountType is 'user'
                    string query = "SELECT * FROM `accountcredentials` WHERE `username` = @username AND `password` = @password AND `accountType` = 'user'";

                    MySqlCommand mysqlCommand = new MySqlCommand(query, conn);
                    mysqlCommand.Parameters.AddWithValue("@username", username);
                    mysqlCommand.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        // if found, return true
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return false; // return false if not found in db
        }

        // validate Admin Login
        public static bool ValidateAdminLogin(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // check if the username and password are correct and correspond, and accountType is 'admin'
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
                    Console.WriteLine(ex.ToString());
                }
            }
            return false; // return false if not found
        }


        // add a guest to the accountcredentials and guests table
        public static void AddNewGuest(Account newAccount, string name, string email, string contactNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction(); // start transaction

                    // get account details
                    string accountType = newAccount.getAccountType();
                    string username = newAccount.getUsername();
                    string password = newAccount.getPassword();

                    // insert into accountcredentials first
                    string insertUserQuery = "INSERT INTO accountcredentials (accountType, username, password) VALUES (@accountType, @username, @password)";
                    MySqlCommand userCommand = new MySqlCommand(insertUserQuery, conn, transaction);
                    userCommand.Parameters.AddWithValue("@accountType", accountType);
                    userCommand.Parameters.AddWithValue("@username", username);
                    userCommand.Parameters.AddWithValue("@password", password);

                    userCommand.ExecuteNonQuery();

                    // retrieve the generated accountID
                    string getAccountIdQuery = "SELECT LAST_INSERT_ID();";
                    MySqlCommand getAccountIdCommand = new MySqlCommand(getAccountIdQuery, conn, transaction);
                    int accountID = Convert.ToInt32(getAccountIdCommand.ExecuteScalar());

                    // insert into guests, linking to the accountID
                    string insertGuestQuery = "INSERT INTO guests (accountID, name, email, contactNumber) VALUES (@accountID, @name, @email, @contactNumber);";
                    MySqlCommand guestCommand = new MySqlCommand(insertGuestQuery, conn, transaction);
                    guestCommand.Parameters.AddWithValue("@accountID", accountID);
                    guestCommand.Parameters.AddWithValue("@name", name);
                    guestCommand.Parameters.AddWithValue("@email", email);
                    guestCommand.Parameters.AddWithValue("@contactNumber", contactNumber);

                    guestCommand.ExecuteNonQuery();

                    // retrieve the generated guestID
                    string getGuestIdQuery = "SELECT LAST_INSERT_ID();";
                    MySqlCommand getGuestIdCommand = new MySqlCommand(getGuestIdQuery, conn, transaction);
                    int guestID = Convert.ToInt32(getGuestIdCommand.ExecuteScalar());

                    // update the accountcredentials table with the guestID
                    string updateAccountQuery = "UPDATE accountcredentials SET guestID = @guestID WHERE accountID = @accountID";
                    MySqlCommand updateAccountCommand = new MySqlCommand(updateAccountQuery, conn, transaction);
                    updateAccountCommand.Parameters.AddWithValue("@guestID", guestID);
                    updateAccountCommand.Parameters.AddWithValue("@accountID", accountID);

                    updateAccountCommand.ExecuteNonQuery();

                    // commit transaction if everything is successful
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // add an admin to the accountcredentials and admins tables
        public static void AddNewAdmin(Account newAdmin, string name, string role)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction(); // start transaction

                    // get account details
                    string accountType = newAdmin.getAccountType();
                    string username = newAdmin.getUsername();
                    string password = newAdmin.getPassword();

                    // insert into accountcredentials first
                    string insertUserQuery = "INSERT INTO accountcredentials (accountType, username, password) VALUES (@accountType, @username, @password)";
                    MySqlCommand userCommand = new MySqlCommand(insertUserQuery, conn, transaction);
                    userCommand.Parameters.AddWithValue("@accountType", accountType);
                    userCommand.Parameters.AddWithValue("@username", username);
                    userCommand.Parameters.AddWithValue("@password", password);

                    userCommand.ExecuteNonQuery();

                    // retrieve the generated accountID
                    string getAccountIdQuery = "SELECT LAST_INSERT_ID();";
                    MySqlCommand getAccountIdCommand = new MySqlCommand(getAccountIdQuery, conn, transaction);
                    int accountID = Convert.ToInt32(getAccountIdCommand.ExecuteScalar());

                    // insert into admins, linking to the accountID
                    string insertAdminQuery = "INSERT INTO admins (accountID, name, role) VALUES (@accountID, @name, @role);";
                    MySqlCommand adminCommand = new MySqlCommand(insertAdminQuery, conn, transaction);
                    adminCommand.Parameters.AddWithValue("@accountID", accountID);
                    adminCommand.Parameters.AddWithValue("@name", name);
                    adminCommand.Parameters.AddWithValue("@role", role);

                    adminCommand.ExecuteNonQuery();

                    // retrieve the generated adminID
                    string getAdminIdQuery = "SELECT LAST_INSERT_ID();";
                    MySqlCommand getAdminIdCommand = new MySqlCommand(getAdminIdQuery, conn, transaction);
                    int adminID = Convert.ToInt32(getAdminIdCommand.ExecuteScalar());

                    // update the accountcredentials table with the adminID
                    string updateAccountQuery = "UPDATE accountcredentials SET adminID = @adminID WHERE accountID = @accountID";
                    MySqlCommand updateAccountCommand = new MySqlCommand(updateAccountQuery, conn, transaction);
                    updateAccountCommand.Parameters.AddWithValue("@adminID", adminID);
                    updateAccountCommand.Parameters.AddWithValue("@accountID", accountID);

                    updateAccountCommand.ExecuteNonQuery();

                    // commit transaction if everything is successful
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // delete the guestID key that corresponds to the guest being deleted
        public static bool DeleteGuestReferenceFromAccountCredentials(int guestID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionstring))
                {
                    conn.Open();
                    string query = "DELETE FROM accountcredentials WHERE guestID = @guestID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@guestID", guestID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        // delete user account by user name and password 
        public static bool DeleteUserAccount(string username)
        {
            bool isAccountDeleted = false;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction(); 

                    // get accountID from accountcredentials
                    string getAccountIDQuery = "SELECT accountID FROM accountcredentials WHERE username = @username";
                    int accountID = -1;

                    using (MySqlCommand getAccountIDCmd = new MySqlCommand(getAccountIDQuery, conn, transaction))
                    {
                        getAccountIDCmd.Parameters.AddWithValue("@username", username);
                        object result = getAccountIDCmd.ExecuteScalar();

                        if (result != null)
                        {
                            accountID = Convert.ToInt32(result);
                        }
                        else
                        {
                            return false; // exit early if no account found
                        }
                    }

                    // get guestID from using accountID
                    string getGuestIDQuery = "SELECT guestID FROM guests WHERE accountID = @accountID";
                    int guestID = -1;

                    using (MySqlCommand getGuestIDCmd = new MySqlCommand(getGuestIDQuery, conn, transaction))
                    {
                        getGuestIDCmd.Parameters.AddWithValue("@accountID", accountID);
                        object result = getGuestIDCmd.ExecuteScalar();

                        if (result != null)
                        {
                            guestID = Convert.ToInt32(result);
                        }
                    }

                    // delete from guests first to avoid FK constraints
                    if (guestID != -1)
                    {
                        string deleteGuestQuery = "DELETE FROM guests WHERE guestID = @guestID";
                        using (MySqlCommand deleteGuestCmd = new MySqlCommand(deleteGuestQuery, conn, transaction))
                        {
                            deleteGuestCmd.Parameters.AddWithValue("@guestID", guestID);
                            int guestDeleted = deleteGuestCmd.ExecuteNonQuery();

                            if (guestDeleted == 0)
                            {
                                transaction.Rollback(); // rollback in case of failure
                                return false;
                            }
                        }
                    }

                    // delete from accountcredentials
                    string deleteAccountQuery = "DELETE FROM accountcredentials WHERE accountID = @accountID";
                    using (MySqlCommand deleteAccountCmd = new MySqlCommand(deleteAccountQuery, conn, transaction))
                    {
                        deleteAccountCmd.Parameters.AddWithValue("@accountID", accountID);
                        int accountDeleted = deleteAccountCmd.ExecuteNonQuery();

                        if (accountDeleted == 0)
                        {
                            transaction.Rollback(); // rollback in case of failure
                            return false;
                        }
                    }

                    // commit transaction if both deletions were successful
                    transaction.Commit();
                    isAccountDeleted = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return isAccountDeleted;
        }

        // delete guest from guestID
        public static void DeleteAccount(int accountID)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // delete query
                    string deleteAccountQuery = "DELETE FROM accountcredentials WHERE accountID = @accountID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(deleteAccountQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@accountID", accountID);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the guest was deleted
                        if (success == 0)
                        {
                            Console.WriteLine("No guest found with the given ID.");
                        }
                        else
                        {
                            MessageBox.Show("Guest deleted successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

    }
}
