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
    public partial class ManageFeedback : Form
    {
        private int deleteFeedbackID; // initialize the delete feedback id

        public ManageFeedback()
        {
            InitializeComponent();

            InitializeTable();

            // display feedback
            Feedback.displayFeedback(FeedbackTable);
        }

        // initialise the table
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
            Form AdminDashboard = new AdminDashboard();
            AdminDashboard.ShowDialog();
        }

        private void feedback_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminRoomAvailability = new AdminRoomAvailability();
            AdminRoomAvailability.ShowDialog();
        }

        private void feedback_manageBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form RoomControlPanel = new RoomControlPanel();
            RoomControlPanel.ShowDialog();
        }

        private void feedback_offersBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageOffers = new ManageOffers();
            ManageOffers.ShowDialog();
        }

        private void feedback_feedback_Click(object sender, EventArgs e)
        {

        }

        private void feedback_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminProfile = new AdminProfile();
            AdminProfile.ShowDialog();
        }

        private void deleteFeedbackNo_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(deleteFeedbackNo.Text, out int deleteFeedbackIDTemp))
            {
                deleteFeedbackID = deleteFeedbackIDTemp;
            }
            else
            {
                deleteFeedbackID = 0;
            }
        }

        private void deleteFeedback_confirmBtn_Click(object sender, EventArgs e)
        {
            // trigger the delete feedback function
            Feedback.ManagerDeleteFeedback(deleteFeedbackID, deleteFeedbackNo);
        }
    }
}
