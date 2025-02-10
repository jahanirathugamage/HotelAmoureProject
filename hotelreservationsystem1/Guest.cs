using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace hotelreservationsystem1
{
    public class Guest : Person
    {
        private int guestID;
        private string email;
        private int contactNumber;
        public List<Feedback> Feedbacks { get; private set; } = new List<Feedback>();

        // constructor
        public Guest(int guestID, Account account, int accountID, string name, string email, int contactNumber) : base(accountID, account, name)
        {
            this.guestID = guestID;
            this.email = email;
            this.contactNumber = contactNumber;
        }

        public void AddFeedback(Feedback feedback)
        {
            Feedbacks.Add(feedback);
        }

        // remove guest and all their feedback
        public void RemoveGuest()
        {
            Feedbacks.Clear();
        }

        // getters & setters

        public int getGuestID() 
        {
            return guestID;
        }

        public string getEmail()
        {
            return email;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }

        public int getContactNumber()
        {
            return contactNumber;
        }

        public void setContactNumber(int contactNumber)
        {
            this.contactNumber = contactNumber;
        }

        // display guest data on a table
        public static void addGuestInfoToTable(Guest guest, DataGridView profileTable)
        {
            // attributes
            string name = guest.getName();
            string email = guest.getEmail();
            string contactNumber = guest.getContactNumber().ToString();

            // Add data to the row
            profileTable.Rows.Add(name, email, contactNumber);
        }

        public static void displayGuestInfo(string getProfileName, DataGridView profileTable, TextBox viewGuestID) 
        {
            try
            {
                // get the guest
                Guest guest = GuestManager.GetGuestInfoByName(getProfileName);

                if (guest != null)
                {
                    // populate the table row
                    Guest.addGuestInfoToTable(guest, profileTable);
                }
                else
                {
                    MessageBox.Show("Invalid Guest Name, try again.");
                }

                viewGuestID.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // update guest profile
        public static void updateGuestProfile(string updateName, string updateEmail, string updateContactNumber, string updateUsername, string updatePassword, bool isNameChecked, bool isEmailChecked, bool isPhoneChecked)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(updateUsername))
                {
                    MessageBox.Show("Please enter username.");
                    return;
                }
                if (string.IsNullOrEmpty(updatePassword))
                {
                    MessageBox.Show("Please enter password.");
                    return;
                }

                // validate user credentials
                bool isUserValid = AccountManager.ValidateUserLogin(updateUsername, updatePassword);

                if (!isUserValid)
                {
                    MessageBox.Show("Invalid user credentials, try again.");
                    return;
                }

                // get the guest 
                Guest updateGuest = GuestManager.GetGuestByUsername(updateUsername);

                // check if the guest was loaded
                if (updateGuest == null)
                {
                    MessageBox.Show("Guest not found.");
                    return;
                }

                if (!isNameChecked && !isEmailChecked && !isPhoneChecked)
                {
                    MessageBox.Show("Please choose an update.");
                    return;
                }

                // update guest attributes based on checked checkboxes

                int guestID = updateGuest.getGuestID(); 

                if (isNameChecked)
                {
                    if (string.IsNullOrWhiteSpace(updateName))
                    {
                        MessageBox.Show("Please enter a name.");
                    }
                    else
                    {
                        GuestManager.UpdateGuestName(guestID, updateName);
                    }
                }

                if (isEmailChecked)
                {
                    if (string.IsNullOrWhiteSpace(updateEmail))
                    {
                        MessageBox.Show("Please enter an email.");
                    }
                    else if (!updateEmail.Contains("@gmail.com"))
                    {
                        MessageBox.Show("Please enter a valid email with '@gmail.com'.");
                    }
                    else
                    {
                        GuestManager.UpdateGuestEmail(guestID, updateEmail);
                    }
                }

                if (isPhoneChecked)
                {
                    if (string.IsNullOrWhiteSpace(updateContactNumber))
                    {
                        MessageBox.Show("Please enter the contact number.");
                    }
                    else
                    {
                        GuestManager.UpdateGuestContact(guestID, updateContactNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // validate details for deleting guest account
        public static void DeleteGuestAccount(string username, string password, Form guestProfile)
        {
            try
            {

                // validate user credentials
                bool isAccountFound = AccountManager.ValidateUserLogin(username, password);

                if (!isAccountFound)
                {
                    MessageBox.Show("Incorrect username or password. Please try again!");
                    return;
                }

                // Retrieve account details
                Account account = AccountManager.GetAccountByUsername(username);
                if (account == null)
                {
                    MessageBox.Show("Unable to retrieve guest account details.");
                    return;
                }

                // check if the user is a guest
                if (!account.getAccountType().Equals("user", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Only guest accounts can be deleted.");
                    return;
                }

                // Retrieve guest details
                Guest guest = GuestManager.GetGuestByUsername(username);
                if (guest == null)
                {
                    MessageBox.Show("Guest profile not found.");
                    return;
                }

                int guestID = guest.getGuestID();

                // delete the guest
                bool isUserDeleted = GuestManager.DeleteGuestAccount(guestID, username);

                // if the user is deleted, close the guest view and redirect back to the login form
                if (isUserDeleted) 
                {
                    guestProfile.Close();
                    Form1 loginForm = new Form1();
                    loginForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
