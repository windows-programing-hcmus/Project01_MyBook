using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DTO
{
    internal class OrderDetailDTO
    {
        public string ordersID { get; set; }
        public string bookID { get; set; }
        public decimal odCurrentPrice { get; set; }
        public int odQuantity { get; set; }
        public decimal odDiscountedPrice { get; set; }
        public decimal odTempPrice { get; set; }
    }
}