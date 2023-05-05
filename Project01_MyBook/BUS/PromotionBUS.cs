using Project01_MyBook.DAO;
using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.BUS
{
    internal class PromotionBUS
    {
        public static PromotionDTO findPromotionByID(string promoID)
        {
            if (promoID == null || promoID.Equals(""))
            {
                return null;
            }
            return PromotionDAO.findPromotionByID(promoID);
        }
        public static List<PromotionDTO> findAllPromotion()
        {
            return PromotionDAO.findAllPromotion();
        }
        public static bool InsertPromotion(PromotionDTO promo)
        {
            if (PromotionBUS.findPromotionByID(promo.promoID) != null)
            {
                return false;
            }
            return PromotionDAO.InsertPromotion(promo);
        }

        public static bool UpdatePromotion(PromotionDTO promo)
        {
            if (PromotionBUS.findPromotionByID(promo.promoID) == null)
            {
                return false;
            }
            return PromotionDAO.UpdatePromotion(promo);
        }

        internal static PromotionDTO findBestPromotion(string tobID)
        {
            return PromotionDAO.findBestPromotion(tobID);
        }
    }
}
