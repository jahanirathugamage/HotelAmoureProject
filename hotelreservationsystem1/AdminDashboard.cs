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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();

            InitializeTables();

            //// display the room occupancy report
            RoomOccupancyReport.displayOccupancyReport(roomOccupancyReportTable);

            // display the revenue report
            RevenueReport.displayRevenuReport(revenueReportTable);

            // display the average guest stay report
            RoomOccupancyReport.displayAverageStayReport(avgStayDurationResult);

        }

        private void InitializeTables() 
        {
            // clear existing columns and rows if any
            roomOccupancyReportTable.Columns.Clear();
            roomOccupancyReportTable.Rows.Clear();

            revenueReportTable.Columns.Clear();
            revenueReportTable.Rows.Clear();

            // add columns to the tables
            roomOccupancyReportTable.Columns.Add("roomType", "Room Type");
            roomOccupancyReportTable.Columns.Add("OccupancyRate", "Occupancy Rate");

            revenueReportTable.Columns.Add("Date", "Date");
            revenueReportTable.Columns.Add("TotalRevenue", "Total Revenue");
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void home_homeBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void home_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminRoomAvailability = new AdminRoomAvailability();
            AdminRoomAvailability.ShowDialog();
        }

        private void home_reserveBtn_Click(object sender, EventArgs e){}

        private void home_offersBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageOffers = new ManageOffers();
            ManageOffers.ShowDialog();
        }

        private void home_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminProfile = new AdminProfile();
            AdminProfile.ShowDialog();
        }

        private void home_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageFeedback = new ManageFeedback();
            ManageFeedback.ShowDialog();
        }

        private void home_manageBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form RoomControlPanel = new RoomControlPanel();
            RoomControlPanel.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e){}
    }
}
