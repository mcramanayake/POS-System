using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System
{
    internal class DatabaseHelper
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["POSConnectionString"].ConnectionString;

        public void TestConnection()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed: " + ex.Message);
                }
            }
        }
    }
}
