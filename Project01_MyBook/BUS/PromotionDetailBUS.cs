using Project01_MyBook.DAO;
using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.BUS
{
    internal class PromotionDetailBUS
    {
        public static bool InsertPromotionDetail(PromotionDetailDTO promo)
        {
            return PromotionDetailDAO.InsertPromotionDetail(promo);
        }
    }
}
