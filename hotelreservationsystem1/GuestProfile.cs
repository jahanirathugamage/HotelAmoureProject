using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace hotelreservationsystem1
{
    public partial class GuestProfile : Form
    {
        // initialize the view guest information attributes
        private string getProfileName;

        private string updateUsername;
        private string updatePassword;
        private string name;
        private string email;
        private string contactNumber;

        // initialize the checkbox values
        private bool isNameChecked;
        private bool isEmailChecked;
        private bool isPhoneChecked;

        // initialize the account deletion values
        private string deleteUsername;
        private string deletePassword;

        public GuestProfile()
        {
            InitializeComponent();

            InitializeTable();
        }

        private void InitializeTable()
        {
            // clear existing columns and rows if any
            profileTable.Columns.Clear();
            profileTable.Rows.Clear();

            // add columns to the table
            profileTable.Columns.Add("name", "Name");
            profileTable.Columns.Add("email", "Email");
            profileTable.Columns.Add("contactNumber", "Contact");
        }

        // clear the update guest data fields
        private void clearUpdateGuestData() 
        {
            editUsername.Text = "";
            editPassword.Text = "";
            updateGuestName.Text = "";
            updateGuestEmail.Text = "";
            updateGuestContact.Text = "";
            updateNameCheckbox.Checked = false;
            updateEmailCheckbox.Checked = false;
            updateContactNumberTextbox.Checked = false;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void profile_homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form GuestDashboard = new GuestDashboard();
            GuestDashboard.ShowDialog();
        }

        private void profile_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AvailableRooms = new AvailableRooms();
            AvailableRooms.ShowDialog();
        }

        private void profile_reserveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ReservationPanel = new ReservationPanel();
            ReservationPanel.ShowDialog();
        }

        private void profile_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form FeedbackForum = new FeedbackForum();
            FeedbackForum.ShowDialog();
        }

        private void profile_profileBtn_Click(object sender, EventArgs e){}

        private void viewGuestID_TextChanged(object sender, EventArgs e)
        {
            getProfileName = viewGuestID.Text;
        }

        private void profileSearchEnterBtn_Click(object sender, EventArgs e)
        {
            profileTable.Rows.Clear();

            Guest.displayGuestInfo(getProfileName, profileTable, viewGuestID);
        }

        private void updateGuestID_TextChanged(object sender, EventArgs e){}

        private void updateGuestName_TextChanged(object sender, EventArgs e)
        {
            name = updateGuestName.Text;
        }

        private void updateGuestEmail_TextChanged(object sender, EventArgs e)
        {
            email = updateGuestEmail.Text;
        }

        private void updateGuestContact_TextChanged(object sender, EventArgs e)
        {
            contactNumber = updateGuestContact.Text;
        }

        private void updateNameCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            isNameChecked = updateNameCheckbox.Checked;
        }

        private void updateEmailCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            isEmailChecked = updateEmailCheckbox.Checked;
        }

        private void updateContactNumberTextbox_CheckedChanged(object sender, EventArgs e)
        {
            isPhoneChecked = updateContactNumberTextbox.Checked;
        }

        private void createAdminAccount_confirmBtn_Click(object sender, EventArgs e)
        {
            Guest.updateGuestProfile(name, email, contactNumber, updateUsername, updatePassword, isNameChecked, isEmailChecked, isPhoneChecked);

            clearUpdateGuestData();
        }

        private void deleteGuestUsername_TextChanged(object sender, EventArgs e)
        {
            deleteUsername = deleteGuestUsername.Text;
        }

        private void deleteGuestPassword_TextChanged(object sender, EventArgs e)
        {
            deletePassword = deleteGuestPassword.Text;
        }

        private void deleteOffer_confirmBtn_Click(object sender, EventArgs e)
        {
            // trigger the deletion of the user account
            Guest.DeleteGuestAccount(deleteUsername, deletePassword, this);

        }

        private void editUsername_TextChanged(object sender, EventArgs e)
        {
            updateUsername = editUsername.Text;
        }

        private void editPassword_TextChanged(object sender, EventArgs e)
        {
            updatePassword = editPassword.Text;
        }
    }
}
