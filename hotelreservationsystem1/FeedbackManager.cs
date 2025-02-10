using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace hotelreservationsystem1
{
    internal class FeedbackManager
    {
        // connect to the db
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";

        // get all the feedback
        public static List<Feedback> GetAllFeedback()
        {
            List<Feedback> feedbackList = new List<Feedback>();

            // Retrieve all guests, rooms, and reservations first for efficient lookup
            List<Guest> guests = GuestManager.GetAllGuests();
            List<Room> rooms = RoomManager.GetAllRooms();
            List<Reservation> reservations = ReservationManager.GetAllReservations();

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM `feedback`";

                    MySqlCommand mysqlCommand = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int feedbackID = reader.GetInt32("feedbackID");
                            int reservationID = reader.GetInt32("reservationID");
                            int guestID = reader.GetInt32("guestID");
                            int roomID = reader.GetInt32("roomID");
                            int rating = reader.GetInt32("rating");
                            string comment = reader.GetString("comment");
                            DateTime dateSubmitted = reader.GetDateTime("dateSubmitted");

                            // get the corresponding reservation, guest, and room
                            Reservation reservation = null;
                            Guest guest = null;
                            Room room = null;

                            foreach (Reservation reservationItem in reservations)
                            {
                                if (reservationItem.getReservationID() == reservationID)
                                {
                                    reservation = reservationItem;
                                    break;
                                }
                            }

                            foreach (Guest guestItem in guests)
                            {
                                if (guestItem.getGuestID() == guestID)
                                {
                                    guest = guestItem;
                                    break;
                                }
                            }

                            foreach (Room roomItem in rooms)
                            {
                                if (roomItem.GetRoomID() == roomID)
                                {
                                    room = roomItem;
                                    break;
                                }
                            }

                            if (reservation != null && guest != null && room != null)
                            {
                                Feedback feedback = new Feedback(feedbackID, rating, comment, dateSubmitted, reservation, guest, room);
                                feedbackList.Add(feedback);
                            }
                            else
                            {
                                Console.WriteLine("Could not get the reservation, guest and room properly.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return feedbackList;
        }



        // add feedback to the db
        public static void AddFeedback(int feedbackID, int reservationID, int guestID, int roomID, int rating, string comment, string dateSubmitted)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    string insertFeedbackQuery = "INSERT INTO feedback (reservationID, guestID, roomID, rating, comment, dateSubmitted) " +
                                                 "VALUES (@reservationID, @guestID, @roomID, @rating, @comment, @dateSubmitted)";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(insertFeedbackQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@reservationID", reservationID);
                        mysqlCommand.Parameters.AddWithValue("@guestID", guestID);
                        mysqlCommand.Parameters.AddWithValue("@roomID", roomID);
                        mysqlCommand.Parameters.AddWithValue("@rating", rating);
                        mysqlCommand.Parameters.AddWithValue("@comment", comment);
                        mysqlCommand.Parameters.AddWithValue("@dateSubmitted", dateSubmitted);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the feedback was created
                        if (success == 0)
                        {
                            MessageBox.Show("Error occurred, try again.");
                        }
                        else
                        {
                            Console.WriteLine("Feedback added successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // delete feedback when user/reservation/room is deleted
        public static void DeleteFeedback(int feedbackID)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // delete query
                    string deleteFeedbackQuery = "DELETE FROM feedback WHERE feedbackID = @feedbackID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(deleteFeedbackQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@feedbackID", feedbackID);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the feedback was deleted
                        if (success == 0)
                        {
                            MessageBox.Show("No feedback found with the given ID.");
                        }
                        else
                        {
                            MessageBox.Show("Feedback deleted successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // delete feedback using reservation id
        public static bool DeleteFeedbackByReservationID(int reservationID)
        {
            bool isDeleted = false;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string deleteFeedbackquery = "DELETE FROM `feedback` WHERE reservationID = @reservationID";
                    MySqlCommand mysqlCommand = new MySqlCommand(deleteFeedbackquery, conn);
                    mysqlCommand.Parameters.AddWithValue("@reservationID", reservationID);

                    int rowsAffected = mysqlCommand.ExecuteNonQuery();
                    isDeleted = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return isDeleted;
        }

    }
}
