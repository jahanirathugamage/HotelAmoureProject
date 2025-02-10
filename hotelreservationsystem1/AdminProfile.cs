using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    public partial class AdminProfile : Form
    {
        private int adminID;
        private string username;
        private string password;
        private string confirmPass;
        private string accountType = "admin";
        private string name;
        private string role;

        // initialize for the admin profile search
        private string adminName;

        public AdminProfile()
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
            profileTable.Columns.Add("adminID", "Admin ID");
            profileTable.Columns.Add("name", "Name");
            profileTable.Columns.Add("role", "Role"); 
        }

        private void clearCreateData() 
        {
            createAdminUsername.Text = "";
            createAdminPassword.Text = "";
            createAdminConfirmPass.Text = "";
            createAdminName.Text = "";
            createAdminRole.Text = "";
            adminProfile_showPass.Checked = false;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void offers_homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminDashboard = new AdminDashboard();
            AdminDashboard.ShowDialog();
        }

        private void offers_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminRoomAvailability = new AdminRoomAvailability();
            AdminRoomAvailability.ShowDialog();
        }

        private void offers_manageBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form RoomControlPanel = new RoomControlPanel();
            RoomControlPanel.ShowDialog();
        }

        private void offers_offersBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageOffers = new ManageOffers();
            ManageOffers.ShowDialog();
        }

        private void offers_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageFeedback = new ManageFeedback();
            ManageFeedback.ShowDialog();
        }

        private void offers_profileBtn_Click(object sender, EventArgs e)
        {

        }

        private void viewAdminID_TextChanged(object sender, EventArgs e)
        {
           adminName = viewAdminID.Text;
        }

        private void profileSearchEnterBtn_Click(object sender, EventArgs e)
        {
            profileTable.Rows.Clear();

            Admin.displayAdmin(adminName, profileTable, viewAdminID);
        }

        private void deleteOffer_confirmBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form Form1 = new Form1();
            Form1.ShowDialog();
        }

        private void adminProfile_showPass_CheckedChanged(object sender, EventArgs e)
        {
            // show or hide the password when checkbox is checked
            createAdminPassword.PasswordChar = adminProfile_showPass.Checked ? '\0' : '*';
            createAdminConfirmPass.PasswordChar = adminProfile_showPass.Checked ? '\0' : '*';
        }

        private void createAdminUsername_TextChanged(object sender, EventArgs e)
        {
            username = createAdminUsername.Text;
        }

        private void createAdminPassword_TextChanged(object sender, EventArgs e)
        {
            password = createAdminPassword.Text;
        }

        private void createAdminConfirmPass_TextChanged(object sender, EventArgs e)
        {
            confirmPass = createAdminConfirmPass.Text;
        }

        private void createAdminAccount_confirmBtn_Click(object sender, EventArgs e)
        {
            // call the create admin function
            Admin.createAdminAccount(name, role, username, password, confirmPass, accountType);

            // clear the fields
            clearCreateData();
        }

        private void createAdminName_TextChanged(object sender, EventArgs e)
        {
            name = createAdminName.Text;
        }

        private void createAdminRole_TextChanged(object sender, EventArgs e)
        {
            role = createAdminRole.Text;
        }
    }
}
