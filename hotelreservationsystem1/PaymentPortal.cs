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
    public partial class PaymentPortal : Form
    {
        private Reservation reservation;

        // initalize the payment information
        private string cardHolderName;
        private string cardNumber;
        private int cvcNumber;
        private int expiryMonth;
        private int expiryYear;


        public PaymentPortal(ref Reservation res) // receive reference
        {
            InitializeComponent();
            reservation = res;
        }


        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cardHolderNameInput_TextChanged(object sender, EventArgs e)
        {
            cardHolderName = cardHolderNameInput.Text;
        }

        private void cardNumberInput_TextChanged(object sender, EventArgs e)
        {
            cardNumber = cardNumberInput.Text;
        }

        private void expiryDateMonthInput_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(expiryDateMonthInput.Text, out int expiryMonthTemp))
            {
                expiryMonth = expiryMonthTemp;
            }
            else
            {
                expiryMonth = 0;
            }
        }

        private void expiryDateYearInput_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(expiryDateYearInput.Text, out int expiryYearTemp))
            {
                expiryYear = expiryYearTemp;
            }
            else
            {
                expiryYear = 0;
            }
        }

        private void cvcInput_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cvcInput.Text, out int cvcNumberTemp))
            {
                cvcNumber = cvcNumberTemp;
            }
            else
            {
                cvcNumber = 0;
            }
        }

        private void usernameInput_TextChanged(object sender, EventArgs e){}

        private void passwordInput_TextChanged(object sender, EventArgs e){}

        private void makePaymentBtn_Click(object sender, EventArgs e)
        {
            // verify payment details
            bool verifiedPayment = Reservation.validPaymentDetails(cardHolderName, cardNumber, cvcNumber, expiryMonth, expiryYear);

            if (verifiedPayment)
            {
                // return to the available page if successful
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void label11_Click(object sender, EventArgs e){}

        private void label13_Click(object sender, EventArgs e){}

        private void label8_Click(object sender, EventArgs e) {}
    }
}
