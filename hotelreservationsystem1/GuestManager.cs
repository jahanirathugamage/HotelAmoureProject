using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace hotelreservationsystem1
{
    internal class GuestManager
    {
        // connect to the db
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";

        // get all guests
        public static List<Guest> GetAllGuests()
        {
            List<Guest> guests = new List<Guest>();

            // get all the accounts
            List<Account> accounts = AccountManager.GetAllAccounts();

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get all guests from guests table
                    string selectAllGuestsQuery = "SELECT * FROM `guests`";
                    using (MySqlCommand mysqlCommand = new MySqlCommand(selectAllGuestsQuery, conn))
                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Get guest attributes
                            int guestID = reader.GetInt32("guestID");
                            int accountID = reader.GetInt32("accountID"); 
                            string name = reader.GetString("name");
                            string email = reader.GetString("email");
                            int contactNumber = reader.GetInt32("contactNumber");

                            // get the account
                            Account account = null;

                            foreach (Account accountItem in accounts) 
                            {
                                if (accountItem.getAccountID() == accountID) 
                                {
                                    account = accountItem;
                                }
                            }

                            guests.Add(new Guest(guestID, account, accountID, name, email, contactNumber));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return guests;
        }

        // get the guests information based on guest name
        public static Guest GetGuestInfoByName(string name)
        {
            int guestID = 0;
            int accountID = 0;
            string email = string.Empty;
            int contactNumber = 0;

            Guest guest = null;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get guest details from guests table
                    string selectGuestQuery = "SELECT * FROM `guests` WHERE name = @name";
                    using (MySqlCommand mysqlCommand = new MySqlCommand(selectGuestQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@name", name);

                        using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                guestID = reader.GetInt32("guestID");
                                accountID = reader.GetInt32("accountID");
                                email = reader.GetString("email");
                                contactNumber = reader.GetInt32("contactNumber");
                            }
                            else
                            {
                                MessageBox.Show("No guest was found.");
                                return null;
                            }

                            reader.Close(); // close reader before executing next query
                        }
                    }

                    // get account details from accountcredentials table using accountID
                    string selectAccountQuery = "SELECT username, password, accountType FROM `accountcredentials` WHERE accountID = @accountID";
                    using (MySqlCommand accountCommand = new MySqlCommand(selectAccountQuery, conn))
                    {
                        accountCommand.Parameters.AddWithValue("@accountID", accountID);

                        using (MySqlDataReader accountReader = accountCommand.ExecuteReader())
                        {
                            string username = "";
                            string password = "";
                            string accountType = "";

                            if (accountReader.Read())
                            {
                                username = accountReader.GetString("username");
                                password = accountReader.GetString("password");
                                accountType = accountReader.GetString("accountType");
                            }

                            accountReader.Close(); // make sure the reader is closed

                            // create Account object
                            Account account = new Account(accountType, username, password);

                            // create Guest object with retrieved data
                            guest = new Guest(guestID, account, accountID, name, email, contactNumber);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return guest;
        }

        // get guest from id
        public static Guest GetGuestByID(int guestID)
        {
            int accountID = 0;
            string name = string.Empty;
            string email = string.Empty;
            int contactNumber = 0;

            Guest guest = null;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get guest details from guests table
                    string selectGuestQuery = "SELECT * FROM `guests` WHERE guestID = @guestID";
                    using (MySqlCommand mysqlCommand = new MySqlCommand(selectGuestQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@guestID", guestID);

                        using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                accountID = reader.GetInt32("accountID");
                                name = reader.GetString("name");
                                email = reader.GetString("email");
                                contactNumber = reader.GetInt32("contactNumber");
                            }
                            else
                            {
                                MessageBox.Show("No guest was found.");
                                return null;
                            }

                            reader.Close(); 
                        }
                    }

                    // get account details from accountcredentials table using accountID
                    string selectAccountQuery = "SELECT username, password, accountType FROM `accountcredentials` WHERE accountID = @accountID";
                    using (MySqlCommand accountCommand = new MySqlCommand(selectAccountQuery, conn))
                    {
                        accountCommand.Parameters.AddWithValue("@accountID", accountID);

                        using (MySqlDataReader accountReader = accountCommand.ExecuteReader())
                        {
                            string username = "";
                            string password = "";
                            string accountType = "";

                            if (accountReader.Read())
                            {
                                username = accountReader.GetString("username");
                                password = accountReader.GetString("password");
                                accountType = accountReader.GetString("accountType");
                            }

                            accountReader.Close(); 

                            // create Account object
                            Account account = new Account(accountType, username, password);

                            // create Guest object with retrieved data
                            guest = new Guest(guestID, account, accountID, name, email, contactNumber);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return guest;
        }

        // get guest by username
        public static Guest GetGuestByUsername(string username)
        {
            int guestID = 0;
            int accountID = 0;
            string name = string.Empty;
            string email = string.Empty;
            int contactNumber = 0;
            string password = string.Empty;
            string accountType = "user";

            Guest guest = null;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get account details from accountcredentials table using username
                    string selectAccountQuery = "SELECT accountID, password FROM `accountcredentials` WHERE `username` = @username";
                    using (MySqlCommand accountCommand = new MySqlCommand(selectAccountQuery, conn))
                    {
                        accountCommand.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader accountReader = accountCommand.ExecuteReader())
                        {
                            if (accountReader.Read())
                            {
                                accountID = accountReader.GetInt32("accountID");
                                password = accountReader.GetString("password");
                            }
                            else
                            {
                                Console.WriteLine("No account was found for the given username.");
                                return null;
                            }
                        }
                    }

                    // get guest details from guests table
                    string selectGuestQuery = "SELECT * FROM `guests` WHERE accountID = @accountID";
                    using (MySqlCommand mysqlCommand = new MySqlCommand(selectGuestQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@accountID", accountID);

                        using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                guestID = reader.GetInt32("guestID");
                                accountID = reader.GetInt32("accountID");
                                email = reader.GetString("email");
                                contactNumber = reader.GetInt32("contactNumber");
                            }
                            else
                            {
                                MessageBox.Show("No guest was found.");
                                return null;
                            }

                            reader.Close(); // close reader before executing next query
                        }
                    }

                    // create Account 
                    Account account = new Account(accountType, username, password);

                    // create and return guest 
                    guest = new Guest(guestID, account, accountID, name, email, contactNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return guest;
        }

        // update guest name from guest id
        public static void UpdateGuestName(int guestID, string name)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateGuestNameQuery = "UPDATE guests SET name = @name WHERE guestID = @guestID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateGuestNameQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@guestID", guestID);
                        mysqlCommand.Parameters.AddWithValue("@name", name);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the guest was edited
                        if (success == 0)
                        {
                            MessageBox.Show("No guest found with the given id.");
                        }
                        else
                        {
                            MessageBox.Show("Guest updated successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update guest email from guest id
        public static void UpdateGuestEmail(int guestID, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateGuestEmailQuery = "UPDATE guests SET email = @email WHERE guestID = @guestID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateGuestEmailQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@guestID", guestID);
                        mysqlCommand.Parameters.AddWithValue("@email", email);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the guest was edited
                        if (success == 0)
                        {
                            MessageBox.Show("No guest found with the given id.");
                        }
                        else
                        {
                            MessageBox.Show("Guest updated successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update guest contact number from guest id
        public static void UpdateGuestContact(int guestID, string contactNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateGuestContactNumberQuery = "UPDATE guests SET contactNumber = @contactNumber WHERE guestID = @guestID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateGuestContactNumberQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@guestID", guestID);

                        // make sure the contactNumber is properly formatted and not null
                        if (string.IsNullOrWhiteSpace(contactNumber))
                        {
                            mysqlCommand.Parameters.AddWithValue("@contactNumber", DBNull.Value); // store NULL if empty
                        }
                        else
                        {
                            mysqlCommand.Parameters.AddWithValue("@contactNumber", contactNumber.Trim()); // store trimmed string
                        }

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the guest was edited
                        if (success == 0)
                        {
                            MessageBox.Show("No guest found with the given id.");
                        }
                        else
                        {
                            MessageBox.Show("Guest updated successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


        // delete guest from guestID
        public static bool DeleteGuest(int guestID)
        {
            bool isAccountDeleted = false;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // delete query
                    string deleteGuestQuery = "DELETE FROM guests WHERE guestID = @guestID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(deleteGuestQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@guestID", guestID);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the guest was deleted
                        if (success == 0)
                        {
                            MessageBox.Show("No guest found with the given ID.");
                        }
                        else
                        {
                            isAccountDeleted = true;
                            MessageBox.Show("Guest deleted successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return isAccountDeleted;
        }

        // delete guest account and profile
        public static bool DeleteGuestAccount(int guestID, string username) 
        {
            bool isAccountDeleted = false;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    bool isGuestReferenceDeleted;
                    bool isGuestDeleted;
                    bool isUserDeleted;

                    // get and delete reservations
                    List<Reservation> guestReservations = ReservationManager.GetGuestReservations(guestID);
                    if (guestReservations != null)
                    {
                        foreach (Reservation reservation in guestReservations)
                        {

                            FeedbackManager.DeleteFeedbackByReservationID(reservation.getReservationID());

                            ReservationManager.DeleteReservation(reservation.getReservationID());
                        }
                    }

                    // delete guest reference from accountcredentials first (to prevent foreign key issues)
                    isGuestReferenceDeleted = AccountManager.DeleteGuestReferenceFromAccountCredentials(guestID);

                    // delete the guest profile
                    isGuestDeleted = DeleteGuest(guestID);

                    // delete the user account
                    isUserDeleted = AccountManager.DeleteUserAccount(username);

                    if (isGuestDeleted && isUserDeleted)
                    {
                        transaction.Commit();
                        isAccountDeleted = true;
                    }
                    else
                    {
                        transaction.Rollback();
                        MessageBox.Show("Could not delete the account. Please try again!");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                }
            }

            return isAccountDeleted;
        }
    }
}
