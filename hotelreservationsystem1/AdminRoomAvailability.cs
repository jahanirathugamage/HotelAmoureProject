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
    public partial class AdminRoomAvailability : Form
    {
        private string selectedRoomType = " "; // default selection of white space
        private string selectedBedType = " ";
        private int selectedBedrooms = 0;
        private string selectedViewType = " ";

        private int roomID;
        private DateTime startDate;
        private DateTime endDate;

        public AdminRoomAvailability()
        {
            InitializeComponent();

            InitializeTable();

            populateDropDownMenus();

            // display all the rooms
            Room.displayRooms(AllRoomsTable);
        }

        // initialise the table
        private void InitializeTable()
        {
            // clear existing columns and rows if any
            AllRoomsTable.Columns.Clear();
            AllRoomsTable.Rows.Clear();

            // add columns to the table
            AllRoomsTable.Columns.Add("roomID", "Room No");
            AllRoomsTable.Columns.Add("roomType", "Type");
            AllRoomsTable.Columns.Add("pricePerNight", "Price");
            AllRoomsTable.Columns.Add("amenities", "Amenities");
            AllRoomsTable.Columns.Add("bedType", "Bed Type");
            AllRoomsTable.Columns.Add("bedrooms", "Bedrooms");
            AllRoomsTable.Columns.Add("additionalServices", "Services");
            AllRoomsTable.Columns.Add("viewType", "View");
        }

        // populate the dropdown menu
        private void populateDropDownMenus()
        {
            roomTypeMenu.Items.Add("Standard");
            roomTypeMenu.Items.Add("Suite");
            roomTypeMenu.Items.Add("Deluxe");

            roomTypeMenu.SelectedIndex = 0;

            bedTypeMenu.Items.Add("Double");
            bedTypeMenu.Items.Add("Queen");
            bedTypeMenu.Items.Add("King");

            bedTypeMenu.SelectedIndex = 0;

            bedroomsMenu.Items.Add("1");
            bedroomsMenu.Items.Add("2");
            bedroomsMenu.Items.Add("3");

            bedroomsMenu.SelectedIndex = 0;

            viewTypeMenu.Items.Add("Ocean");
            viewTypeMenu.Items.Add("Garden");
            viewTypeMenu.Items.Add("City");

            viewTypeMenu.SelectedIndex = 0;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void home_homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminDashboard = new AdminDashboard();
            AdminDashboard.ShowDialog();
        }

        private void home_roomsBtn_Click(object sender, EventArgs e)
        {

        }

        private void home_reserveBtn_Click(object sender, EventArgs e){}

        private void home_offersBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageOffers = new ManageOffers();
            ManageOffers.ShowDialog();
        }

        private void home_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageFeedback = new ManageFeedback();
            ManageFeedback.ShowDialog();
        }

        private void home_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminProfile = new AdminProfile();
            AdminProfile.ShowDialog();
        }

        private void roomTypeMenu_SelectedIndexChanged(object sender, EventArgs e){}

        private void room_searchBtn_Click(object sender, EventArgs e){}

        private void resultField_Click(object sender, EventArgs e){}

        private void home_manageBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form RoomControlPanel = new RoomControlPanel();
            RoomControlPanel.ShowDialog();
        }

        private void AllRoomsTable_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        private void roomTypeMenu_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            selectedRoomType = roomTypeMenu.SelectedItem.ToString();
        }

        private void bedTypeMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedBedType = bedTypeMenu.SelectedItem.ToString();
        }

        private void bedroomsMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBedroomsTemp = bedroomsMenu.SelectedItem.ToString();
            selectedBedrooms = int.Parse(selectedBedroomsTemp);
        }

        private void viewTypeMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedViewType = viewTypeMenu.SelectedItem.ToString();
        }

        private void room_searchBtn_Click_1(object sender, EventArgs e)
        {
            Room.searchForAvailableRoom(selectedRoomType, selectedBedType, selectedBedrooms, selectedViewType, resultField);
        }

        private void roomIDInput_TextChanged(object sender, EventArgs e)
        {

            if (int.TryParse(roomIDInput.Text, out int roomIDTemp))
            {
                roomID = roomIDTemp;
            }
            else
            {
                roomID = 0;
            }
        }

        private void startDateInput_ValueChanged(object sender, EventArgs e)
        {
            startDate = startDateInput.Value;
        }

        private void endDateInput_ValueChanged(object sender, EventArgs e)
        {
            endDate = endDateInput.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // check if the room id input is available for a chosen period of time
            Room.CheckRoomOccupancy(roomID, startDate, endDate, roomAvailableLabel, roomIDInput);
        }
    }
}
