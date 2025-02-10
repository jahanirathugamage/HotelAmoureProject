using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    internal class AdminManager
    {
        // connect to the db
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";

        // get all admin
        public static List<Admin> GetAllAdmins() 
        {
            List<Admin> admins = new List<Admin>();

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string selectAllAdminsQuery = "SELECT * FROM `admins`";
                    MySqlCommand mysqlCommand = new MySqlCommand(selectAllAdminsQuery, conn);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Get guest attributes
                            int adminID = reader.GetInt32("adminID");
                            int accountID = reader.GetInt32("accountID");
                            string name = reader.GetString("name");
                            string role = reader.GetString("role");

                            string username = string.Empty;
                            string password = string.Empty;
                            string accountType = string.Empty;

                            // query to get account credentials based on accountID
                            string selectAccountQuery = "SELECT username, password, accountType FROM `accountcredentials` WHERE accountID = @accountID";
                            using (MySqlCommand accountCommand = new MySqlCommand(selectAccountQuery, conn))
                            {
                                accountCommand.Parameters.AddWithValue("@accountID", accountID);
                                using (MySqlDataReader accountReader = accountCommand.ExecuteReader())
                                {
                                    if (accountReader.Read())
                                    {
                                        username = accountReader.GetString("username");
                                        password = accountReader.GetString("password");
                                        accountType = accountReader.GetString("accountType");
                                    }
                                }
                            }

                            // create Account object
                            Account account = new Account(accountType, username, password);

                            // create Guest object
                            Admin admin = new Admin(accountID, account, name, adminID, role);

                            admins.Add(admin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return admins;
        }

        // get admin information based on admin name
        public static Admin GetAdminInfoByName(string adminName)
        {
            int accountID = 0;
            int adminID = 0;
            string role = string.Empty;
            string username = string.Empty;
            string password = string.Empty;
            string accountType = string.Empty;

            Admin admin = null;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // query to get admin details by name
                    string selectAdminQuery = "SELECT * FROM `admins` WHERE name = @adminName LIMIT 1";
                    using (MySqlCommand mysqlCommand = new MySqlCommand(selectAdminQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@adminName", adminName);

                        using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                adminID = reader.GetInt32("adminID");
                                accountID = reader.GetInt32("accountID");
                                role = reader.GetString("role");
                            }
                            else
                            {
                                Console.WriteLine("Admin not found.");
                                return null;
                            }
                        }
                    }

                    // check if accountID is valid before querying account credentials
                    Account account = null;
                    if (accountID > 0)
                    {
                        // query to get account credentials based on accountID
                        string selectAccountQuery = "SELECT username, password, accountType FROM `accountcredentials` WHERE accountID = @accountID";
                        using (MySqlCommand accountCommand = new MySqlCommand(selectAccountQuery, conn))
                        {
                            accountCommand.Parameters.AddWithValue("@accountID", accountID);

                            using (MySqlDataReader accountReader = accountCommand.ExecuteReader())
                            {
                                if (accountReader.Read())
                                {
                                    username = accountReader.GetString("username");
                                    password = accountReader.GetString("password");
                                    accountType = accountReader.GetString("accountType");
                                }
                                else
                                {
                                    Console.WriteLine("Account credentials not found.");
                                    return null;
                                }
                            }
                        }

                        // create Account object only if valid credentials were retrieved
                        account = new Account(accountType, username, password);
                    }
                    else
                    {
                        Console.WriteLine("Invalid accountID.");
                        return null;
                    }

                    // make sure the account is valid before creating the Admin object
                    if (account != null)
                    {
                        admin = new Admin(accountID, account, adminName, adminID, role);
                    }
                    else
                    {
                        Console.WriteLine("Failed to get account information.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return admin;
        }
    }
}
