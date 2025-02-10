using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace hotelreservationsystem1
{
    public class Reservation
    {
        // attributes
        private int reservationID;
        public Guest Guest; // composition - Reservation must have a Guest
        public Room Room;   // composition - Reservation must have a Room
        private DateTime checkInDate;
        private DateTime checkOutDate;
        private string reservationStatus; // stores the state of the reservation: Cancelled, Unconfirmed or Confirmed
        private double totalPrice;
        private DateTime createDate; // stores the date of reservation creation
        private int duration; // stores the length of duration of the reservation

        private List<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        // constructor
        public Reservation(int reservationID, Room room, Guest guest, DateTime checkInDate, DateTime checkOutDate,string reservationStatus, double totalPrice, DateTime createDate, int duration) 
        {
            if (guest == null) throw new ArgumentNullException(nameof(guest), "Guest cannot be null.");
            if (room == null) throw new ArgumentNullException(nameof(room), "Room cannot be null.");

            this.reservationID = reservationID;
            Guest = guest;
            Room = room;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.reservationStatus = reservationStatus;
            this.totalPrice = totalPrice;
            this.createDate = createDate;
            this.duration = duration;
        }

        // getters and setters
        public int getReservationID() 
        { 
            return reservationID;
        }

        public Guest getGuest() 
        {
            return Guest;
        }

        public Room getRoom() 
        {
            return Room;
        }

        public DateTime getCheckInDate() 
        { 
            return checkInDate;
        }

        public DateTime getCheckOutDate() 
        {
            return checkOutDate; 
        }

        public string getReservationStatus() 
        { 
            return reservationStatus;
        }

        public double getTotalPrice() 
        { 
            return totalPrice; 
        }

        public DateTime getCreateDate() 
        { 
            return createDate;
        }

        public int getDuration() 
        { 
            return duration; 
        }

        public void setReservationID(int reservationID) 
        { 
            this.reservationID = reservationID;
        }

        public void setCheckInDate(DateTime checkInDate) 
        { 
            this.checkInDate = checkInDate; 
        }

        public void setCheckOutDate(DateTime checkOutDate) 
        { 
            this.checkOutDate = checkOutDate;
        }

        public void setReservationStatus(string reservationStatus) 
        { 
            this.reservationStatus = reservationStatus; 
        }

        public void setTotalPrice(double totalPrice) 
        { 
            this.totalPrice = totalPrice;
        }

        public void setCreateDate(DateTime createDate) 
        {
            this.createDate = createDate;
        } 

        public void setDuration(int duration) 
        { 
            this.duration = duration; 
        }

        public void AddFeedback(Feedback feedback)
        {
            Feedbacks.Add(feedback);
        }

        public void RemoveReservation()
        {
            Feedbacks.Clear();
        }

        // reservation data into a table
        public static void addReservationToTable(Reservation reservation, DataGridView reservationsTable)
        {
            string reservationID = reservation.getReservationID().ToString();
            string roomID = reservation.Room.GetRoomID().ToString();
            string checkInDate = reservation.getCheckInDate().ToString("yyyy-MM-dd");
            string checkOutDate = reservation.getCheckOutDate().ToString("yyyy-MM-dd");
            string reservationStatus = reservation.getReservationStatus();

            // add data to row
            reservationsTable.Rows.Add(reservationID, roomID, checkInDate, checkOutDate, reservationStatus);
        }

        // display reservations
        public static void displayUserReservations(string guestName, DataGridView reservationsTable)
        {
            if (string.IsNullOrEmpty(guestName))
            {
                MessageBox.Show("Please enter a name.");
            }
            else
            {
                try
                {
                    // get guest id
                    Guest guest = GuestManager.GetGuestInfoByName(guestName);
                    int guestID = guest.getGuestID();

                    // get all the reservations that belong to the guest
                    List<Reservation> guestReservations = ReservationManager.GetGuestReservations(guestID);

                    foreach (Reservation reservation in guestReservations) addReservationToTable(reservation, reservationsTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // validate check-in date and validate check-out date
        public static bool validateCheckInAndCheckOutDates(DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                bool isValid = false;

                // check if the check in date is not the same day as the check out date
                if (checkInDate >= checkOutDate)
                {
                    isValid = false;
                }
                else
                {
                    // the check in date is not on the same day as the check out day and the check out day comes after the check in date
                    isValid = true;
                }

                return isValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false; // return false in case of an exception
            }
        }

        // calculate the subtotal
        public static double calculateSubtotal(int roomID, int duration)
        {
            double subtotal = 0;
            double pricePerNight = 0;

            // get the price of room per night
            List<Room> rooms = RoomManager.GetAllRooms();

            try
            {
                bool roomFound = false;

                foreach (Room room in rooms)
                {
                    // get current loop's room's id
                    int currentRoomID = room.GetRoomID();

                    // check if the current loop's room's id matches the room id of the reservation
                    if (currentRoomID == roomID)
                    {
                        roomFound = true;

                        // store the price of the room
                        pricePerNight = room.GetPricePerNight();
                        break;
                    }
                }
                // show error message if no room was found
                if (!roomFound)
                {
                    MessageBox.Show("Error, please enter a valid Room No.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

            // formula for calculating the subtotal 
            subtotal = (pricePerNight * duration);

            return subtotal;
        }


        // calulate the total discount
        public static double calculateDiscount(int roomID, int duration)
        {

            // get the subtotal
            double subtotal = calculateSubtotal(roomID, duration);

            string roomType = "";
            List<int> discounts = new List<int>();
            int totalDiscount = 0;

            // get the room type
            List<Room> rooms = RoomManager.GetAllRooms();

            try
            {
                bool roomFound = false;

                foreach (Room room in rooms)
                {
                    // get current loop's room's id
                    int currentRoomID = room.GetRoomID();

                    // check if the current loop's room's id matches the room id of the reservation
                    if (currentRoomID == roomID)
                    {
                        roomFound = true;

                        // get the room type of room to check if it is applicable for any discounts
                        roomType = room.GetRoomType();

                        // get the special offer discounts
                        List<SpecialOffer> specialOffers = SpecialOfferManager.GetAllActiveSpecialOffers();

                        foreach (SpecialOffer specialOffer in specialOffers)
                        {
                            // get current loop's special offer's list of applicable rooms
                            List<string> currentApplicableRooms = specialOffer.ApplicableRoomTypes;

                            // check if the room type of the reservation's room is there in the offer's applicable rooms list
                            foreach (string applicableRoom in currentApplicableRooms)
                            {
                                if (applicableRoom.Equals(roomType))
                                {
                                    // get the discount percentage of the offer
                                    discounts.Add(specialOffer.getDiscountPercentage());
                                }
                            }
                        }
                        break;
                    }
                }
                // show error message if no room was found
                if (!roomFound)
                {
                    MessageBox.Show("Error, please enter a valid Room No.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

            // get the total discount percentages from all the discounts
            foreach (int discount in discounts) { totalDiscount += discount; }

            // formula for calculating final discount that will be applied on the price of stay ---> ((subtotal * discounts)/100)
            double finalDiscount = ((subtotal * totalDiscount) / 100);

            return finalDiscount;
        }
        

        // calculate the duration of stay
        public static int calculateDurationOfReservation(DateTime checkInDate, DateTime checkOutDate) 
        {
            int duration = 0;

            // the difference in days between the check out day and the check in date to get the duration of stay
            duration = (checkOutDate.Date - checkInDate.Date).Days;

            // ensure duration is not negative
            if (duration < 0)
            {
                MessageBox.Show("The Check-Out date must be after Check-In date.");
                return 0;  // return 0 for invalid durations
            }

            return duration;
        }

        // schedule reservation
        public static void scheduleReservation(int roomID, bool checkInDateSelected, bool checkOutDateSelected, DateTime checkInDate, DateTime checkOutDate, Label subtotalPlaceHolder, Label discountPlaceHolder, Label totalPlaceHolder, Label subtotalLabel, Label discountLabel, Label totalPriceLable) 
        {
            try
            {
                // validate if the user has entered a correct room no
                if (roomID == 0) 
                {
                    MessageBox.Show("Please enter a valid Room No.");
                    return;
                }
                // validate if the user has selected a check in date and check out date
                else if (!checkInDateSelected || !checkOutDateSelected)
                {
                    MessageBox.Show("Please select both check-in and check-out dates.");
                    return;
                }
                else
                {
                    // validate the check in and check out dates
                    bool areDatesValid = validateCheckInAndCheckOutDates(checkInDate, checkOutDate);

                    // the dates are valid
                    if (areDatesValid)
                    {
                        // check if room is available
                        bool isRoomAvailable = ReservationManager.SearchRoomAvailabilityByID(roomID, checkInDate, checkOutDate);

                        // proceed if room is available
                        if (isRoomAvailable)
                        {
                            // calculate the duration of stay
                            int duration = calculateDurationOfReservation(checkInDate, checkOutDate);

                            // get the subtotal of the reservation
                            double subtotal = calculateSubtotal(roomID, duration);

                            // display the subtotal
                            subtotalLabel.Text = $"LKR{subtotal:N2}";
                            subtotalPlaceHolder.Visible = true;
                            subtotalLabel.Visible = true;

                            // get the discount of the reservation
                            double discount = calculateDiscount(roomID, duration);

                            // display the subtotal
                            discountLabel.Text = $"LKR{discount:N2}"; // format: LKR000,000.00
                            discountPlaceHolder.Visible = true;
                            discountLabel.Visible = true;

                            // get the total price of reservation ---> subtotal - discount
                            double totalPrice = (subtotal - discount);

                            // display the total price 
                            totalPriceLable.Text = $"LKR{totalPrice:N2}";
                            totalPlaceHolder.Visible = true;
                            totalPriceLable.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("The selected room is not available for the chosen dates.");
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Enter a Check-In date that is before the Check-Out date.");
                        return;
                    }   
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }
        }

        // create reservation object
        public static Reservation createReservation(int roomID, string username, string password, int reservationID, DateTime checkInDate, DateTime checkOutDate, string reservationStatus, double totalPrice, DateTime createDate, int duration)
        {
            try
            {
                // verify the user
                bool verifiedUser = AccountManager.ValidateUserLogin(username, password);

                if (!verifiedUser)
                {
                    MessageBox.Show("Invalid User credentials, try again.");
                    return null; // exit early if user validation fails
                }

                // get the guest
                Guest foundGuest = GuestManager.GetGuestByUsername(username);
                Room foundRoom = RoomManager.GetRoomByID(roomID);


                // if no guest was found, show an error message and exit
                if (foundGuest == null)
                {
                    MessageBox.Show("Incorrect username or password, try again.");
                    return null;
                }

                // if no room was found, show an error message and exit
                if (foundRoom == null)
                {
                    MessageBox.Show("Please enter a valid Room No.");
                    return null;
                }

                // create the reservation object
                return new Reservation(reservationID, foundRoom, foundGuest, checkInDate, checkOutDate, reservationStatus, totalPrice, createDate, duration);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null; // return null in case of exceptions
            }
        }

        // confirm reservation
        public static void confirmReservation(DateTime checkInDate, DateTime checkOutDate, Reservation reservation, int roomID, string username, string password, int reservationID) 
        {
            try
            {
                // get the duration, total price, create date and status of the reservation
                int duration = calculateDurationOfReservation(checkInDate, checkOutDate);

                // calculate the total
                double subtotal = calculateSubtotal(roomID, duration);
                double discount = calculateDiscount(roomID, duration);
                double totalPrice = (subtotal - discount);

                string reservationStatus = "Confirmed";

                DateTime today = DateTime.Today;
                DateTime createDate = today;

                // create the reservation object to pass onto the payment portal - temp object with uncompleted attribute data such as reservationStatus and createDate
                reservation = createReservation(roomID, username, password, reservationID, checkInDate, checkOutDate, reservationStatus, totalPrice, createDate, duration);

                if (reservation == null)
                {
                    MessageBox.Show("Error occurred, try again.");
                }
                else
                {
                    // pass the reference of the reservation object to the payment portal
                    PaymentPortal paymentPortal = new PaymentPortal(ref reservation);

                    // get the result of the reference from the payment portal
                    DialogResult result = paymentPortal.ShowDialog();

                    // check if the payment was successful or made
                    if (result == DialogResult.OK)
                    {
                        // add reservation to db
                        ReservationManager.AddReservation(reservation);
                    }
                    else
                    {
                        MessageBox.Show("Error, payment was not completed.");
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // validate the payment portal card details
        public static bool validPaymentDetails(string cardHolderName, string cardNumber, int cvcNumber, int expiryMonth, int expiryYear)
        {
            // get the current year
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            try
            {
                // iniatialize the validation
                bool validPaymentDetails = false;

                // check if the card holder name is entered
                if (string.IsNullOrEmpty(cardHolderName))
                {
                    MessageBox.Show("Please enter a name.");
                }
                else if (string.IsNullOrEmpty(cardNumber))
                {
                    MessageBox.Show("Please enter the card number.");
                }
                // check if the card number is 16 digits long
                else if (cardNumber.Length != 16)
                {
                    MessageBox.Show("Please enter a 16-digit long card number.");
                }  
                else if (expiryMonth == 0 || expiryYear == 0)
                {
                    MessageBox.Show("Please enter expiry date of card.");
                }
                // check if the expiry date is valid
                else if (expiryMonth < 1 || expiryMonth > 12 || expiryYear < currentYear || (expiryYear == currentYear && expiryMonth < currentMonth))
                {
                    MessageBox.Show("Please enter a valid expiry date");
                }
                else if (cvcNumber == 0)
                {
                    MessageBox.Show("Please enter the cvc.");
                }
                // check if the cvc is 3 digits long
                else if (cvcNumber.ToString().Length != 3)
                {
                    MessageBox.Show("Please enter a 3-digit long cvc.");
                }
                else
                {
                    validPaymentDetails = true;
                }

                return validPaymentDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false; // return false in case of an exception
            }
        }

        
        // update reservation 
        public static void updateReservation(int reservationID, DateTime? checkInDate, DateTime? checkOutDate, string username, string password)
        {
            try
            {
                // validate reservation existence
                if (reservationID == 0)
                {
                    MessageBox.Show("Please enter a valid reservation number.");
                    return;
                }

                // validate if both the check-in and check-out dates were chosen
                if (!checkInDate.HasValue || !checkOutDate.HasValue)
                {
                    MessageBox.Show("Please select Check-in and Check-out dates");
                    return;
                }

                // validate if the username was entered
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter username.");
                    return;
                }

                // validate if the password was entered
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter password");
                    return;
                }

                // validate the user credentials
                bool isUserValid = AccountManager.ValidateUserLogin(username, password);

                if (!isUserValid)
                {
                    MessageBox.Show("Invalid username or password, try again.");
                    return;
                }

                // validate if the check-in and check-out dates come in the right order
                bool areDatesValid = validateCheckInAndCheckOutDates(checkInDate.Value, checkOutDate.Value);

                if (!areDatesValid)
                {
                    MessageBox.Show("Please select a Check-In date that is before the Check-Out date.");
                    return;
                }

                ReservationManager.UpdateReservationCheckInAndCheckOut(reservationID, checkInDate.Value, checkOutDate.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // cancel reservation
        public static void cancelReservation(int cancelReservationID, string cancelReservationUsername, string cancelReservationPassword)
        {
            try
            {
                // validate reservation existence
                if (cancelReservationID == 0)
                {
                    MessageBox.Show("Please enter a valid reservation number.");
                    return;
                }

                // validate if user has entered username
                if (string.IsNullOrEmpty(cancelReservationUsername))
                {
                    MessageBox.Show("Please enter username.");
                    return;
                }

                // validate if user has entered password
                if (string.IsNullOrEmpty(cancelReservationPassword))
                {
                    MessageBox.Show("Please enter password.");
                    return;
                }

                // validate user credentials
                bool isUserValid = AccountManager.ValidateUserLogin(cancelReservationUsername, cancelReservationPassword);

                if (!isUserValid)
                {
                    MessageBox.Show("Invalid username or password, try again.");
                    return;
                }

                // check if reservation belongs to the guest and is a not checked out reservation
                Reservation cancelReservation = null;

                // get guest
                Guest guest = GuestManager.GetGuestByUsername(cancelReservationUsername);
                int guestID = guest.getGuestID();

                List<Reservation> guestReservations = ReservationManager.GetGuestReservations(guestID);

                foreach (Reservation reservation in guestReservations)
                {
                    int reservationID = reservation.getReservationID();
                    string reservationStatus = reservation.getReservationStatus();

                    if ((reservationID == cancelReservationID) && (reservationStatus == "Confirmed"))
                    {
                        cancelReservation = reservation;
                        break; 
                    }
                }

                if (cancelReservation == null)
                {
                    MessageBox.Show("No reservation of guest found with the given ID.");
                    return;
                }

                // call the reservation cancellation function
                ReservationManager.DeleteReservation(cancelReservationID);

                MessageBox.Show("Reservation successfully cancelled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // the checkout procedure
        public static void checkoutProcess(string checkoutUsername, string checkoutPassword, DateTime checkingOutDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(checkoutUsername))
                {
                    MessageBox.Show("Please enter a username.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(checkoutPassword))
                {
                    MessageBox.Show("Please enter a password.");
                    return;
                }

                // validate user credentials
                bool isUserValid = AccountManager.ValidateUserLogin(checkoutUsername, checkoutPassword);

                if (!isUserValid)
                {
                    MessageBox.Show("Invalid user credentials, try again.");
                    return;
                }

                // get the guest from username
                Guest currentGuest = GuestManager.GetGuestByUsername(checkoutUsername);

                if (currentGuest == null)
                {
                    MessageBox.Show("Guest not found.");
                    return;
                }

                // get reservations associated with the guest
                int currentGuestID = currentGuest.getGuestID();
                List<Reservation> guestReservationList = ReservationManager.GetGuestReservations(currentGuestID);

                // find the reservation matching the checkout date
                Reservation checkingOutReservation = guestReservationList
                    .FirstOrDefault(r => r.getReservationStatus() == "Confirmed" &&
                                         r.getCheckOutDate().Date == checkingOutDate.Date);

                if (checkingOutReservation == null)
                {
                    MessageBox.Show("No active reservations found for this guest.");
                    return;
                }

                // make sure reservation has an associated room
                Room reservationRoom = checkingOutReservation.Room;
                if (reservationRoom == null)
                {
                    Console.WriteLine("Error: Reservation has no associated room.");
                    return;
                }

                // make sure reservation has an associated guest
                checkingOutReservation.Guest = currentGuest;

                // update reservation status to CheckedOut
                int checkOutReservationID = checkingOutReservation.getReservationID();
                ReservationManager.UpdateReservationStatus(checkOutReservationID);

                // open FeedbackForm and pass the complete reservation
                FeedbackForm feedbackForm = new FeedbackForm(checkingOutReservation);
                feedbackForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
