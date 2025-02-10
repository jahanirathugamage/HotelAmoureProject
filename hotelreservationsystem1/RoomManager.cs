using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    internal class RoomManager
    {

        // connect to the db
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";


        // get all the rooms
        public static List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    string selectAllRoomsQuery = "SELECT * FROM `rooms`";
                    MySqlCommand mysqlCommand = new MySqlCommand(selectAllRoomsQuery, conn);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // common fields for all rooms
                            int roomID = reader.GetInt32("roomID");
                            string roomType = reader.GetString("roomType");
                            double pricePerNight = reader.GetDouble("pricePerNight");

                            // amenities
                            string amenitiesRaw = reader.IsDBNull(reader.GetOrdinal("amenities")) ? "" : reader.GetString("amenities");
                            List<string> amenities = new List<string>(amenitiesRaw.Split(','));


                            // create the appropriate room based on room type
                            switch (roomType)
                            {
                                case "Standard":
                                    string bedType = reader.IsDBNull(reader.GetOrdinal("bedType")) ? "" : reader.GetString("bedType");
                                    rooms.Add(new StandardRoom(roomID, pricePerNight, amenities, bedType));
                                    break;

                                case "Suite":
                                    int bedrooms = reader.IsDBNull(reader.GetOrdinal("bedrooms")) ? 0 : reader.GetInt32("bedrooms");
                                    string servicesRaw = reader.IsDBNull(reader.GetOrdinal("additionalServices")) ? "" : reader.GetString("additionalServices");
                                    List<string> additionalServices = new List<string>(servicesRaw.Split(','));
                                    rooms.Add(new SuiteRoom(roomID, pricePerNight, amenities, bedrooms, additionalServices));
                                    break;

                                case "Deluxe":
                                    string viewType = reader.IsDBNull(reader.GetOrdinal("viewType")) ? "Standard View" : reader.GetString("viewType");
                                    rooms.Add(new DeluxeRoom(roomID, pricePerNight, amenities, viewType));
                                    break;

                                default:
                                    MessageBox.Show($"Unknown room type: {roomType} for Room ID: {roomID}");
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return rooms;
        }

        // get room from roomID
        public static Room GetRoomByID(int roomID)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // query to get room details based on roomID
                    string selectRoomQuery = "SELECT * FROM `rooms` WHERE `roomID` = @roomID";
                    MySqlCommand mysqlCommand = new MySqlCommand(selectRoomQuery, conn);
                    mysqlCommand.Parameters.AddWithValue("@roomID", roomID);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // common fields for all rooms
                            string roomType = reader.GetString("roomType");
                            double pricePerNight = reader.GetDouble("pricePerNight");

                            // amenities
                            string amenitiesRaw = reader.IsDBNull(reader.GetOrdinal("amenities")) ? "" : reader.GetString("amenities");
                            List<string> amenities = new List<string>(amenitiesRaw.Split(','));

                            // create the appropriate room based on room type
                            switch (roomType)
                            {
                                case "Standard":
                                    string bedType = reader.IsDBNull(reader.GetOrdinal("bedType")) ? "" : reader.GetString("bedType");
                                    return new StandardRoom(roomID, pricePerNight, amenities, bedType);

                                case "Suite":
                                    int bedrooms = reader.IsDBNull(reader.GetOrdinal("bedrooms")) ? 0 : reader.GetInt32("bedrooms");
                                    string servicesRaw = reader.IsDBNull(reader.GetOrdinal("additionalServices")) ? "" : reader.GetString("additionalServices");
                                    List<string> additionalServices = new List<string>(servicesRaw.Split(','));
                                    return new SuiteRoom(roomID, pricePerNight, amenities, bedrooms, additionalServices);

                                case "Deluxe":
                                    string viewType = reader.IsDBNull(reader.GetOrdinal("viewType")) ? "Standard View" : reader.GetString("viewType");
                                    return new DeluxeRoom(roomID, pricePerNight, amenities, viewType);

                                default:
                                    MessageBox.Show($"Unknown room type: {roomType} for Room ID: {roomID}");
                                    return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return null; // return null if room was not found
        }

        // add a standard room to the database
        public static void AddNewStandardRoom(StandardRoom standardRoom) 
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get attribute values
                    string roomType = standardRoom.GetRoomType();
                    double pricePerNight = standardRoom.GetPricePerNight();

                    string amenities = string.Join(", ", standardRoom.GetAmenities());
                    string bedType = standardRoom.GetBedType();

                    // select only standard rooms
                    string insertStandardRoomQuery = "INSERT INTO rooms (roomType, pricePerNight, amenities, bedType) VALUES (@roomType, @pricePerNight, @amenities, @bedType)";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(insertStandardRoomQuery, conn)) 
                    {
                        mysqlCommand.Parameters.AddWithValue("@roomType", roomType);
                        mysqlCommand.Parameters.AddWithValue("@pricePerNight", pricePerNight);
                        mysqlCommand.Parameters.AddWithValue("@amenities", amenities);
                        mysqlCommand.Parameters.AddWithValue("@bedType", bedType);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the room was created
                        if (success == 0) //  is false
                        {
                            Console.WriteLine("Error occurred, try again.");
                        }
                        else
                        {
                            MessageBox.Show("Standard Room created successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // add a suite room to the database
        public static void AddNewSuiteRoom(SuiteRoom suiteRoom)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get attribute values
                    string roomType = suiteRoom.GetRoomType();

                    // convert the boolean to string value

                    double pricePerNight = suiteRoom.GetPricePerNight();
                    string amenities = string.Join(", ", suiteRoom.GetAmenities());
                    int bedrooms = suiteRoom.GetBedrooms();
                    string additionalServices = string.Join(", ", suiteRoom.GetAdditionalServices());

                    // select only suite rooms
                    string insertSuiteRoomQuery = "INSERT INTO rooms (roomType, pricePerNight, amenities, bedrooms, additionalServices) VALUES (@roomType, @pricePerNight, @amenities, @bedrooms, @additionalServices)";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(insertSuiteRoomQuery, conn)) 
                    {
                        mysqlCommand.Parameters.AddWithValue("@roomType", roomType);
                        mysqlCommand.Parameters.AddWithValue("@pricePerNight", pricePerNight);
                        mysqlCommand.Parameters.AddWithValue("@amenities", amenities);
                        mysqlCommand.Parameters.AddWithValue("@bedrooms", bedrooms);
                        mysqlCommand.Parameters.AddWithValue("@additionalServices", additionalServices);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the room was created
                        if (success == 0) //  is false
                        {
                            Console.WriteLine("Error occurred, try again.");
                        }
                        else
                        {
                            MessageBox.Show("Suite Room created successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // add a deluxe room to the database
        public static void AddNewDeluxeRoom(DeluxeRoom deluxeRoom)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get attribute values
                    string roomType = deluxeRoom.GetRoomType();

                    // convert the boolean to string value

                    double pricePerNight = deluxeRoom.GetPricePerNight();
                    string amenities = string.Join(", ", deluxeRoom.GetAmenities());
                    string viewType = deluxeRoom.GetViewType();

                    // select only deluxe rooms
                    string insertDeluxeRoomQuery = "INSERT INTO rooms (roomType, pricePerNight, amenities, viewType) VALUES (@roomType, @pricePerNight, @amenities, @viewType)";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(insertDeluxeRoomQuery, conn)) 
                    {
                        mysqlCommand.Parameters.AddWithValue("@roomType", roomType);
                        mysqlCommand.Parameters.AddWithValue("@pricePerNight", pricePerNight);
                        mysqlCommand.Parameters.AddWithValue("@amenities", amenities);
                        mysqlCommand.Parameters.AddWithValue("@viewType", viewType);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the room was created
                        if (success == 0) //  is false
                        {
                            Console.WriteLine("Error occurred, try again.");
                        }
                        else
                        {
                            MessageBox.Show("Suite Room created successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update room type from room id
        public static void UpdateRoomType(int roomID, string roomType)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateRoomTypeQuery = "UPDATE rooms SET roomType = @roomType WHERE roomID = @roomID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateRoomTypeQuery, conn)) 
                    {
                        mysqlCommand.Parameters.AddWithValue("@roomID", roomID);
                        mysqlCommand.Parameters.AddWithValue("@roomType", roomType);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the room was edited
                        if (success == 0)
                        {
                            Console.WriteLine("No room found with the given ID.");
                        }
                        else
                        {
                            MessageBox.Show("Room updated successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update room price from room id
        public static void UpdateRoomPrice(int roomID, double pricePerNight)
        {
            // validate the user input price
            if (pricePerNight <= 0.00)
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }
            else 
            {
                using (MySqlConnection conn = new MySqlConnection(connectionstring))
                {
                    try
                    {
                        conn.Open();

                        // update query
                        string updateRoomPriceQuery = "UPDATE rooms SET pricePerNight = @pricePerNight WHERE roomID = @roomID";

                        using (MySqlCommand mysqlCommand = new MySqlCommand(updateRoomPriceQuery, conn))
                        {
                            mysqlCommand.Parameters.AddWithValue("@roomID", roomID);
                            mysqlCommand.Parameters.AddWithValue("@pricePerNight", pricePerNight);

                            int success = mysqlCommand.ExecuteNonQuery();

                            // validate if the room was edited
                            if (success == 0)
                            {
                                Console.WriteLine("No room found with the given ID.");
                            }
                            else 
                            {
                                MessageBox.Show("Room updated successfully!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            } 
        }

        // update room amenities from room id
        public static void UpdateRoomAmenities(int roomID, string amenities)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateRoomAmenitiesQuery = "UPDATE rooms SET amenities = @amenities WHERE roomID = @roomID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateRoomAmenitiesQuery, conn)) 
                    {
                        mysqlCommand.Parameters.AddWithValue("@roomID", roomID);
                        mysqlCommand.Parameters.AddWithValue("@amenities", amenities);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the room was edited
                        if (success == 0)
                        {
                            Console.WriteLine("No room found with the given ID.");
                        }
                        else
                        {
                            MessageBox.Show("Room updated successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // delete a room in the database
        public static void DeleteRoom(int roomID)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // delete query
                    string deleteRoomQuery = "DELETE FROM rooms WHERE roomID = @roomID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(deleteRoomQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@roomID", roomID);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the room was deleted
                        if (success == 0)
                        {
                            Console.WriteLine("No room found with the given ID.");
                        }
                        else
                        {
                            MessageBox.Show("Room deleted successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

    }
}
