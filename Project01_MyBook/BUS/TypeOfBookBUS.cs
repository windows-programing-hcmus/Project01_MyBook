using Project01_MyBook.DAO;
using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.BUS
{   
    internal class TypeOfBookBUS
    {
        public static TypeOfBookDTO findTypeOfBookByID(string tobID)
        {
            if (tobID == null || tobID.Equals(""))
            {
                return null;
            }
            return TypeOfBookDAO.findTypeOfBookByID(tobID);
        }

        public static List<TypeOfBookDTO> findAllTypeOfBook()
        {
            return TypeOfBookDAO.findAllTypeOfBook();
        }
        public static List<BookDTO> findAllBook()
        {
            return BookDAO.findAllBook();
        }
        public static bool InsertTypeOfBook(TypeOfBookDTO tob)
        {
            if (TypeOfBookBUS.findTypeOfBookByID(tob.tobID) != null)
            {
                return false;
            }
            return TypeOfBookDAO.InsertTypeOfBook(tob);
        }

        public static bool UpdateTypeOfBook(TypeOfBookDTO tob)
        {
            if (TypeOfBookBUS.findTypeOfBookByID(tob.tobID) == null)
            {
                return false;
            }
            return TypeOfBookDAO.UpdateTypeOfBook(tob);
        }
        public static bool DeleteTypeOfBook(string tobID)
        {
            if (tobID == null || tobID.Equals(""))
            {
                return false;
            }
            if (TypeOfBookBUS.findTypeOfBookByID(tobID) == null)
            {
                return false;
            }
            return TypeOfBookDAO.DeleteTypeOfBook(tobID);
        }
    }
}
