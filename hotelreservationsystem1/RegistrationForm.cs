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
    public partial class RegistrationForm: Form
    {

        private string username;
        private string password;
        private string confirmPass;
        private string accountType = "user";

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void register_signinBtn_Click(object sender, EventArgs e)
        {
            // hide the registration form 
            this.Hide();

            // connect to the login form 
            Form Form1 = new Form1();
            Form1.ShowDialog();

        }

        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            // show or hide the password when checkbox is checked
            register_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
            register_confirmPass.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        private void register_signupBtn_Click(object sender, EventArgs e)
        {
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
                AccountManager.AddNewUser(newAccount);

                // Show success message and redirect to login form
                MessageBox.Show("Registration successful!");

                // redirect to login 
                this.Hide();
                
                Form Form1 = new Form1();
                Form1.ShowDialog();
            }
        }

        private void register_username_TextChanged(object sender, EventArgs e)
        {
            username = register_username.Text;
        }

        private void register_password_TextChanged(object sender, EventArgs e)
        {
            password = register_password.Text;
        }

        private void register_confirmPass_TextChanged(object sender, EventArgs e)
        {
            confirmPass = register_confirmPass.Text;
        }
    }
}
