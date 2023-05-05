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
    internal class CustomerDAO
    {
        internal static CustomerDTO findCustomerByPhoneNumber(string cusPhoneNumber)
        {
            var con = ConnectDB.openConnection();

            var sql = $"SELECT * FROM CUSTOMER WHERE cusPhoneNumber = '{cusPhoneNumber}'";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string cusName = (string)reader["cusName"];

                var cus = new CustomerDTO()
                {
                    cusPhoneNumber = cusPhoneNumber,
                    cusName = cusName
                };
                return cus;
            }
            reader.Close();
            return null;
        }

        internal static List<CustomerDTO> findAllCustomer()
        {
            var con = ConnectDB.openConnection();

            var sql = "SELECT * FROM CUSTOMER";

            var command = new SqlCommand(sql, con);
            var reader = command.ExecuteReader();
            List<CustomerDTO> cuss = new List<CustomerDTO>();
            while (reader.Read())
            {
                string cusPhoneNumber = (string)reader["cusPhoneNumber"];
                string cusName = (string)reader["cusName"];

                var cus = new CustomerDTO()
                {
                    cusPhoneNumber = cusPhoneNumber,
                    cusName = cusName,
                };
                cuss.Add(cus);
            }
            reader.Close();
            return cuss;
        }

        internal static bool InsertCustomer(CustomerDTO cus)
        {
            var con = ConnectDB.openConnection();

            var sql = "INSERT INTO CUSTOMER(cusPhoneNumber, cusName) " +
                $"VALUES('{cus.cusPhoneNumber}', N'{cus.cusName}')";
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
