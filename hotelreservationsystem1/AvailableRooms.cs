using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace hotelreservationsystem1
{
    public partial class AvailableRooms : Form
    {
        
        // initalize the attributes for filtering the room availability
        private string selectedRoomType = " "; // default selection of white space
        private string selectedBedType = " ";
        private int selectedBedrooms = 0;
        private string selectedViewType = " ";

        // initialize the attributes for reservation
        private Reservation reservation;
        private int reservationID = 0; // default reservation status - irrelevant as it is not going to be added to db
        private int roomID;
        private bool checkInDateSelected = false;
        private bool checkOutDateSelected = false;
        private DateTime checkInDate;
        private DateTime checkOutDate;
        private string reservationStatus = "Unconfirmed"; // default reservation status until its paid for

        // initialize the user verification information
        private string username;
        private string password;

        // get all the rooms
        List<Room> roomList = RoomManager.GetAllRooms();

        public AvailableRooms()
        {
            InitializeComponent();

            InitializeTable();

            populatDropDownMenus();

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
        private void populatDropDownMenus()
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

        // clear reservation data fields
        private void clearReservationDataFields() 
        {
            reserveRoomNo.Text = "";
            reserveCheckInDatePicker.Value = DateTime.Now;
            reserveCheckoutDatePicker.Value = DateTime.Now;
            usernameInput.Text = "";
            passwordInput.Text = "";
            subtotalLabel.Visible = false;
            subtotalPlaceHolder.Visible = false;
            discountLabel.Visible = false;
            discountPlaceHolder.Visible = false;
            totalPlaceHolder.Visible = false;
            totalPriceLable.Visible = false;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rooms_homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form GuestDashboard = new GuestDashboard();
            GuestDashboard.ShowDialog();
        }

        private void rooms_roomsBtn_Click(object sender, EventArgs e){}

        private void rooms_reserveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ReservationPanel = new ReservationPanel();
            ReservationPanel.ShowDialog();
        }

        private void rooms_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form FeedbackForum = new FeedbackForum();
            FeedbackForum.ShowDialog();
        }

        private void rooms_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form GuestProfile = new GuestProfile();
            GuestProfile.ShowDialog();
        }

        private void roomTypeMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedRoomType = roomTypeMenu.SelectedItem.ToString();
        }

        private void room_searchBtn_Click(object sender, EventArgs e)
        {
            Room.searchForAvailableRoom(selectedRoomType, selectedBedType, selectedBedrooms, selectedViewType, resultField);
        }

        private void resultField_Click(object sender, EventArgs e){}

        private void panel6_Paint(object sender, PaintEventArgs e){}

        private void label2_Click(object sender, EventArgs e){}

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

        private void reserveRoomNo_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(reserveRoomNo.Text, out int roomIDTemp))
            {
                roomID = roomIDTemp;
            }
            else
            {
                roomID = 0;
            }
        }

        private void reserveCheckInDatePicker_ValueChanged(object sender, EventArgs e)
        {
            checkInDateSelected = true;
            checkInDate = reserveCheckInDatePicker.Value.Date;
        }

        private void reserveCheckoutDatePicker_ValueChanged(object sender, EventArgs e)
        {
            checkOutDateSelected = true;
            checkOutDate = reserveCheckoutDatePicker.Value.Date;
        }

        // actions that happen when the user clicks the schedule reservation button
        private void scheduleRes_submitBtn_Click(object sender, EventArgs e)
        {
            Reservation.scheduleReservation(roomID, checkInDateSelected, checkOutDateSelected, checkInDate, checkOutDate, subtotalPlaceHolder, discountPlaceHolder, totalPlaceHolder, subtotalLabel, discountLabel, totalPriceLable);
        }

        private void reservationConfirmBtn_Click(object sender, EventArgs e){}


        private void reserveCancelBtn_Click(object sender, EventArgs e)
        {
            // clear reservation data
            clearReservationDataFields();
        }

        private void usernameInput_TextChanged(object sender, EventArgs e)
        {
            username = usernameInput.Text;
        }

        private void passwordInput_TextChanged(object sender, EventArgs e)
        {
            password = passwordInput.Text;
        }

        private void label3_Click(object sender, EventArgs e){}

        private void resConfirmBtn_Click(object sender, EventArgs e)
        {
            Reservation.confirmReservation(checkInDate, checkOutDate, reservation, roomID, username, password, reservationID);

            clearReservationDataFields();
        }
    }
}
