using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DADonorFrequency
    {
        public static int Insert(MDonorFrequency ent, BaseRequest baseRequest)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DonorFrequency_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonorId", SqlDbType.Int).Value = ent.DonorId;
                    cmd.Parameters.Add("@IQuantity", SqlDbType.Int).Value = ent.Quantity;
                    cmd.Parameters.Add("@IFrequency", SqlDbType.Int).Value = ent.Frequency;
                    cmd.Parameters.Add("@IAmount", SqlDbType.Decimal).Value = ent.Amount;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
        }

        public static int Update(MDonorFrequency ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DonorFrequency_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonorId", SqlDbType.Int).Value = ent.DonorId;
                    cmd.Parameters.Add("@IQuantity", SqlDbType.Int).Value = ent.Quantity;
                    cmd.Parameters.Add("@IFrequency", SqlDbType.Int).Value = ent.Frequency;
                    cmd.Parameters.Add("@IPaymentNumber", SqlDbType.Decimal).Value = ent.PaymentNumber;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
        }


        public static List<MDonorFrequency> Select(MDonorFrequency ent, ref int Val)
        {
            List<MDonorFrequency> lstresult = new List<MDonorFrequency>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DonorFrequency_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDate", SqlDbType.VarChar).Value = ent.Date;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MDonorFrequency result = new MDonorFrequency();
                            result.DonorId = Convert.ToInt32(reader["DonorId"]);
                            result.Amount = Convert.ToDecimal(reader["Amount"]);
                            result.Frequency = Convert.ToInt32(reader["Frequency"]);
                            result.Quantity = Convert.ToInt32(reader["Quantity"]);
                            result.Date = Convert.ToInt32(reader["NextDonation"]);
                            result.PaymentNumber = Convert.ToInt32(reader["PaymentNumber"]);
                            result.FirstName = Convert.ToString(reader["FirstName"]);
                            result.LastName = Convert.ToString(reader["LastName"]);
                            result.Email = Convert.ToString(reader["Email"]);

                            lstresult.Add(result);
                            Val = 0;
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    Val = 2;
                }
            }
            return lstresult;
        }
    }
}
