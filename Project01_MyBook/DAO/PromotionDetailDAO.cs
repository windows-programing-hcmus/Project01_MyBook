using Project01_MyBook.DTO;
using Project01_MyBook.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DAO
{
    internal class PromotionDetailDAO
    {
        internal static bool InsertPromotionDetail(PromotionDetailDTO promo)
        {
            var con = ConnectDB.openConnection();

            var sql = "INSERT INTO PROMOTIONDETAIL(promoID, tobID) " +
                $"VALUES('{promo.promoID}', '{promo.tobID}')";

            var command = new SqlCommand(sql, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
