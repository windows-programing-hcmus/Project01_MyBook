using Project01_MyBook.DTO;
using Project01_MyBook.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DAO
{
    internal class BookDAO
    {
        public static BookDTO findBookByID(string bookID)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM BOOK WHERE bookID = '{bookID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string bookName = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobID = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var  book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = bookName,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                return book;
            }
            reader.Close();
            return null;
        }


        internal static List<BookDTO> findBookByName(string bookName)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM BOOK WHERE bookName LIKE N'%{bookName}%'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string name = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobID = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = name,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }

        internal static List<BookDTO> findNearOutOfBook()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT * FROM BOOK WHERE bookQuantity < 5 ";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string bookName = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobId = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = bookName,
                    tobID = tobId,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }
        internal static List<BookDTO> findNearOutOfBookByTOB(string tobID)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM BOOK WHERE bookQuantity < 5 AND tobID = '{tobID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string bookName = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobId = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = bookName,
                    tobID = tobId,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }
        internal static List<BookDTO> findBestSellerBook()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT * FROM BOOK b," +
                " (SELECT bookID FROM ORDERSDETAIL GROUP BY bookID HAVING SUM(odQuantity) > 5) bsb" +
                " WHERE b.bookID = bsb.bookID";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string name = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobID = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = name,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }

        internal static List<BookDTO> findBestSellerBookByTOB(string tobID)
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT * FROM BOOK b," +
                " (SELECT bookID FROM ORDERSDETAIL GROUP BY bookID HAVING SUM(odQuantity) > 5) bsb" +
                $" WHERE b.bookID = bsb.bookID AND tobID = '{tobID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string name = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobId = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = name,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }

        internal static int countBookOnSell()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT COUNT(bookQuantity) FROM BOOK";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteScalar();

            Int32 result = (Int32)reader;

            return (int)result;
        }

        internal static int countBookSold()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT SUM(odQuantity) FROM ORDERSDETAIL";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteScalar();

            Int32 result = (Int32) reader;

            return (int) result;
        }

        internal static int countBookSoldInDate(string date)
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT SUM(odQuantity) FROM ORDERSDETAIL OD, ORDERS O" +
                " WHERE OD.ordersID = O.ordersID" +
                $" AND FORMAT(O.ordersTime, 'yyyy-MM-dd')  = '{date}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteScalar();
            reader = (reader == DBNull.Value) ? 0 : reader;

            Int32 result = (Int32)reader;

            return (int)result;
        }
        internal static List<BookDTO> findBookByRangePrice(int min, int max)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM BOOK WHERE bookPrice BETWEEN {min} AND {max}";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string bookName = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobID = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = bookName,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }

        internal static List<BookDTO> findBookByTypeOfBook(string tob)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM BOOK WHERE tobID = '{tob}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string bookName = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobID = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = bookName,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }

        internal static List<BookDTO> findTop5()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT TOP 5 * FROM BOOK WHERE bookQuantity < 5 ORDER BY bookQuantity ASC";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string bookName = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobID = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = bookName,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }

        public static List<BookDTO> findAllBook()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT * FROM BOOK";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string bookName = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobID = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = bookName,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }
        internal static List<BookDTO> findAllBookByTOB(string tobID)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM BOOK WHERE tobID = '{tobID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<BookDTO> books = new List<BookDTO>();
            while (reader.Read())
            {
                string bookID = (string)reader["bookID"];
                string bookName = (string)reader["bookName"];
                string bookAuthor = (string)reader["bookAuthor"];
                string tobId = (string)reader["tobID"];
                decimal bookPrice = (decimal)reader["bookPrice"];
                //int bookPrice = 0;
                int bookQuantity = (int)reader["bookQuantity"];
                int bookPublishedYear = (int)reader["bookPublishedYear"];
                var book = new BookDTO()
                {
                    bookID = bookID,
                    bookName = bookName,
                    tobID = tobID,
                    bookAuthor = bookAuthor,
                    bookPrice = bookPrice,
                    bookQuantity = bookQuantity,
                    bookPublishedYear = bookPublishedYear
                };
                books.Add(book);
            }
            reader.Close();
            return books;
        }
        public static bool InsertBook(BookDTO book)
        {
            var con = ConnectDB.openConnection();

            var sql = "INSERT INTO BOOK(bookID, tobID, bookName, bookAuthor, bookPrice, bookPublishedYear, bookQuantity) " +
                $"VALUES('{book.bookID}', '{book.tobID}', N'{book.bookName}', N'{book.bookAuthor}', {book.bookPrice}, {book.bookPublishedYear}, {book.bookQuantity})";

            var command = new SqlCommand(sql, con);
            try
            {
                command.ExecuteNonQuery();
            } catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }
        public static bool UpdateBook(BookDTO book)
        {
            var con = ConnectDB.openConnection();

            var sql = $"UPDATE BOOK SET tobID = '{book.tobID}', bookName = N'{book.bookName}', bookAuthor = N'{book.bookAuthor}', " +
                $"bookPrice = {(int)book.bookPrice}, bookPublishedYear = {book.bookPublishedYear}, bookQuantity = {book.bookQuantity} " +
                $"WHERE bookID = '{book.bookID}'";

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
        internal static bool DeleteBook(string bookID)
        {
            var con = ConnectDB.openConnection();

            var sql = "alter table Book nocheck constraint all\n" +
                "alter table Ordersdetail nocheck constraint all\n" +
                $"delete from Book where bookID = '{bookID}'\n" +
                "alter table Book check constraint all\n" +
                "alter table Ordersdetail check constraint all\n";

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


        internal static List<BestSellingBook> getBestSellingInWeek(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd hh:mm:ss");
            var con = ConnectDB.openConnection();
            var sql = "select TOP(5) p.bookID,p.bookName, sum(do.odQuantity) as Quantity from Orders o join ORDERSDETAIL do on o.ordersID = do.OrdersID join BOOK p on p.bookID = do.bookID where ordersTime between DATEADD(DAY, -7, @SelectedDate) and DATEADD(DAY, 1, @SelectedDate) group by p.bookID, p.bookName order by sum(do.odQuantity) desc;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            List<BestSellingBook> list = new List<BestSellingBook>();
            while (reader.Read())
            {
                var bookID = (String)reader["bookID"];
                var bookName = (String)reader["bookName"];
                var Quantity = (int)reader["Quantity"];
                var linkImg = $"/Resource/Images/BookCovers/{bookID}.jpg";

                BestSellingBook book = new BestSellingBook()
                {

                    bookName = bookName,
                    Quantity = Quantity,
                    linkImg=linkImg
                };
              
                if (book.bookName != "")
                    list.Add(book);
            }
            reader.Close();
            return list;
        }

        internal static List<BestSellingBook> getBestSellingInMonth(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd hh:mm:ss");
            var con = ConnectDB.openConnection();
            var sql = "select TOP(5) p.bookID,p.bookName, sum(do.odQuantity) as Quantity from Orders o join ORDERSDETAIL do on o.ordersID = do.OrdersID join BOOK p on p.bookID = do.bookID where datepart(year, ordersTime) = datepart(year, @SelectedDate) and datepart(month, ordersTime) = datepart(month, @SelectedDate) group by p.bookID, p.bookName order by sum(do.odQuantity) desc;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            List<BestSellingBook> list = new List<BestSellingBook>();

            while (reader.Read())
            {

                var bookID = (String)reader["bookID"];
                var bookName = (String)reader["bookName"];
                var Quantity = (int)reader["Quantity"];
                var linkImg = $"/Resource/Images/BookCovers/{bookID}.jpg";

                BestSellingBook book = new BestSellingBook()
                {


                    bookName = bookName,
                    Quantity = Quantity,
                    linkImg = linkImg
                };
               
                if (book.bookName != "")
                    list.Add(book);
            }
            reader.Close();
            return list;
        }

        internal static List<BestSellingBook> getBestSellingInYear(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd hh:mm:ss");
            var con = ConnectDB.openConnection();
            var sql = "select TOP(5) p.bookID, p.bookName, sum(do.odQuantity) as Quantity from Orders o join ORDERSDETAIL do on o.ordersID = do.OrdersID join BOOK p on p.bookID = do.bookID where datepart(year, ordersTime) = datepart(year, @SelectedDate) group by p.bookID, p.bookName order by sum(do.odQuantity) desc;";
            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, con);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            List<BestSellingBook> list = new List<BestSellingBook>();

            while (reader.Read())
            {

                var bookID = (String)reader["bookID"];
                var bookName = (String)reader["bookName"];
                var Quantity = (int)reader["Quantity"];
                var linkImg = $"/Resource/Images/BookCovers/{bookID}.jpg";

                BestSellingBook book = new BestSellingBook()
                {


                    bookName = bookName,
                    Quantity = Quantity,
                    linkImg = linkImg
                };

                if (book.bookName != "")
                    list.Add(book);
            }
            reader.Close();
            return list;
        }


    }
}
