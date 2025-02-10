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
    public partial class ReservationPanel : Form
    {
        // initialize attributes for reservation table
        private string guestName;

        // initialize attributes for update of reservation
        private int reservationID = 0;
        private DateTime checkInDate;
        private DateTime checkOutDate;
        private string username;
        private string password;
        private DateTime checkingOutDate;

        // initialize cancel reservation attributes
        private int cancelReservationID;
        private string cancelReservationUsername;
        private string cancelReservationPassword;

        // intiailize check out attributes
        private string checkoutUsername;
        private string checkoutPassword;

        private Feedback feedback;
        private int feedbackID = 0; 
        private int rating = 0;
        private string comment = "";
        private DateTime dateSubmitted = DateTime.Now;

        public ReservationPanel()
        {
            InitializeComponent();

            InitializeTable();

        }

        // initialise the table
        private void InitializeTable()
        {
            // clear existing columns and rows if any
            reservationsTable.Columns.Clear();
            reservationsTable.Rows.Clear();

            // add columns to the table
            reservationsTable.Columns.Add("reservationID", "Reservation");
            reservationsTable.Columns.Add("roomID", "Room No");
            reservationsTable.Columns.Add("checkInDate", "Check-In Date");
            reservationsTable.Columns.Add("checkOutDate", "Check-Out Date");
            reservationsTable.Columns.Add("reservationStatus", "Status");
        }

        private void clearUpdateReservationDataFields() 
        {
            updateReservationID.Text = "";
            updateCheckInDatePicker.Value = DateTime.Now;
            updateCheckoutDatePicker.Value = DateTime.Now;
            updateResUsername.Text = "";
            updateResPassword.Text = "";
        }

        private void clearCancelReservationDataFields() 
        {
            cancelResID.Text = "";
            cancelResUsername.Text = "";
            cancelResPassword.Text = "";
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void reservation_homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form GuestDashboard = new GuestDashboard();
            GuestDashboard.ShowDialog();
        }

        private void reservation_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AvailableRooms = new AvailableRooms();
            AvailableRooms.ShowDialog();
        }

        private void reservation_reserveBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void reservation_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form FeedbackForum = new FeedbackForum();
            FeedbackForum.ShowDialog();
        }

        private void reservation_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form GuestProfile = new GuestProfile();
            GuestProfile.ShowDialog();
        }

        private void viewReservationsName_TextChanged(object sender, EventArgs e)
        {
            guestName = viewReservationsName.Text;
        }

        // actions that happen when the enter button to get the reservations is clicked
        private void ReservationsEnterBtn_Click(object sender, EventArgs e)
        {
            reservationsTable.Rows.Clear();

            Reservation.displayUserReservations(guestName, reservationsTable);

            viewReservationsName.Text = "";
        }

        private void updateReservationID_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(updateReservationID.Text, out int reservationIDTemp))
            {
                reservationID = reservationIDTemp;
            }
            else
            {
                reservationID = 0;
            }
        }

        private void updateCheckInDatePicker_ValueChanged(object sender, EventArgs e)
        {
            checkInDate = updateCheckInDatePicker.Value;
        }

        private void updateCheckoutDatePicker_ValueChanged(object sender, EventArgs e)
        {
            checkOutDate = updateCheckoutDatePicker.Value; 
        }

        private void updateResUsername_TextChanged(object sender, EventArgs e)
        {
            username = updateResUsername.Text;
        }

        private void updateResPassword_TextChanged(object sender, EventArgs e)
        {
            password = updateResPassword.Text;
        }

        private void updateCheckinCheckbox_CheckedChanged(object sender, EventArgs e){}

        private void updateCheckoutCheckbox_CheckedChanged(object sender, EventArgs e){}

        private void updateRes_ConfirmBtn_Click(object sender, EventArgs e)
        {
            Reservation.updateReservation(reservationID, checkInDate, checkOutDate, username, password);
            clearUpdateReservationDataFields();
        }

        private void cancelResID_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cancelResID.Text, out int cancelReservationIDTemp))
            {
                cancelReservationID = cancelReservationIDTemp;
            }
            else
            {
                cancelReservationID = 0;
            }
        }

        private void cancelResUsername_TextChanged(object sender, EventArgs e)
        {
            cancelReservationUsername = cancelResUsername.Text;
        }

        private void cancelResPassword_TextChanged(object sender, EventArgs e)
        {
            cancelReservationPassword = cancelResPassword.Text;
        }

        private void cancelRes_ConfirmBtn_Click(object sender, EventArgs e)
        {
            Reservation.cancelReservation(cancelReservationID, cancelReservationUsername, cancelReservationPassword);
            clearCancelReservationDataFields();
        }

        private void checkOutUsername_TextChanged(object sender, EventArgs e)
        {
            checkoutUsername = checkOutUsername.Text;
        }

        private void checkOutPassword_TextChanged(object sender, EventArgs e)
        {
            checkoutPassword = checkOutPassword.Text;
        }

        private void checkOutBtn_Click(object sender, EventArgs e)
        {
            // get the date of when the user clicked the check out btn
            checkingOutDate = DateTime.Now;

            Reservation.checkoutProcess(checkoutUsername, checkoutPassword, checkingOutDate);

            checkOutUsername.Text = "";
            checkOutPassword.Text = "";
        }
    }
}
