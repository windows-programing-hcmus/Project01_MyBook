using Project01_MyBook.DTO;
using Project01_MyBook.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DAO
{
    internal class TypeOfBookDAO
    {
        internal static TypeOfBookDTO findTypeOfBookByID(string tobID)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM TYPEOFBOOK WHERE tobID = '{tobID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string tobName = (string)reader["tobName"];
                             
                var tob = new TypeOfBookDTO()
                {
                    tobID = tobID,
                    tobName = tobName
                };
                return tob;
            }
            reader.Close();
            return null;
        }

        internal static List<TypeOfBookDTO> findAllTypeOfBook()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT * FROM TYPEOFBOOK";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<TypeOfBookDTO> tobs = new List<TypeOfBookDTO>();
            while (reader.Read())
            {
                string tobID = (string)reader["tobID"];
                string tobName = (string)reader["tobName"];
                
                var tob = new TypeOfBookDTO()
                {
                    tobID = tobID,
                    tobName = tobName,
                };
                tobs.Add(tob);
            }
            reader.Close();
            return tobs;
        }


        internal static bool InsertTypeOfBook(TypeOfBookDTO tob)
        {
            var con = ConnectDB.openConnection();

            var sql = "INSERT INTO TYPEOFBOOK(tobID, tobName) " +
                $" VALUES('{tob.tobID}', N'{tob.tobName}')";
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

        internal static bool UpdateTypeOfBook(TypeOfBookDTO tob)
        {
            var con = ConnectDB.openConnection();

            var sql = $"UPDATE TYPEOFBOOK SET tobName = N'{tob.tobName}'" +
                $" WHERE tobID = '{tob.tobID}'";

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
        internal static bool DeleteTypeOfBook(string tobID)
        {
            var con = ConnectDB.openConnection();

            var sql = "alter table TYPEOFBOOK nocheck constraint all\n" +
                "alter table BOOK nocheck constraint all\n" +
                "alter table PROMOTIONDETAIL nocheck constraint all\n" +
                $"delete from TYPEOFBOOK where tobID = '{tobID}'\n" +
                "alter table TYPEOFBOOK check constraint all\n" +
                "alter table BOOK check constraint all\n" +
                "alter table PROMOTIONDETAIL check constraint all\n";

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
