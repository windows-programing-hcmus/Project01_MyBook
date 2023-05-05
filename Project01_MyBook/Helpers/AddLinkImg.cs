using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.Helpers
{
    internal class AddLinkImg
    {
        static public List<BookDTO> addLinkstoBook(List<BookDTO> lists)
        {
            foreach (var item in lists)
            {
                item.linkImg = $"/Resource/Images/BookCovers/{item.bookID}.jpg";
            }

            return lists;
        }
       /* static public List<BestSellingBook> addLinkstoBookBestSell(List<BestSellingBook> lists)
        {
            foreach (var item in lists)
            {

                item.linkImg = $"/Resource/Images/BookCovers/{item.bookID}.jpg";
            }

            return lists;
        }*/


    }
}
