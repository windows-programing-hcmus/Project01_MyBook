using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project01_MyBook.Helpers;
using System.Data.SqlClient;
using System.Data.Common;

namespace Project01_MyBook.DAO
{
    internal class Statistics_DAO 
    {
        internal static string getTotalRevenueUntilDate(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd hh:mm:ss");
            var con = ConnectDB.openConnection();
            var sql = "select convert(varchar,cast(SUM(do.odQuantity * do.odTempPrice) as money), 1) as Revenue from ORDERSDETAIL do join ORDERS o on do.ordersID = o.ordersID  where ordersTime <=@SelectedDate;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            string result = "0";
            if (reader.Read())
            {
                if (reader["Revenue"].GetType() != typeof(DBNull))
                    result = (string)reader["Revenue"];
            }
            reader.Close();
            return result.ToString();
        }

        internal static string getTotalProfitUntilDate(DateTime src)
        {
            var con = ConnectDB.openConnection();
            string sqlFormattedDate = src.ToString("yyyy-MM-dd hh:mm:ss");

            var sql = "select convert(varchar,cast(SUM(do.odQuantity *(do.odTempPrice - b.bookPrice)) as money), 1) \r\nas Profit from ORDERSDETAIL do join BOOK b on do.bookID = b.bookID join Orders o on do.ordersID = o.ordersID where ordersTime <= <= @SelectedDate;";
            
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            string result = "0";
            if (reader.Read())
            {
                if (reader["Profit"].GetType() != typeof(DBNull))
                    result = (string)reader["Profit"];
            }
            reader.Close();
            return result.ToString();
        }

        internal static int getTotalOrdersUntilDate(DateTime src)
        {
            var con = ConnectDB.openConnection();
            string sqlFormattedDate = src.ToString("yyyy-MM-dd hh:mm:ss");

            var sql = "select COUNT(ordersID) as Orders from Orders where ordersTime <= @SelectedDate;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            int result = 0;
            if (reader.Read())
            {
                result = (int)reader["Orders"];
            }
            reader.Close();
            return result;
        }

        internal static List<Tuple<string, decimal>> getDailyRevenue(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd hh:mm:ss");
            var con = ConnectDB.openConnection();
            //var sql = "select convert(varchar, o.OrderDate) as OrderDate, convert(varchar,cast(SUM(do.Quantity * p.SoldPrice) as money), 1) as Revenue from DetailOrder do join Phone p on do.PhoneID = p.ID join Orders o on do.OrderID = o.ID where OrderDate < @SelectedDate group by o.OrderDate order by o.OrderDate asc;";
            var sql = "select convert(varchar, o.ordersTime) as OrderDate, cast(SUM(do.odQuantity * do.odTempPrice) AS decimal(13,4)) as Revenue from ORDERSDETAIL do join BOOK b on do.bookID = b.bookID join Orders o on do.ordersID = o.ordersID where ordersTime <= @SelectedDate group by o.ordersTime order by o.ordersTime asc;";
            
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["OrderDate"], (decimal)reader["Revenue"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, decimal>> getWeeklyRevenue(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");
            var con = ConnectDB.openConnection();
            var sql = "SELECT convert(varchar, DATEPART(iso_week, o.ordersTime)) AS Week, cast(SUM(do.odQuantity * do.odTempPrice) as decimal(13,4)) as Revenue FROM BOOK b join ORDERSDETAIL do on b.bookID = do.bookID join Orders o on o.ordersID = do.OrdersID WHERE DATEPART(year, o.ordersTime) = @SelectedYear GROUP BY DATEPART(iso_week, o.ordersTime) ORDER BY DATEPART(iso_week, o.ordersTime); ";
            
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["Week"], (decimal)reader["Revenue"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, decimal>> getMonthlyRevenue(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");
            var con = ConnectDB.openConnection();
            var sql = "WITH Months as (select month(GETDATE()) as Monthnumber, datename(month, GETDATE()) as NameOfMonth, 1 as number union all select month(dateadd(month, number, (GETDATE()))) Monthnumber, datename(month, dateadd(month, number, (GETDATE()))) as NameOfMonth, number + 1  from Months  where number < 12) select NameOfMonth, Revenue from Months left join (select datepart(month, o.ordersTime) as OrderMonth, cast(SUM(do.odQuantity * do.odTempPrice) AS decimal(13, 4)) as Revenue from ORDERSDETAIL do join book b on do.bookID = b.bookID join Orders o on do.ordersID = o.ordersID where datepart(year, o.ordersTime) = @SelectedYear group by datepart(month, o.ordersTime)) MonthlyRevenue on Months.Monthnumber = MonthlyRevenue.OrderMonth order by Monthnumber;";
           
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                string monthName = (string)reader["NameOfMonth"];
                decimal monthValue = 0;

                if (reader["Revenue"].GetType() != typeof(DBNull))
                {
                    monthValue = (decimal)reader["Revenue"];
                }

                resultList.Add(Tuple.Create(monthName, (decimal)monthValue));
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, decimal>> getYearlyRevenue()
        {
            var con = ConnectDB.openConnection();
            var sql = "select convert(varchar, datepart(year, o.ordersTime)) as OrderYear, cast(SUM(do.odQuantity * odTempPrice) AS decimal(13,4)) as Revenue from ORDERSDETAIL do join BOOK b on do.bookID = b.bookID join Orders o on do.OrdersID = o.ordersID group by datepart(year, o.ordersTime) order by datepart(year, o.ordersTime) asc;";

            var command = new SqlCommand(sql, con);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["OrderYear"], (decimal)reader["Revenue"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, decimal>> getDailyProfit(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd hh:mm:ss");
            var con = ConnectDB.openConnection();
            var sql = " select convert(varchar, o.ordersTime) as OrderDate, cast(SUM(do.odQuantity * (do.odTempPrice - b.bookPrice)) AS decimal(13,4)) as Profit from ORDERSDETAIL do join BOOK b on do.bookID = b.bookID join Orders o on do.OrdersID = o.ordersID where ordersTime =@SelectedDate  group by o.ordersTime order by o.ordersTime asc;";
           
            
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["OrderDate"], (decimal)reader["Profit"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, decimal>> getWeeklyProfit(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");
            var con = ConnectDB.openConnection();
            var sql = "SELECT convert(varchar, DATEPART(iso_week, o.ordersTime)) AS Week, cast(SUM(do.odQuantity * (do.odTempPrice - b.bookPrice)) as decimal(13,4)) as Profit FROM BOOK b join ORDERSDETAIL do on b.bookID = do.bookID join Orders o on o.ordersID = do.ordersID WHERE DATEPART(year, o.ordersTime) = @SelectedYear GROUP BY DATEPART(iso_week, o.ordersTime) ORDER BY DATEPART(iso_week, o.ordersTime);";
            
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["Week"], (decimal)reader["Profit"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, decimal>> getMonthlyProfit(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy");
            var con = ConnectDB.openConnection();
            var sql = "WITH Months as (select month(GETDATE()) as Monthnumber, datename(month, GETDATE()) as NameOfMonth, 1 as number union all select month(dateadd(month, number, (GETDATE()))) Monthnumber, datename(month, dateadd(month, number, (GETDATE()))) as NameOfMonth, number + 1  from Months  where number < 12) select NameOfMonth, Profit from Months left join (select datepart(month, o.ordersTime) as OrderMonth, cast(SUM(do.odQuantity * (do.odTempPrice - p.bookPrice)) AS decimal(13, 4)) as Profit from ORDERSDETAIL do join BOOK p on do.bookID = p.bookID join Orders o on do.OrdersID = o.ordersID where datepart(year, o.ordersTime) = @SelectedYear group by datepart(month, o.ordersTime)) MonthlyProfit on Months.Monthnumber = MonthlyProfit.OrderMonth order by Monthnumber;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedYear";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                string monthName = (string)reader["NameOfMonth"];
                decimal monthValue = 0;

                if (reader["Profit"].GetType() != typeof(DBNull))
                {
                    monthValue = (decimal)reader["Profit"];
                }

                resultList.Add(Tuple.Create(monthName, (decimal)monthValue));
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, decimal>> getYearlyProfit()
        {
            var con = ConnectDB.openConnection();
            var sql = "select convert(varchar, datepart(year, o.ordersTime)) as OrderYear, cast(SUM(do.odQuantity * (do.odTempPrice - p.bookPrice)) AS decimal(13,4)) as Profit from ORDERSDETAIL do join BOOK p on do.bookID = p.bookID join Orders o on do.OrdersID = o.ordersID group by datepart(year, o.ordersTime) order by datepart(year, o.ordersTime) asc;";

            var command = new SqlCommand(sql, con);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, decimal>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["OrderYear"], (decimal)reader["Profit"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, int>> getDailyQuantityOfSpecificProduct(string srcPhoneID, string srcCategoryID, DateTime srcDate)
        {
            var con = ConnectDB.openConnection();
            var sql = "select convert(varchar, o.ordersTime) as OrderDate, SUM(do.odQuantity) as Quantity from BOOK p left join ORDERSDETAIL do on p.bookID = do.bookID join Orders o on do.ordersID = o.ordersID where p.bookID = @bookID  and p.tobID = @tobID and o.ordersTime <=@SelectedDate  group by o.ordersTime order by o.ordersTime asc;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@bookID";
            sqlParameter.Value = srcPhoneID;

            var sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@tobID";
            sqlParameter1.Value = srcCategoryID;

            string sqlFormattedDate = srcDate.ToString("yyyy-MM-dd hh:mm:ss");
            var sqlParameter2 = new SqlParameter();
            sqlParameter2.ParameterName = "@SelectedDate";
            sqlParameter2.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);

            command.Parameters.Add(sqlParameter);
            command.Parameters.Add(sqlParameter1);
            command.Parameters.Add(sqlParameter2);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                var tuple = Tuple.Create((string)reader["OrderDate"], (int)reader["Quantity"]);
                resultList.Add(tuple);
            }
            reader.Close();
            return resultList;
        }


        internal static List<Tuple<string, int>> getMonthlyQuantityOfSpecificProduct(string bookID, string srcCategoryID, DateTime srcDate)
        {
            var con = ConnectDB.openConnection();
            var sql = "WITH Months as (select month(GETDATE()) as Monthnumber, datename(month, GETDATE()) as NameOfMonth, 1 as number union all select month(dateadd(month, number, (GETDATE()))) Monthnumber, datename(month, dateadd(month, number, (GETDATE()))) as NameOfMonth, number + 1  from Months  where number < 12) select NameOfMonth, Quantity from Months left join (select datepart(month, o.ordersTime) as OrderMonth, SUM(do.odQuantity) as Quantity from ORDERSDETAIL do join BOOK p on do.bookID = p.bookID join Orders o on do.OrdersID = o.OrdersID where datepart(year, o.ordersTime) = @SelectedYear and p.bookID = @bookID and p.tobID = @tobID group by datepart(month, o.ordersTime)) MonthlyQuantity on Months.Monthnumber = MonthlyQuantity .OrderMonth order by Monthnumber;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@bookID";
            sqlParameter.Value = bookID;

            var sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@tobID";
            sqlParameter1.Value = srcCategoryID;

            string sqlFormattedDate = srcDate.ToString("yyyy");
            var sqlParameter2 = new SqlParameter();
            sqlParameter2.ParameterName = "@SelectedYear";
            sqlParameter2.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);

            command.Parameters.Add(sqlParameter2);
            command.Parameters.Add(sqlParameter);
            command.Parameters.Add(sqlParameter1);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                string monthName = (string)reader["NameOfMonth"];
                int quantity = 0;

                if (reader["Quantity"].GetType() != typeof(DBNull))
                {
                    quantity = (int)reader["Quantity"];
                }
                resultList.Add(Tuple.Create(monthName, quantity));
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, int>> getYearlyQuantityOfSpecificProduct(string bookID, string srcCategoryID)
        {
            var con = ConnectDB.openConnection();
            var sql = "select convert(varchar, datepart(year, o.ordersTime)) as OrderYear, SUM(do.odQuantity) as Quantity from ORDERSDETAIL do join BOOK p on do.bookID = p.bookID join Orders o on do.OrdersID = o.ordersID join TYPEOFBOOK cat on p.tobID = cat.tobID where p.bookID = @bookID and cat.tobID = @tobID group by convert(varchar, datepart(year, o.ordersTime)) order by convert(varchar, datepart(year, o.ordersTime));";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@bookID";
            sqlParameter.Value = bookID;

            var sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@tobID";
            sqlParameter1.Value = srcCategoryID;

            var command = new SqlCommand(sql, con);

            command.Parameters.Add(sqlParameter);
            command.Parameters.Add(sqlParameter1);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                resultList.Add(Tuple.Create((string)reader["OrderYear"], (int)reader["Quantity"]));
            }
            reader.Close();
            return resultList;
        }

        internal static List<Tuple<string, int>> getBookQuantityInCategory(string srcCategoryID)
        {
            var sql = "select p.bookName, sum(do.odQuantity) as Quantity from BOOK p join ORDERSDETAIL do on p.bookID = do.bookID join Orders o on o.ordersID = do.OrdersID where p.tobID = @SelectedCategory group by p.bookName;";
            var con = ConnectDB.openConnection();
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedCategory";
            sqlParameter.Value = srcCategoryID;

            var command = new SqlCommand(sql, con);

            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var resultList = new List<Tuple<string, int>>();
            while (reader.Read())
            {
                string bookName = (string)reader["bookName"];
                int quantity = 0;

                if (reader["Quantity"].GetType() != typeof(DBNull))
                {
                    quantity = (int)reader["Quantity"];
                }
                resultList.Add(Tuple.Create(bookName, quantity));
            }
            reader.Close();
            return resultList;
        }


    }
}