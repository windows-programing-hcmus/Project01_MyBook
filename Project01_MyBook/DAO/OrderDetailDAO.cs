using Project01_MyBook.DTO;
using Project01_MyBook.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DAO
{
    internal class OrderDetailDAO
    {
        internal static List<OrderDetailDTO> findOrderDetailByOrderID(string orderID)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM ORDERSDETAIL WHERE ordersID = '{orderID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<OrderDetailDTO> list = new List<OrderDetailDTO>();

            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                decimal odCurrentPrice = (decimal)reader["odCurrentPrice"];
                int odQuantity = (int)reader["odQuantity"];
                decimal odDiscountedPrice = (decimal)reader["odDiscountedPrice"];
                decimal odTempPrice = (decimal)reader["odTempPrice"];

                var odDetail = new OrderDetailDTO()
                {
                    ordersID = orderID,
                    bookID = bookID,
                    odCurrentPrice = odCurrentPrice,
                    odQuantity = odQuantity,
                    odDiscountedPrice = odDiscountedPrice,
                    odTempPrice = odTempPrice
                };
                list.Add(odDetail);
            }
            reader.Close();
            return list;
        }

        internal static bool InsertOrderDetail(OrderDetailDTO od)
        {
            var con = ConnectDB.openConnection();

            var currentPrice = od.odCurrentPrice.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
            var discountPrice = od.odDiscountedPrice.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
            var tempPrice = od.odTempPrice.ToString(CultureInfo.CreateSpecificCulture("en-GB"));

            var sql = "INSERT INTO ORDERSDETAIL(ordersID, bookID, odCurrentPrice, odDiscountedPrice, odQuantity, odTempPrice)" +
                      $" VALUES('{od.ordersID}', '{od.bookID}', {currentPrice}, {discountPrice}, {od.odQuantity}, {tempPrice})";
            
            var command = new SqlCommand(sql, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        internal static int findTotalBookByOrderID(string orderID)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT SUM(odQuantity) FROM ORDERSDETAIL WHERE ordersID = '{orderID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteScalar();

            Int32 result = (Int32)reader;

            return (int)result;
        }
    }
}