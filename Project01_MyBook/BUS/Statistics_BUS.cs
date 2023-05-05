using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project01_MyBook.DAO;
using Project01_MyBook.DTO;

namespace Project01_MyBook.BUS
{

    internal class Statistics_BUS
    { 
        public string getTotalRevenueUntilDate(DateTime src)
        {
            return Statistics_DAO.getTotalRevenueUntilDate(src);
        }

        public string getTotalProfitUntilDate(DateTime src)
        {
            return Statistics_DAO.getTotalProfitUntilDate(src);
        }

        public int getTotalOrdersUntilDate(DateTime src)
        {
            return Statistics_DAO.getTotalOrdersUntilDate(src);
        }

        public List<Tuple<string, decimal>> getDailyRevenue(DateTime src)
        {
            return Statistics_DAO.getDailyRevenue(src);
        }

        public List<Tuple<string, decimal>> getWeeklyRevenue(DateTime src)
        {
            return Statistics_DAO.getWeeklyRevenue(src);
        }

        public List<Tuple<string, decimal>> getMonthlyRevenue(DateTime src)
        {
            return Statistics_DAO.getMonthlyRevenue(src);
        }

        public List<Tuple<string, decimal>> getYearlyRevenue()
        {
            return Statistics_DAO.getYearlyRevenue();
        }

        public List<Tuple<string, decimal>> getDailyProfit(DateTime src)
        {
            return Statistics_DAO.getDailyProfit(src);
        }

        public List<Tuple<string, decimal>> getWeeklyProfit(DateTime src)
        {
            return Statistics_DAO.getWeeklyProfit(src);
        }

        public List<Tuple<string, decimal>> getMonthlyProfit(DateTime src)
        {
            return Statistics_DAO.getMonthlyProfit(src);
        }

        public List<Tuple<string, decimal>> getYearlyProfit()
        {
            return Statistics_DAO.getYearlyProfit();
        }

        public List<Tuple<string, int>> getDailyQuantityOfSpecificProduct(string srcPhoneID, string srcCategoryID, DateTime srcDate)
        {
            return Statistics_DAO.getDailyQuantityOfSpecificProduct(srcPhoneID, srcCategoryID, srcDate);
        }

        public List<Tuple<string, int>> getMonthlyQuantityOfSpecificProduct(string srcPhoneID, string srcCategoryID, DateTime srcDate)
        {
            return Statistics_DAO.getMonthlyQuantityOfSpecificProduct(srcPhoneID, srcCategoryID, srcDate);
        }

        public List<Tuple<string, int>> getYearlyQuantityOfSpecificProduct(string srcPhoneID, string srcCategoryID)
        {
            return Statistics_DAO.getYearlyQuantityOfSpecificProduct(srcPhoneID, srcCategoryID);
        }

        public List<Tuple<string, int>> getBookQuantityInCategory(string srcCategoryID)
        {
            return Statistics_DAO.getBookQuantityInCategory(srcCategoryID);
        }

    }
}
