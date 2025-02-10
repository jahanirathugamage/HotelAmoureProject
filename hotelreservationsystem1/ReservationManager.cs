using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    internal class ReservationManager
    {
        // connect to the db
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";

        // get all the reservations
        public static List<Reservation> GetAllReservations()
        {
            List<Reservation> reservationList = new List<Reservation>();

            // Retrieve all guests and rooms first for efficient lookup
            List<Guest> guests = GuestManager.GetAllGuests();
            List<Room> rooms = RoomManager.GetAllRooms();

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string getAllReservationsQuery = "SELECT * FROM `reservations`";
                    MySqlCommand mysqlCommand = new MySqlCommand(getAllReservationsQuery, conn);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int reservationID = reader.GetInt32("reservationID");
                            int roomID = reader.GetInt32("roomID");
                            int guestID = reader.GetInt32("guestID");

                            DateTime checkInDate = reader.GetDateTime("checkInDate");
                            DateTime checkOutDate = reader.GetDateTime("checkOutDate");
                            string reservationStatus = reader.GetString("reservationStatus");
                            double totalPrice = reader.GetDouble("totalPrice");
                            DateTime createDate = reader.GetDateTime("createDate");
                            int duration = reader.GetInt32("duration");

                            // Find the corresponding guest and room
                            Guest guest = guests.FirstOrDefault(g => g.getGuestID() == guestID);
                            Room room = rooms.FirstOrDefault(r => r.GetRoomID() == roomID);

                            if (guest != null && room != null)
                            {
                                Reservation reservation = new Reservation(
                                    reservationID, room, guest, checkInDate, checkOutDate, reservationStatus, totalPrice, createDate, duration
                                );
                                reservationList.Add(reservation);
                            }
                            else
                            {
                                Console.WriteLine($"Guest or Room not found for reservation ID {reservationID}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return reservationList;
        }

        // get reservations that belong to a guest by their id
        public static List<Reservation> GetGuestReservations(int guestID)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM `reservations` WHERE guestID = @guestID";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, conn);
                    mySqlCommand.Parameters.AddWithValue("@guestID", guestID);

                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int reservationID = reader.GetInt32("reservationID");
                            int roomID = reader.GetInt32("roomID");
                            DateTime checkInDate = reader.GetDateTime("checkInDate");
                            DateTime checkOutDate = reader.GetDateTime("checkOutDate");
                            string reservationStatus = reader.GetString("reservationStatus");
                            double totalPrice = reader.GetDouble("totalPrice");
                            DateTime createDate = reader.GetDateTime("createDate");
                            int duration = reader.GetInt32("duration");

                            // get Room object directly
                            Room room = RoomManager.GetRoomByID(roomID);

                            // get Guest object directly
                            Guest guest = GuestManager.GetGuestByID(guestID);

                            if (room != null && guest != null)
                            {
                                // create and add the reservation object
                                Reservation reservation = new Reservation(
                                    reservationID, room, guest, checkInDate, checkOutDate, reservationStatus, totalPrice, createDate, duration
                                );

                                reservations.Add(reservation);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return reservations;
        }

        // get reservation by reservation id
        public static Reservation GetReservationByID(int reservationID)
        {
            Reservation reservation = null;

            // get all guests and rooms 
            List<Guest> guests = GuestManager.GetAllGuests();
            List<Room> rooms = RoomManager.GetAllRooms();

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string getReservationQuery = "SELECT * FROM `reservations` WHERE reservationID = @reservationID";
                    MySqlCommand mysqlCommand = new MySqlCommand(getReservationQuery, conn);
                    mysqlCommand.Parameters.AddWithValue("@reservationID", reservationID);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        if (reader.Read()) // 
                        {
                            int roomID = reader.GetInt32("roomID");
                            int guestID = reader.GetInt32("guestID");

                            DateTime checkInDate = reader.GetDateTime("checkInDate");
                            DateTime checkOutDate = reader.GetDateTime("checkOutDate");
                            string reservationStatus = reader.GetString("reservationStatus");
                            double totalPrice = reader.GetDouble("totalPrice");
                            DateTime createDate = reader.GetDateTime("createDate");
                            int duration = reader.GetInt32("duration");

                            Guest guest = guests.FirstOrDefault(g => g.getGuestID() == guestID);
                            Room room = rooms.FirstOrDefault(r => r.GetRoomID() == roomID);

                            if (guest != null && room != null)
                            {
                                reservation = new Reservation(
                                    reservationID, room, guest, checkInDate, checkOutDate, reservationStatus, totalPrice, createDate, duration
                                );
                            }
                            else
                            {
                                Console.WriteLine($"Guest or Room not found for reservation ID {reservationID}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return reservation;
        }


        // check if the reservation duration does not overlap with any other already existing reservations to handle double booking
        public static bool SearchRoomAvailabilityByID(int roomID, DateTime checkInDate, DateTime checkOutDate)
        {
            bool available = false;

            try
            {
                string getRoomAvailabilityQuery = @"
                    SELECT COUNT(*) 
                    FROM reservations 
                    WHERE roomID = @roomID 
                    AND (
                        checkInDate < @checkOutDate 
                        AND checkOutDate > @checkInDate
                    )";

                using (MySqlConnection conn = new MySqlConnection(connectionstring))
                {
                    conn.Open();
                    using (MySqlCommand mysqlCommand = new MySqlCommand(getRoomAvailabilityQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@roomID", roomID);
                        mysqlCommand.Parameters.AddWithValue("@checkInDate", checkInDate.Date);
                        mysqlCommand.Parameters.AddWithValue("@checkOutDate", checkOutDate.Date);

                        object result = mysqlCommand.ExecuteScalar();

                        if (result != null && Convert.ToInt32(result) == 0)
                        {
                            available = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return available;
        }

        // add reservation to db
        public static void AddReservation(Reservation reservation)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get attribute values
                    int roomID = reservation.Room.GetRoomID();
                    int guestID = reservation.Guest.getGuestID();
                    string checkInDate = reservation.getCheckInDate().ToString("yyyy-MM-dd"); // format for MySQL
                    string checkOutDate = reservation.getCheckOutDate().ToString("yyyy-MM-dd"); 
                    string reservationStatus = reservation.getReservationStatus();
                    double totalPrice = reservation.getTotalPrice();
                    string createDate = reservation.getCreateDate().ToString("yyyy-MM-dd"); 
                    int duration = reservation.getDuration();

                    string insertReservationQuery = "INSERT INTO reservations (roomID, guestID, checkInDate, checkOutDate, reservationStatus, totalPrice, createDate, duration) " +
                                                    "VALUES (@roomID, @guestID, @checkInDate, @checkOutDate, @reservationStatus, @totalPrice, @createDate, @duration)";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(insertReservationQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@roomID", roomID);
                        mysqlCommand.Parameters.AddWithValue("@guestID", guestID);
                        mysqlCommand.Parameters.AddWithValue("@checkInDate", checkInDate);
                        mysqlCommand.Parameters.AddWithValue("@checkOutDate", checkOutDate);
                        mysqlCommand.Parameters.AddWithValue("@reservationStatus", reservationStatus);
                        mysqlCommand.Parameters.AddWithValue("@totalPrice", totalPrice);
                        mysqlCommand.Parameters.AddWithValue("@createDate", createDate);
                        mysqlCommand.Parameters.AddWithValue("@duration", duration);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the reservation was created
                        if (success == 0) //  is false
                        {
                            MessageBox.Show("Error occurred, try again.");
                        }
                        else
                        {
                            MessageBox.Show("Reservation made successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


        // update reservation check-in and check-out based on reservation id
        public static void UpdateReservationCheckInAndCheckOut(int reservationID, DateTime checkInDate, DateTime checkOutDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateReservationQuery = "UPDATE reservations SET checkInDate = @checkInDate, checkOutDate = @checkOutDate WHERE reservationID = @reservationID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateReservationQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@reservationID", reservationID);
                        mysqlCommand.Parameters.AddWithValue("@checkInDate", checkInDate.ToString("yyyy-MM-dd"));
                        mysqlCommand.Parameters.AddWithValue("@checkOutDate", checkOutDate.ToString("yyyy-MM-dd"));

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the reservation was edited
                        if (success > 0)
                        {
                            MessageBox.Show("Reservation updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No reservation found with the given ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update the reservation status to cancelled
        public static void UpdateReservationStatus(int reservationID)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateReservationQuery = "UPDATE reservations SET reservationStatus = @reservationStatus WHERE reservationID = @reservationID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateReservationQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@reservationID", reservationID);
                        mysqlCommand.Parameters.AddWithValue("@reservationStatus", "CheckedOut");

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the reservation was edited
                        if (success < 0)
                        {
                            MessageBox.Show("No reservation found with the given ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // delete reservation by id
        public static bool DeleteReservation(int reservationID)
        {
            bool isDeleted = false;

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM `reservations` WHERE reservationID = @reservationID";
                    MySqlCommand mysqlCommand = new MySqlCommand(query, conn);
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
