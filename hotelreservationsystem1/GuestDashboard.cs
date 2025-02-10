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
    public partial class GuestDashboard : Form
    {
        public GuestDashboard()
        {
            InitializeComponent();

            InitializeTable();

            SpecialOffer.displayActiveSpecialOffers(activeSpecialOfferTable);
        }

        // initialize the table
        private void InitializeTable()
        {
            // clear existing columns and rows if any
            activeSpecialOfferTable.Columns.Clear();
            activeSpecialOfferTable.Rows.Clear();

            // add columns to the table
            activeSpecialOfferTable.Columns.Add("offerID", "Offer No");
            activeSpecialOfferTable.Columns.Add("offerName", "Name");
            activeSpecialOfferTable.Columns.Add("discountPercentage", "Discount");
            activeSpecialOfferTable.Columns.Add("applicableRooms", "Rooms");
            activeSpecialOfferTable.Columns.Add("startDate", "Start Date");
            activeSpecialOfferTable.Columns.Add("endDate", "End Date");
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void home_homeBtn_Click(object sender, EventArgs e){}

        private void home_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AvailableRooms = new AvailableRooms();
            AvailableRooms.ShowDialog();
        }

        private void home_reserveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ReservationPanel = new ReservationPanel();
            ReservationPanel.ShowDialog();
        }

        private void home_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form GuestProfile = new GuestProfile();
            GuestProfile.ShowDialog();
        }

        private void home_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form FeedbackForum = new FeedbackForum();   
            FeedbackForum.ShowDialog();
        }
    }
}
