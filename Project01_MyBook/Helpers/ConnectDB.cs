using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.Helpers
{
    internal class ConnectDB
    {
        
        public static SqlConnection openConnection()
        {
            var connectionString = "Server=.\\SQLEXPRESS;Database=BookStoreSchema;Trusted_Connection=True;";
            var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return connection;
        }

    }
}
