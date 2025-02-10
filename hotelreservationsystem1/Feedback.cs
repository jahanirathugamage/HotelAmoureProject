using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace hotelreservationsystem1
{
    public class Feedback
    {
        private int feedbackID;
        private Reservation reservation;
        private Guest guest;
        private Room room;
        private int rating;
        private string comment;
        private DateTime dateSubmitted;

        public Feedback(int feedbackID, int rating, string comment, DateTime dateSubmitted, Reservation reservation = null, Guest guest = null, Room room = null) 
        {
            this.feedbackID = feedbackID;
            this.rating = rating;
            this.comment = comment;
            this.dateSubmitted = dateSubmitted;

            // enforcing that Feedback belongs to only one of room, reservation and guest
            if (room != null)
            {
                this.room = room;
                room.AddFeedback(this);
            }
            else if (guest != null)
            {
                this.guest = guest;
                guest.AddFeedback(this);
            }
            else if (reservation != null)
            {
                this.reservation = reservation;
                reservation.AddFeedback(this);
            }
            else
            {
                // make sure that every feedback has a guest, room and reservation
                throw new ArgumentException("Feedback must be associated with a Room, Guest, or Reservation.");
            }
        } 

        // getters and setters
        public int getFeedbackID(){ 
            return feedbackID; 
        } 
        public Reservation getReservation()
        { 
            return reservation; 
        } 

        public Guest getGuest()
        { 
            return guest; 
        } 

        public Room getRoom()
        { 
            return room; 
        } 

        public int getRating()
        { 
            return rating;
        } 

        public string getComment()
        { 
            return comment;
        } 

        public DateTime getDateSubmitted()
        { 
            return dateSubmitted; 
        } 

        public void setFeedbackID(int feedback) 
        { 
            this.feedbackID = feedback; 
        }

        public void setReservation(Reservation reservation) 
        { 
            this.reservation = reservation;
        }

        public void setGuest(Guest guest) 
        {
            this.guest = guest; 
        }

        public void setRoom(Room room) 
        { 
            this.room = room; 
        }

        public void setRating(int rating) 
        {
            this.rating = rating;
        }

        public void setComment(string comment) 
        { 
            this.comment = comment; 
        }

        public void setDateSubmitted(DateTime dateSubmitted) 
        { 
            this.dateSubmitted = dateSubmitted;
        }


        // display feedback on table
        public static void addFeedbackToTable(Feedback feedback, DataGridView FeedbackTable)
        {
            try
            {
                // get attributes
                string feedbackID = feedback.getFeedbackID().ToString();
                string comment = feedback.getComment();
                string rating = $"{feedback.getRating().ToString()}/10";
                string roomID = feedback.getRoom().GetRoomID().ToString();
                string dateSubmitted = feedback.getDateSubmitted().ToString("yyyy-MM-dd");

                // add data to the row
                FeedbackTable.Rows.Add(feedbackID, comment, rating, roomID, dateSubmitted);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void displayFeedback(DataGridView FeedbackTable) 
        {
            try
            {
                // get all the feedback
                List<Feedback> feedbackList = FeedbackManager.GetAllFeedback();

                // display all the feedback entries
                foreach (Feedback feedbackEntry in feedbackList) Feedback.addFeedbackToTable(feedbackEntry, FeedbackTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // create feedback
        public static void createFeedback(Reservation reservation, int rating, string comment, DateTime dateSubmittedInput, Form FeedbackForm) 
        {
            int feedbackID = 0; // initialise the feedbackID to 0 as it is not required by the db to be added manually

            try
            {
                // validate the rating and comment
                if ((rating < 0) || (rating > 10))
                {
                    MessageBox.Show("Please enter a valid rating (0-10).");
                    return;
                }
                if (string.IsNullOrEmpty(comment))
                {
                    MessageBox.Show("Please enter your review.");
                    return;
                }

                // get the guest and room of the reservation to add to the feedback object
                Guest guest = reservation.Guest;
                Room room = reservation.Room;

                if (guest == null || room == null)
                {
                    Console.WriteLine("Reservation details are not available.");
                    return;
                }

                // get the ids
                int reservationID = reservation.getReservationID();
                int guestID = guest.getGuestID();
                int roomID = room.GetRoomID();

                // convert dateSubmitted into string
                string dateSubmitted = dateSubmittedInput.ToString("yyyy-MM-dd");


                FeedbackManager.AddFeedback(feedbackID, reservationID, guestID, roomID, rating, comment, dateSubmitted);

                MessageBox.Show("Thank you for your feedback! Hope you enjoyed your stay.");
                FeedbackForm.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        // delete feedback
        public static void ManagerDeleteFeedback(int deleteFeedbackID, TextBox deleteFeedbackNo) 
        {
            try 
            {
                // validating if the user has entered a valid feedback id
                if (deleteFeedbackID == 0)
                {
                    MessageBox.Show("Please enter a valid feedback ID.");
                    return;
                }
                else
                {
                    FeedbackManager.DeleteFeedback(deleteFeedbackID);

                    // clear feedback ID data field
                    deleteFeedbackNo.Text = "";
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
