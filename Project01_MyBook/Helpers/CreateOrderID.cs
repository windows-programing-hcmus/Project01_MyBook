using Project01_MyBook.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.Helpers
{
    internal class CreateOrderID
    {
        public static string generatedID()
        {
            string id = "";
            do
            {
                DateTime localDate = DateTime.Now;

                int day = localDate.Day;
                int month = localDate.Month;
                int year = localDate.Year - 2000;

                string yearStr = year > 10 ? $"{year}" : $"0{year}";
                string monthStr = month > 10 ? $"{month}" : $"0{month}";
                string dayStr = day > 10 ? $"{day}" : $"0{day}";

                id = yearStr + monthStr + dayStr;

                int h = localDate.Hour;
                int min = localDate.Minute;
                int sc = localDate.Second;

                char word1 = Convert.ToChar((h % 25) + 65);
                var word2 = Convert.ToChar((min % 25) + 65);
                var word3 = Convert.ToChar((sc % 25) + 65);

                id += $"{word1}{word2}{word3}";


            } while (OrderBUS.findOrderByID(id) != null);
            return id;
        }
    }
}
