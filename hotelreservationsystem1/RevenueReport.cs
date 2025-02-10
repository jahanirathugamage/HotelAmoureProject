using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    public class RevenueReport
    {
        public DateTime Date;
        public double TotalRevenue;

        // connect to the db
        private static string connectionstring = "server=localhost;database=hotel_amoure;user=root;password=;";

        // generate a revenue for all the occupancy related to a specific date
        public static List<RevenueReport> GetRevenueBasedOccupancyReport()
        {
            List<RevenueReport> revenueReportList = new List<RevenueReport>();
            string query = @"
                    SELECT 
                        DATE(checkInDate) AS Date,
                        SUM(totalPrice) AS TotalRevenue
                    FROM reservations
                    GROUP BY Date;
                ";

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RevenueReport report = new RevenueReport
                            {
                                Date = reader.GetDateTime("Date"),
                                TotalRevenue = reader.GetDouble("TotalRevenue")
                            };
                            revenueReportList.Add(report);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return revenueReportList;
        }

        // display revenue reports on the table
        public static void addRevenueReportToTable(RevenueReport item, DataGridView revenueReportTable)
        {
            try
            {
                // get attributes
                string Date = item.Date.ToString("yyyy-MM-dd");
                string TotalRevenue = $"LKR{item.TotalRevenue:N0}"; // format LKR000,000

                // add data to the row
                revenueReportTable.Rows.Add(Date, TotalRevenue);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void displayRevenuReport(DataGridView revenueReportTable) 
        {
            try
            {
                List<RevenueReport> revenueReportList = RevenueReport.GetRevenueBasedOccupancyReport();

                // display all the room entries
                foreach (RevenueReport item in revenueReportList)
                {
                    RevenueReport.addRevenueReportToTable(item, revenueReportTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
