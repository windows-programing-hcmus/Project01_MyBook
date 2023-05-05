using Project01_MyBook.DAO;
using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01_MyBook.BUS
{
    internal class OrderDetailBUS
    {
        public static List<OrderDetailDTO> findOrderDetailByOrderID(string orderID)
        {
            if (orderID == null || orderID.Equals(""))
            {
                return null;
            }
            return OrderDetailDAO.findOrderDetailByOrderID(orderID);
        }
        public static int findTotalBookByOrderID(string orderID)
        {
            if (orderID == null || orderID.Equals(""))
            {
                return 0;
            }
            return OrderDetailDAO.findTotalBookByOrderID(orderID);
        }

        internal static bool InsertOrderDetail(OrderDetailDTO od)
        {
            return OrderDetailDAO.InsertOrderDetail(od);
        }
    }
}
