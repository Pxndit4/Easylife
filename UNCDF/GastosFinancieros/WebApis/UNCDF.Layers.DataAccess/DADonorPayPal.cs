using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DADonorPayPal
    {
        public static int Insert(MDonorPayPal ent, BaseRequest baseRequest)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DonorPaypal_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonorId", SqlDbType.Int).Value = ent.DonorId;
                    cmd.Parameters.Add("@IPaymentId", SqlDbType.VarChar).Value = ent.PaymentId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (SqlException ex)
                {
                    return 2;
                }
            }
        }
    }
}
