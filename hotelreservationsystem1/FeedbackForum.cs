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
    public partial class FeedbackForum : Form
    {
        public FeedbackForum()
        {
            InitializeComponent();

            InitializeTable();

            // display feedback
            Feedback.displayFeedback(FeedbackTable);
        }

        private void InitializeTable()
        {
            // clear existing columns and rows if any
            FeedbackTable.Columns.Clear();
            FeedbackTable.Rows.Clear();

            // add columns to the table
            FeedbackTable.Columns.Add("feedbackID", "Feedback No");
            FeedbackTable.Columns.Add("comment", "Comment");
            FeedbackTable.Columns.Add("rating", "Rating");
            FeedbackTable.Columns.Add("roomID", "Room No");
            FeedbackTable.Columns.Add("dateSubmitted", "Posted");
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void feedback_homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form GuestDashboard = new GuestDashboard();
            GuestDashboard.ShowDialog();
        }

        private void feedback_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AvailableRooms = new AvailableRooms();
            AvailableRooms.ShowDialog();
        }

        private void feedback_reserveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ReservationPanel = new ReservationPanel();
            ReservationPanel.ShowDialog();
        }

        private void feedback_feedback_Click(object sender, EventArgs e)
        {
            
        }

        private void feedback_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form GuestProfile = new GuestProfile();
            GuestProfile.ShowDialog();
        }

        private void deleteFeedbackNo_TextChanged(object sender, EventArgs e){}

        private void inputUsername_TextChanged(object sender, EventArgs e){}

        private void inputPassword_TextChanged(object sender, EventArgs e){}

        private void deleteFeedback_confirmBtn_Click(object sender, EventArgs e){}

        private void nameInput_TextChanged(object sender, EventArgs e){}

        private void FeedbackSearchBtn_Click(object sender, EventArgs e){}
    }
}
