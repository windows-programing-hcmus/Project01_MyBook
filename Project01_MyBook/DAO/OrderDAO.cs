using Project01_MyBook.BUS;
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
    internal class OrderDAO
    {
        internal static OrderDTO findOrderByID(string orderID)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM ORDERS WHERE ordersID = '{orderID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string cusPhoneNumber = "";
                if (!reader.IsDBNull(1))
                {
                    cusPhoneNumber = (string)reader["cusPhoneNumber"];
                }
                string accUsername = (string)reader["accUsername"];
                //string tobID = (string)reader["tobID"];
                decimal ordersPrice = (decimal)reader["ordersPrice"];
                var ordersTime = (DateTime)reader["ordersTime"];
                
                var order = new OrderDTO()
                {
                    ordersID = orderID,
                    cusPhoneNumber = cusPhoneNumber,
                    accUsername = accUsername,
                    ordersPrices = ordersPrice,
                    ordersTime = ordersTime
                };
                return order;
            }
            reader.Close();
            return null;
        }

        internal static int countOrderInDate(string date)
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT Count(*) FROM ORDERSDETAIL OD, ORDERS O" +
                " WHERE OD.ordersID = O.ordersID" +
                $" AND FORMAT(O.ordersTime, 'yyyy-MM-dd')  = '{date}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteScalar();
            reader = (reader == DBNull.Value) ? 0 : reader;

            Int32 result = (Int32)reader;

            return (int)result;
        }

        internal static double countBooksSoldByDate(string date)
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT SUM(ODT.odQuantity) FROM ORDERS OD, ORDERSDETAIL ODT " +
                $"WHERE OD.ordersID = ODT.ordersID AND FORMAT(OD.ordersTime, 'dd/MM/yyyy') = '{date}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteScalar();
            reader = (reader == DBNull.Value) ? 0 : reader;

            double result = (Int32)reader;

            return (double)result;
        }

        internal static double RevenueByMonth(string month)
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT SUM(OD.ordersPrice) FROM ORDERS OD " +
                $"WHERE FORMAT(OD.ordersTime, 'MM') = '{month}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteScalar();

            if (reader == DBNull.Value)
                return 0.0;

            decimal result = (decimal)reader;

            return (double)result;
        }

        internal static List<OrderDTO> findOrderByRangeDate(DateTime start, DateTime end)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM ORDERS WHERE ordersTime BETWEEN '{start}' AND '{end}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<OrderDTO> orders = new List<OrderDTO>();

            while (reader.Read())
            {
                string ordersID = (string)reader["ordersID"];
                string cusPhoneNumber = "";
                if (!reader.IsDBNull(1))
                {
                    cusPhoneNumber = (string)reader["cusPhoneNumber"];
                }
                string accUsername = (string)reader["accUsername"];
                string tobID = (string)reader["tobID"];
                int ordersPrice = (int)reader["ordersPrice"];
                var ordersTime = (DateTime)reader["ordersTime"];

                var order = new OrderDTO()
                {
                    ordersID = ordersID,
                    cusPhoneNumber = cusPhoneNumber,
                    accUsername = accUsername,
                    ordersPrices = ordersPrice,
                    ordersTime = ordersTime
                };
                orders.Add(order);
            }
            reader.Close();
            return orders;
        }

        internal static List<OrderDTO> findAllOrder()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT * FROM ORDERS ORDER BY ORDERS.ordersTime DESC";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<OrderDTO> orders = new List<OrderDTO>();

            while (reader.Read())
            {
                string ordersID = (string)reader["ordersID"];
                string cusPhoneNumber = "";
                if (!reader.IsDBNull(1))
                {
                    cusPhoneNumber = (string)reader["cusPhoneNumber"];
                }
                string accUsername = (string)reader["accUsername"];
                //string tobID = (string)reader["tobID"];

                //uncomment when data is completed
                decimal ordersPrice = (decimal)reader["ordersPrice"];

                //uncomment when data is completed
                var ordersTime = (DateTime)reader["ordersTime"];

                var order = new OrderDTO()
                {
                    ordersID = ordersID,
                    cusPhoneNumber = cusPhoneNumber,
                    accUsername = accUsername,
                    ordersPrices = ordersPrice,
                    ordersTime = ordersTime,
                    ordersTotal = OrderDetailBUS.findTotalBookByOrderID(ordersID)
                };
                orders.Add(order);
            }
            reader.Close();
            return orders;
        }

        internal static bool InsertOrder(OrderDTO order)
        {
            var con = ConnectDB.openConnection();

            var price = order.ordersPrices.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
            var time = order.ordersTime.ToString("yyyy-MM-dd hh:mm:ss");
            var cusPhone = order.cusPhoneNumber.Equals("null") ?  "null" : $"'{order.cusPhoneNumber}'";
            var sql = "INSERT INTO ORDERS(ordersID, cusPhoneNumber, accUsername, ordersPrice, ordersTime)" +
                $" VALUES('{order.ordersID}', {cusPhone}, '{order.accUsername}', {price}, '{time}')";
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

        internal static bool UpdateOrder(OrderDTO order)
        {
            var con = ConnectDB.openConnection();

            var sql = $"UPDATE ORDERS SET cusPhoneNumber = '{order.cusPhoneNumber}', accUsername = '{order.accUsername}', ordersPrices = {order.ordersPrices}, ordersTime = '{order.ordersTime}' "
                + $"WHERE ordersID = {order.ordersID}";

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
        internal static bool DeleteOrder(string orderID)
        {
            var con = ConnectDB.openConnection();

            var sql = "alter table ORDERS nocheck constraint all\n" +
                "alter table ORDERSDETAIL nocheck constraint all\n" +
                "alter table BOOK nocheck constraint all\n" +
                "alter table ACCOUNT nocheck constraint all\n" +
                "alter table CUSTOMER nocheck constraint all\n" +
                $"delete from ORDERS where ordersID = '{orderID}'\n" +
                $"delete from ORDERSDETAIL where ordersID = '{orderID}'\n" +
                "alter table ORDERS check constraint all\n" +
                "alter table ORDERSDETAIL check constraint all\n" +
                "alter table BOOK check constraint all\n" +
                "alter table ACCOUNT check constraint all\n" +
                "alter table CUSTOMER check constraint all\n";

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
    }
}
