using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.DTO
{
    public class PromotionDTO:ICloneable
    {
        public string promoID { get; set; }
        public string promoName { get; set; }
        public float promoDiscount { get; set; }
        public string promoDesciption { get; set; }
        public DateTime promoStartTime { get; set; }
        public DateTime promoEndTime { get; set; }
        public bool promoStatus { get; set; } = true;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
