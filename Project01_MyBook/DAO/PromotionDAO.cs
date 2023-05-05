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
    internal class PromotionDAO
    {
        internal static PromotionDTO findPromotionByID(string promoID)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM PROMOTION WHERE promoID = '{promoID}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string promoName = (string)reader["promoName"];
                float promoDiscount = (float)reader["promoDiscount"];
                string promoDesciption = (string)reader["promoDescription"];
                var promoStartTime = (DateTime)reader["promoStartTime"];
                var promoEndTime = (DateTime)reader["promoEndTime"];
                bool promoStatus = (bool)reader["promoStatus"];
                
                var promo = new PromotionDTO()
                {
                    promoID = promoID,
                    promoName = promoName,
                    promoDiscount = promoDiscount,
                    promoDesciption = promoDesciption,
                    promoStartTime = promoStartTime,
                    promoEndTime = promoEndTime,
                    promoStatus = promoStatus,
                };
                return promo;
            }
            reader.Close();
            return null;
        }

        internal static PromotionDTO findBestPromotion(string tobID)
        {
            var con = ConnectDB.openConnection();

            var sql = "Select p.* from Promotion p, Promotiondetail pd " +
                "where p.promoID = pd.promoID " +
                "and p.promoStatus = 1 " +
                $"and pd.tobID = '{tobID}' " +
                "and p.promoDiscount = " +
                "(Select Max(p.promoDiscount) from Promotion p, Promotiondetail pd " +
                "where p.promoID = pd.promoID " +
                "and p.promoStatus = 1 " +
                $"and pd.tobID = '{tobID}')";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string promoID = (string)reader["promoID"];
                string promoName = (string)reader["promoName"];
                float promoDiscount = (float)reader["promoDiscount"];
                string promoDesciption = (string)reader["promoDescription"];
                var promoStartTime = (DateTime)reader["promoStartTime"];
                var promoEndTime = (DateTime)reader["promoEndTime"];
                bool promoStatus = (bool)reader["promoStatus"];

                var promo = new PromotionDTO()
                {
                    promoID = promoID,
                    promoName = promoName,
                    promoDiscount = promoDiscount,
                    promoDesciption = promoDesciption,
                    promoStartTime = promoStartTime,
                    promoEndTime = promoEndTime,
                    promoStatus = promoStatus,
                };
                return promo;
            }
            reader.Close();
            return null;
        }

        internal static List<PromotionDTO> findAllPromotion()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT * FROM PROMOTION";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            var promos = new List<PromotionDTO>();
            while (reader.Read())
            {
                string promoID = (string)reader["promoID"];         
                string promoName = (string)reader["promoName"];
                float promoDiscount = (float)reader["promoDiscount"];
                string promoDesciption = (string)reader["promoDescription"];
                var promoStartTime = (DateTime)reader["promoStartTime"];
                var promoEndTime = (DateTime)reader["promoEndTime"];
                bool promoStatus = (bool)reader["promoStatus"];

                var promo = new PromotionDTO()
                {
                    promoID = promoID,
                    promoName = promoName,
                    promoDiscount = promoDiscount,
                    promoDesciption = promoDesciption,
                    promoStartTime = promoStartTime,
                    promoEndTime = promoEndTime,
                    promoStatus = promoStatus
                };
                promos.Add(promo);
            }
            reader.Close();
            return promos;
        }

        internal static bool InsertPromotion(PromotionDTO promo)
        {
            var con = ConnectDB.openConnection();

            var sql = "INSERT INTO PROMOTION(promoID, promoName,promoDiscount, promoDescription, promoStartTime, promoEndTime, promoStatus) " +
                $"VALUES('{promo.promoID}', N'{promo.promoName}', {promo.promoDiscount}, " +
                $"N'{promo.promoDesciption}', '{promo.promoStartTime}', '{promo.promoEndTime}', '{promo.promoStatus}')";
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

        internal static bool UpdatePromotion(PromotionDTO promo)
        {
            var con = ConnectDB.openConnection();

            var sql = $"UPDATE PROMOTION SET promoName = N'{promo.promoName}', promoDiscount = {promo.promoDiscount}, promoDescription = N'{promo.promoDesciption}', " +
                $"promoStartTime = '{promo.promoStartTime}', promoEndTime = '{promo.promoEndTime}', promoStatus = '{promo.promoStatus}'" +
                $"WHERE promoID = '{promo.promoID}'";

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
