using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    internal class SpecialOfferManager
    {
        // connect to the db
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";

        // get all the active offers
        public static List<SpecialOffer> GetAllActiveSpecialOffers()
        {
            List<SpecialOffer> activeSpecialOffers = new List<SpecialOffer>();

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // select only the active offers
                    string selectAllActiveOffersQuery = "SELECT * FROM `specialoffers` WHERE offerStatus = 'Active'";

                    MySqlCommand mysqlCommand = new MySqlCommand(selectAllActiveOffersQuery, conn);

                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int offerID = reader.GetInt32("offerID");
                            string offerName = reader.GetString("offerName");
                            int discountPercentage = reader.GetInt32("discountPercentage");

                            DateTime startDate = reader.GetDateTime("startDate");
                            DateTime endDate = reader.GetDateTime("endDate");

                            // offer applicable room types
                            string applicableRoomTypesRaw = reader.IsDBNull(reader.GetOrdinal("applicableRoomTypes")) ? "" : reader.GetString("applicableRoomTypes");
                            List<string> applicableRoomTypes = new List<string>(applicableRoomTypesRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                            // offer status
                            string offerStatusTemp = reader.IsDBNull(reader.GetOrdinal("offerStatus")) ? "Deactivated" : reader.GetString("offerStatus");
                            bool offerStatus = offerStatusTemp.Equals("Active", StringComparison.OrdinalIgnoreCase);

                            // create special offer object
                            SpecialOffer specialOffer = new SpecialOffer(offerID, offerName, discountPercentage, startDate, endDate, applicableRoomTypes, offerStatus);

                            // add to list
                            activeSpecialOffers.Add(specialOffer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return activeSpecialOffers;
        }


        // create a special offer and add it to the db
        public static void AddNewSpecialOffer(SpecialOffer specialOffer)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // get attribute values
                    string offerName = specialOffer.getOfferName();
                    int discountPercentage = specialOffer.getDiscountPercentage();

                    // format the start and end dates
                    string startDate = specialOffer.getStartDate().ToString("yyyy-MM-dd");
                    string endDate = specialOffer.getEndDate().ToString("yyyy-MM-dd");

                    string applicableRoomTypes = string.Join(", ", specialOffer.ApplicableRoomTypes);
                    string offerStatus = specialOffer.getOfferStatus() ? "Active" : "Deactivated";

                    string insertSpecialOfferQuery = "INSERT INTO specialoffers (offerName, discountPercentage, startDate, endDate, applicableRoomTypes, offerStatus) VALUES (@offerName, @discountPercentage, @startDate, @endDate, @applicableRoomTypes, @offerStatus)";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(insertSpecialOfferQuery, conn)) 
                    {
                        mysqlCommand.Parameters.AddWithValue("@offerName", offerName);
                        mysqlCommand.Parameters.AddWithValue("@discountPercentage", discountPercentage);
                        mysqlCommand.Parameters.AddWithValue("@startDate", startDate);
                        mysqlCommand.Parameters.AddWithValue("@endDate", endDate);
                        mysqlCommand.Parameters.AddWithValue("@applicableRoomTypes", applicableRoomTypes);
                        mysqlCommand.Parameters.AddWithValue("@offerStatus", offerStatus);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the room was created
                        if (success == 0) //  is false
                        {
                            Console.WriteLine("Error occurred, try again.");
                        }
                        else
                        {
                            MessageBox.Show("Special Offer created successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        // update a special offer discount based on special offer id
        public static void UpdateOfferDiscount(int offerID, int discountPercentage)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateOfferDiscountQuery = "UPDATE specialoffers SET discountPercentage = @discountPercentage WHERE offerID = @offerID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateOfferDiscountQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@offerID", offerID);
                        mysqlCommand.Parameters.AddWithValue("@discountPercentage", discountPercentage);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the offer was edited
                        if (success == 0)
                        {
                            Console.WriteLine("No offer found with the given ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update a special offer start date based on special offer id
        public static void UpdateOfferStartDate(int offerID, DateTime offerStartDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    string startDate = offerStartDate.ToString("yyyy-MM-dd");

                    // update query
                    string updateOfferStartQuery = "UPDATE specialoffers SET startDate = @startDate WHERE offerID = @offerID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateOfferStartQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@offerID", offerID);
                        mysqlCommand.Parameters.AddWithValue("@startDate", startDate);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the offer was edited
                        if (success == 0)
                        {
                            Console.WriteLine("No offer found with the given ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update a special offer end date based on special offer id
        public static void UpdateOfferEndDate(int offerID, DateTime offerEndDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    string endDate = offerEndDate.ToString("yyyy-MM-dd");

                    // update query
                    string updateOfferEndQuery = "UPDATE specialoffers SET endDate = @endDate WHERE offerID = @offerID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateOfferEndQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@offerID", offerID);
                        mysqlCommand.Parameters.AddWithValue("@endDate", endDate);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the offer was edited
                        if (success == 0)
                        {
                            Console.WriteLine("No offer found with the given ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update a special offer status to active based on special offer id
        public static void ActivateOffer(int offerID, string offerStatus)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateOfferStatusToActiveQuery = "UPDATE specialoffers SET offerStatus = @offerStatus WHERE offerID = @offerID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateOfferStatusToActiveQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@offerID", offerID);
                        mysqlCommand.Parameters.AddWithValue("@offerStatus", offerStatus);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the offer was edited
                        if (success == 0)
                        {
                            Console.WriteLine("No offer found with the given ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // update a special offer status to deactivated based on special offer id
        public static void DeactivateOffer(int offerID, string offerStatus)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();

                    // update query
                    string updateOfferStatusToUnactiveQuery = "UPDATE specialoffers SET offerStatus = @offerStatus WHERE offerID = @offerID";

                    using (MySqlCommand mysqlCommand = new MySqlCommand(updateOfferStatusToUnactiveQuery, conn))
                    {
                        mysqlCommand.Parameters.AddWithValue("@offerID", offerID);
                        mysqlCommand.Parameters.AddWithValue("@offerStatus", offerStatus);

                        int success = mysqlCommand.ExecuteNonQuery();

                        // validate if the offer was edited
                        if (success == 0)
                        {
                            MessageBox.Show("No offer found with the given ID.");
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
