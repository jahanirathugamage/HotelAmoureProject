using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    public partial class Form1 : Form
    {
        private string username;
        private string password;

        public Form1()
        {
            InitializeComponent();

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit(); // trigger application to close
        }

        private void login_registerBtn_Click(object sender, EventArgs e)
        {
            // hide the login form 
            this.Hide();

            // connect to the registration form 
            Form RegistrationForm = new RegistrationForm();
            RegistrationForm.ShowDialog();

        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            // show or hide the password when checkbox is checked
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            // AccountManager.TestDBConnection();

            // validate username and password 
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // validate user login
            bool isUserValid = AccountManager.ValidateUserLogin(username, password);
            bool isAdminValid = AccountManager.ValidateAdminLogin(username, password);

            if (isUserValid)
            {
                // open the user dashboard
                this.Hide();
                Form GuestDashboard = new GuestDashboard();
                GuestDashboard.ShowDialog();

            }
            else if (isAdminValid)
            {
                // open the admin dashboard
                this.Hide();
                Form AdminDashboard = new AdminDashboard();
                AdminDashboard.ShowDialog();

            }
            else
            {
                MessageBox.Show("Try Again! Invalid username or password.");
            }
        }

        private void login_username_TextChanged(object sender, EventArgs e)
        {
            username = login_username.Text;
        }

        private void login_password_TextChanged(object sender, EventArgs e)
        {
            password = login_password.Text;
        }
    }
}
