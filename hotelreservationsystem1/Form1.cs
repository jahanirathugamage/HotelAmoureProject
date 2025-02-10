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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

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
            // close the login form 
            this.Close();

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
            Account.processLogin(username,password,this);
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
