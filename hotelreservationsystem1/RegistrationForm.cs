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
        private string name;
        private string email;
        private string contactNumber;
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
            this.Close();

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
            Account.processRegistration(name, email, contactNumber, username, password, confirmPass, accountType);
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

        private void register_name_TextChanged(object sender, EventArgs e)
        {
            name = register_name.Text;
        }

        private void resgister_email_TextChanged(object sender, EventArgs e)
        {
            email = resgister_email.Text;
        }

        private void register_contactNumber_TextChanged(object sender, EventArgs e)
        {
            contactNumber = register_contactNumber.Text;
        }
    }
}
