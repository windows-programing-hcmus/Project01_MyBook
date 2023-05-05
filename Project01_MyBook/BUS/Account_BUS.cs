using Project01_MyBook.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.BUS
{
   internal class Account_BUS
    {
        public static bool findUser(string username, string password)
        {
            if (username == null || username == "" || password == null || password == "")
            {
                return false;
            }
            return Account_DAO.findUserByUsernamePassword(username, password);
        }
        public static string getHashedPasswordByUsername(string username)
        {
            if (username == null || username == "")
            {
                return null;
            }
            return Account_DAO.getHashedPasswordByUsername(username);
        }
    }
}
