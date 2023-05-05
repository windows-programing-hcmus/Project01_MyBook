using Project01_MyBook.Config;
using Project01_MyBook.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DAO
{
    internal class Account_DAO
    {
        internal static bool findUserByUsernamePassword(string username, string password)
        {
            var con = ConnectDB.openConnection();

            var sql = $"Select * from account where accUsername = '{username}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string accPassword = (string)reader["accPassword"];
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, accPassword))
                {
                    AppConfig.SetValue(AppConfig.Password, accPassword);
                    return true;
                }
                return false;
            }
            reader.Close();
            return false;
        }

        internal static string getHashedPasswordByUsername(string username)
        {
            var con = ConnectDB.openConnection();

            var sql = $"Select * from account where accUsername = '{username}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string accPassword = (string)reader["accPassword"];

                return accPassword;
            }
            reader.Close();
            return null;
        }

    }
}
