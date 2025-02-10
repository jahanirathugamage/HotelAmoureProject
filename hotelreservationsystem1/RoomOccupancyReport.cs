using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    public class RoomOccupancyReport
    {
        // attributes
        public string RoomType;
        public int TotalBookings;
        public double OccupancyRate;

        // connect to the db
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";

        // get the most popular room type from the reservations
        public static List<RoomOccupancyReport> GetRoomTypeOccupancyReport()
        {
            List<RoomOccupancyReport> reportList = new List<RoomOccupancyReport>();
            string getRoomTypeOccupancyRatesquery = @"
                    SELECT 
                        r.roomType,
                        COUNT(res.reservationID) AS TotalBookings,
                        (COUNT(res.reservationID) / (SELECT COUNT(*) FROM reservations)) * 100 AS OccupancyRate
                    FROM reservations res
                    JOIN rooms r ON res.roomID = r.roomID
                    GROUP BY r.roomType;
                ";

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    MySqlCommand mysqlCommand = new MySqlCommand(getRoomTypeOccupancyRatesquery, conn);
                    using (MySqlDataReader reader = mysqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoomOccupancyReport report = new RoomOccupancyReport
                            {
                                RoomType = reader.GetString("roomType"),
                                TotalBookings = reader.GetInt32("TotalBookings"), // all the reservations
                                OccupancyRate = reader.GetDouble("OccupancyRate") // 
                            };
                            reportList.Add(report);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return reportList;
        }

        // get the average duration of how long each reservation was
        public static double GetAverageStayDuration()
        {
            double averageStayDuration = 0;
            string query = @"
                    SELECT 
                        AVG(DATEDIFF(checkOutDate, checkInDate)) AS AvgStayDuration
                    FROM reservations;
                ";

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        averageStayDuration = Convert.ToDouble(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return averageStayDuration;
        }

        // display occupancy reports on the table
        public static void addOccupancyReportToTable(RoomOccupancyReport entry, DataGridView roomOccupancyReportTable)
        {

            // get attributes
            string roomType = entry.RoomType;
            string OccupancyRate = $"{entry.OccupancyRate.ToString()}%"; // format 00%

            // add data to the row
            roomOccupancyReportTable.Rows.Add(roomType, OccupancyRate);
        }

        public static void displayOccupancyReport(DataGridView roomOccupancyReportTable) 
        {
            // display the room occupancy report
            try
            {
                List<RoomOccupancyReport> roomOccupancy = RoomOccupancyReport.GetRoomTypeOccupancyReport();

                // display all the room entries
                foreach (RoomOccupancyReport entry in roomOccupancy)
                {
                    RoomOccupancyReport.addOccupancyReportToTable(entry, roomOccupancyReportTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void displayAverageStayReport(Label avgStayDurationResult) 
        {
            // display the average guest stay report
            try
            {
                double averageStay = RoomOccupancyReport.GetAverageStayDuration();
                avgStayDurationResult.Text = $"{Math.Round(averageStay, 0)} days";
                avgStayDurationResult.Visible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
