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
    public partial class FeedbackForm : Form
    {

        // initialize the feedback input attributes
        private Reservation reservation;
        private int rating = 0;
        private string comment = "";
        private DateTime dateSubmitted = DateTime.Now;

        public FeedbackForm(Reservation reservation) 
        {
            InitializeComponent();
            this.reservation = reservation; // store the reservation object
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ratingInput_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ratingInput.Text, out int ratingTemp))
            {
                rating = ratingTemp;
            }
            else
            {
                rating = -1;
            }
        }

        private void reviewInput_TextChanged(object sender, EventArgs e)
        {
            comment = reviewInput.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // clear the feedback fields
            ratingInput.Text = "";
            reviewInput.Text = "";
        }

        private void postBtn_Click(object sender, EventArgs e)
        {
           Feedback.createFeedback(reservation, rating, comment, dateSubmitted, this);
        }
    }
}
