using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DTO
{
    public class BookDTO:ICloneable
    {
        public string bookID { get; set; }
        public string bookName { get; set; }
        public string bookAuthor { get; set; }
        public string tobID { get; set; }
        public decimal bookPrice { get; set; }
        public int bookQuantity { get; set; }
        public int bookPublishedYear { get; set; }

        public string linkImg { get; set; }

        public object Clone()
        {
            return MemberwiseClone() as BookDTO;
        }
    }

    public class BestSellingBook : BookDTO
    {
        public int Quantity { get; set; }
    }
}
